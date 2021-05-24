using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using VeekunPokedexMerger.Classes.DTOs;

namespace VeekunPokedexMerger.Classes.Facades
{
    public static class CSVHolderFacade
    {
        public static CSVHolderDTO Generate()
        {
            return new CSVHolderDTO()
            {
                stats = GetCSV<StatsDTO>("stats"),

                pokemon = GetCSV<PokemonDTO>("pokemon"),
                pokemon_names = GetCSV<PokemonSpeciesNamesDTO>("pokemon_species_names"),

                pokemon_forms = GetCSV<PokemonFormsDTO>("pokemon_forms"),
                pokemon_form_names = GetCSV<PokemonFormNamesDTO>("pokemon_form_names"),

                type_names = GetCSV<TypeNamesDTO>("type_names"),
                pokemon_types = GetCSV<PokemonTypesDTO>("pokemon_types"),

                moves = GetCSV<MovesDTO>("moves"),
                move_names = GetCSV<MoveNamesDTO>("move_names"),

                pokemon_moves = GetCSV<PokemonMovesDTO>("pokemon_moves"),
                pokemon_stats = GetCSV<PokemonStatsDTO>("pokemon_stats"),

                abilities = GetCSV<AbilitiesDTO>("abilities"),
                ability_names = GetCSV<AbilityNameDTO>("ability_names"),
                pokemon_abilities = GetCSV<PokemonAbilityDTO>("pokemon_abilities")
            };
        }

        private static IEnumerable<T> GetCSV<T>(string filename)
        {
            return GetCSVFile<T>($"{Directory.GetCurrentDirectory()}\\CSVs\\{filename}.csv");
        }

        private static IEnumerable<T> GetCSVFile<T>(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<T>().ToArray();
            }
        }
    }
}