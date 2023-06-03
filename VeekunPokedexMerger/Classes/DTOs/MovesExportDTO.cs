using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class MovesExportDTO
    {
        public string Name { get; set; }
        public int? Power { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public bool MultiTarget { get; set; }
    }
}