using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class PokemonDTO
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public int species_id { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public int base_experience { get; set; }
        public int order { get; set; }
        public int is_default { get; set; }
    }
}