using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class PokemonMovesDTO
    {
        public int pokemon_id { get; set; }
        public int version_group_id { get; set; }
        public int move_id { get; set; }
        public int pokemon_move_method_id { get; set; }
        public int level { get; set; }
        public int? order { get; set; }
    }
}