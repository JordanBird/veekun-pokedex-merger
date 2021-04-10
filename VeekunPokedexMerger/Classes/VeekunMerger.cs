using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using VeekunPokedexMerger.Classes.DTOs;

namespace VeekunPokedexMerger.Classes
{
    public static class VeekunMerger
    {
        private const int LOCALE = 9;

        public static void Merge()
        {
            var stats = GetCSV<StatsDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\stats.csv");

            var pokemon = GetCSV<PokemonDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\pokemon.csv");
            var pokemon_names = GetCSV<PokemonSpeciesNamesDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\pokemon_species_names.csv");

            var pokemon_forms = GetCSV<PokemonFormsDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\pokemon_forms.csv");
            var pokemon_form_names = GetCSV<PokemonFormNamesDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\pokemon_form_names.csv");

            var type_names = GetCSV<TypeNamesDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\type_names.csv");
            var pokemon_types = GetCSV<PokemonTypesDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\pokemon_types.csv");

            var moves = GetCSV<MovesDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\moves.csv");
            var move_names = GetCSV<MoveNamesDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\move_names.csv");

            var pokemon_moves = GetCSV<PokemonMovesDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\pokemon_moves.csv");
            var pokemon_stats = GetCSV<PokemonStatsDTO>($"{Directory.GetCurrentDirectory()}\\CSVs\\pokemon_stats.csv");

            var output = new List<ExportDTO>();
            foreach (var mon in pokemon_forms)
            {
                output.Add(GetExportForPokemon(mon, pokemon, pokemon_names, pokemon_forms, pokemon_form_names, type_names, pokemon_types, stats, moves, move_names, pokemon_moves, pokemon_stats));

                Console.WriteLine($"Pooling {mon.identifier}");
            }

            Console.WriteLine("Exporting..");

            Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\Output");
            using (var writer = new StreamWriter($"{Directory.GetCurrentDirectory()}\\Output\\Merged Veekun Pokedex.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(output);
            }

            Console.WriteLine("Export Completed.");
        }

        private static ExportDTO GetExportForPokemon(PokemonFormsDTO mon, IEnumerable<PokemonDTO> pokemon, IEnumerable<PokemonSpeciesNamesDTO> pokemon_names, IEnumerable<PokemonFormsDTO> pokemon_forms, IEnumerable<PokemonFormNamesDTO> pokemon_form_names, IEnumerable<TypeNamesDTO> type_names, IEnumerable<PokemonTypesDTO> pokemon_types, IEnumerable<StatsDTO> stats, IEnumerable<MovesDTO> moves, IEnumerable<MoveNamesDTO> moveNames,  IEnumerable<PokemonMovesDTO> pokemon_moves, IEnumerable<PokemonStatsDTO> pokemon_stats)
        {
            var speciesId = pokemon.FirstOrDefault(x => x.id == mon.pokemon_id).species_id;

            var name = "";
            if (String.IsNullOrEmpty(mon.form_identifier))
            {
                name = pokemon_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.pokemon_species_id == speciesId).name;
            }
            else
            {
                name = pokemon_form_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.pokemon_form_id == mon.id)?.pokemon_name;

                if (String.IsNullOrEmpty(name))
                {
                    name = pokemon_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.pokemon_species_id == speciesId).name;
                }
            }

            var output = new ExportDTO()
            {
                NationalDexNumber = speciesId,
                Name = name,

                Type1 = GetType(mon, 1, type_names, pokemon_types),
                Type2 = GetType(mon, 2, type_names, pokemon_types),

                HP = GetStat(mon, "hp", stats, pokemon_stats),
                Attack = GetStat(mon, "attack", stats, pokemon_stats),
                Defense = GetStat(mon, "defense", stats, pokemon_stats),
                SpecialAttack = GetStat(mon, "special-attack", stats, pokemon_stats),
                SpecialDefense = GetStat(mon, "special-defense", stats, pokemon_stats),
                Speed = GetStat(mon, "speed", stats, pokemon_stats),

                Moves = GetMoves(mon, moveNames, pokemon_moves)
            };

            output.Total = output.HP + output.Attack + output.Defense + output.SpecialAttack + output.SpecialDefense + output.Speed;

            return output;
        }

        private static int GetStat(PokemonFormsDTO pokemon, string stat, IEnumerable<StatsDTO> stats, IEnumerable<PokemonStatsDTO> pokemon_stats)
        {
            return pokemon_stats.FirstOrDefault(x => x.pokemon_id == pokemon.pokemon_id && x.stat_id == stats.FirstOrDefault(s => s.identifier.ToUpper() == stat.ToUpper())?.id)?.base_stat ?? 0;
        }

        private static string GetType(PokemonFormsDTO pokemon, int slot, IEnumerable<TypeNamesDTO> type_names, IEnumerable<PokemonTypesDTO> pokemon_types)
        {
            return type_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.type_id == pokemon_types.FirstOrDefault(t => t.pokemon_id == pokemon.pokemon_id && t.slot == slot)?.type_id)?.name;
        }

        private static string GetMoves(PokemonFormsDTO pokemon, IEnumerable<MoveNamesDTO> moveNames, IEnumerable<PokemonMovesDTO> pokemon_moves)
        {
            var pokemonMoves = pokemon_moves.Where(x => x.pokemon_id == pokemon.pokemon_id);

            var output = "|";
            foreach (var pokemonMove in pokemonMoves)
            {
                var move = moveNames.FirstOrDefault(x => x.local_language_id == LOCALE && x.move_id == pokemonMove.move_id).name;

                output += $"{move}|";
            }

            return output;
        }

        private static IEnumerable<T> GetCSV<T>(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<T>().ToArray();
            }
        }
    }
}