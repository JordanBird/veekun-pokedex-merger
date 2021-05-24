using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class AbilitiesDTO
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public int generation_id { get; set; }
        public bool is_main_series { get; set; }
    }
}