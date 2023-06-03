using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using VeekunPokedexMerger.Classes.DTOs;
using VeekunPokedexMerger.Classes.Facades;

namespace VeekunPokedexMerger.Classes
{
    public static class PokemonMerger
    {
        private const int LOCALE = 9;

        public static void Merge()
        {
            var csvs = CSVHolderFacade.Generate();

            var output = new List<ExportDTO>();
            foreach (var mon in csvs.pokemon_forms)
            {
                output.Add(GetExportForPokemon(mon, csvs));

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

        private static ExportDTO GetExportForPokemon(PokemonFormsDTO mon, CSVHolderDTO csvs)
        {
            var speciesId = csvs.pokemon.FirstOrDefault(x => x.id == mon.pokemon_id).species_id;

            var name = "";
            if (String.IsNullOrEmpty(mon.form_identifier))
            {
                name = csvs.pokemon_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.pokemon_species_id == speciesId).name;
            }
            else
            {
                name = csvs.pokemon_form_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.pokemon_form_id == mon.id)?.pokemon_name;

                if (String.IsNullOrEmpty(name))
                {
                    name = csvs.pokemon_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.pokemon_species_id == speciesId).name;
                }
            }

            var output = new ExportDTO()
            {
                NationalDexNumber = speciesId,
                Name = name,

                Type1 = GetType(mon, 1, csvs),
                Type2 = GetType(mon, 2, csvs),

                HP = GetStat(mon, "hp", csvs),
                Attack = GetStat(mon, "attack", csvs),
                Defense = GetStat(mon, "defense", csvs),
                SpecialAttack = GetStat(mon, "special-attack", csvs),
                SpecialDefense = GetStat(mon, "special-defense", csvs),
                Speed = GetStat(mon, "speed", csvs),

                Ability1 = GetAbility(mon, 1, csvs),
                Ability2 = GetAbility(mon, 2, csvs),
                HiddenAbility = GetAbility(mon, 3, csvs),

                Moves = GetMoves(mon, csvs)
            };

            output.Total = output.HP + output.Attack + output.Defense + output.SpecialAttack + output.SpecialDefense + output.Speed;

            return output;
        }

        private static int GetStat(PokemonFormsDTO pokemon, string stat, CSVHolderDTO csvs)
        {
            return csvs.pokemon_stats.FirstOrDefault(x => x.pokemon_id == pokemon.pokemon_id && x.stat_id == csvs.stats.FirstOrDefault(s => s.identifier.ToUpper() == stat.ToUpper())?.id)?.base_stat ?? 0;
        }

        private static string GetType(PokemonFormsDTO pokemon, int slot, CSVHolderDTO csvs)
        {
            return csvs.type_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.type_id == csvs.pokemon_types.FirstOrDefault(t => t.pokemon_id == pokemon.pokemon_id && t.slot == slot)?.type_id)?.name;
        }

        private static string GetMoves(PokemonFormsDTO pokemon, CSVHolderDTO csvs)
        {
            var pokemonMoves = csvs.pokemon_moves.Where(x => x.pokemon_id == pokemon.pokemon_id);

            var output = "|";
            foreach (var pokemonMove in pokemonMoves)
            {
                var move = csvs.move_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.move_id == pokemonMove.move_id).name;

                output += $"{move}|";
            }

            return output;
        }

        private static string GetAbility(PokemonFormsDTO pokemon, int slot, CSVHolderDTO csvs)
        {
            return csvs.ability_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.ability_id == csvs.pokemon_abilities.FirstOrDefault(t => t.pokemon_id == pokemon.pokemon_id && t.slot == slot)?.ability_id)?.name;
        }
    }
}