using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class MovesDTO
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public int generation_id { get; set; }
        public int type_id { get; set; }
        public int? power { get; set; }
        public int? pp { get; set; }
        public int? accuracy { get; set; }
        public int? priority { get; set; }
        public int? target_id { get; set; }
        public int? damage_class_id { get; set; }
        public int? effect_id { get; set; }
        public int? effect_chance { get; set; }
        public int? contest_type_id { get; set; }
        public int? contest_effect_id { get; set; }
        public int? super_contest_effect_id { get; set; }
    }
}