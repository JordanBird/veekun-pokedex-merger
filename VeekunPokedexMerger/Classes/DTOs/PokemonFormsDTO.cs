using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class PokemonFormsDTO
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public string form_identifier { get; set; }
        public int pokemon_id { get; set; }
        public int introduced_in_version_group_id { get; set; }
        public int is_default { get; set; }
        public int is_battle_only { get; set; }
        public int is_mega { get; set; }
        public int form_order { get; set; }
        public int order { get; set; }
    }
}