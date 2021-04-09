using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class PokemonSpeciesNamesDTO
    {
        public int pokemon_species_id { get; set; }
        public int local_language_id { get; set; }
        public string name { get; set; }
        public string genus { get; set; }
    }
}