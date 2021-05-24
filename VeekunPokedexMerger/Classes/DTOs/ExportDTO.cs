using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class ExportDTO
    {
        public int NationalDexNumber { get; set; }

        public string Name { get; set; }

        public string Type1 { get; set; }
        public string Type2 { get; set; }

        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int Total { get; set; }

        public string Ability1 { get; set; }
        public string Ability2 { get; set; }
        public string HiddenAbility { get; set; }

        public string Moves { get; set; }
    }
}