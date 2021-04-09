using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class StatsDTO
    {
        public int id { get; set; }
        public int? damage_class_id { get; set; }
        public string identifier { get; set; }
        public int is_battle_only { get; set; }
        public int? game_index { get; set; }
    }
}