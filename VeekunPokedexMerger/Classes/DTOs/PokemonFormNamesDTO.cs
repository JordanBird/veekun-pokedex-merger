using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class PokemonFormNamesDTO
    {
        public int pokemon_form_id { get; set; }
        public int local_language_id { get; set; }
        public string form_name { get; set; }
        public string pokemon_name { get; set; }
    }
}