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
    public static class MoveHolderFacade
    {
        public static MoveHolderDTO Generate()
        {
            return new MoveHolderDTO()
            {
                moves = GetCSV<MovesDTO>("moves"),
                move_names = GetCSV<MoveNamesDTO>("move_names"),

                type_names = GetCSV<TypeNamesDTO>("type_names")
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