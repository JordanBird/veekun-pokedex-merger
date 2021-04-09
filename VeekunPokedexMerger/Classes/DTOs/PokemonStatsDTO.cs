using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class PokemonStatsDTO
    {
        public int pokemon_id { get; set; }
        public int stat_id { get; set; }
        public int base_stat { get; set; }
        public int effort { get; set; }
    }
}