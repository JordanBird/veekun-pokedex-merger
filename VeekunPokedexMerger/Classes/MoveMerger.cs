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
    public static class MoveMerger
    {
        private const int LOCALE = 9;

        public static void Merge()
        {
            var csvs = MoveHolderFacade.Generate();

            var output = new List<MovesExportDTO>();
            foreach (var move in csvs.moves)
            {
                output.Add(GetExportForMove(move, csvs));

                Console.WriteLine($"Pooling {move.identifier}");
            }

            Console.WriteLine("Exporting..");

            Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\Output");
            using (var writer = new StreamWriter($"{Directory.GetCurrentDirectory()}\\Output\\Merged Veekun Movedex.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(output);
            }

            Console.WriteLine("Export Completed.");
        }

        private static MovesExportDTO GetExportForMove(MovesDTO move, MoveHolderDTO csvs)
        {
            var moveName = csvs.move_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.move_id == move.id).name;

            var output = new MovesExportDTO()
            {
                Name = moveName,
                Power = move.power,
                Category = GetCategory(move.damage_class_id),
                MultiTarget = GetMultiTarget(move.target_id),
                Type = GetType(move, csvs)
            };

            return output;
        }

        //TODO: Pull from CSV: https://github.com/veekun/pokedex/blob/master/pokedex/data/csv/move_damage_classes.csv
        private static string GetCategory(int? damageClassId)
        {
            switch (damageClassId)
            {
                case 1:
                    return "Status";
                case 2:
                    return "Physical";
                case 3:
                    return "Special";
                default:
                    return "Unknown";
            }
        }

        //TODO: Verify and pull from CSV
        private static bool GetMultiTarget(int? targetId)
        {
            switch (targetId)
            {
                case 11:
                case 12:
                case 13:
                case 14:
                    return true;
                default:
                    return false;
            }
        }

        private static string GetType(MovesDTO move, MoveHolderDTO csvs)
        {
            return csvs.type_names.FirstOrDefault(x => x.local_language_id == LOCALE && x.type_id == move.type_id)?.name;
        }
    }
}