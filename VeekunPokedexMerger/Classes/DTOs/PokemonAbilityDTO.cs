using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class PokemonAbilityDTO
    {
        public int pokemon_id { get; set; }
        public int ability_id { get; set; }
        public int is_hidden { get; set; }
        public int slot { get; set; }
    }
}