using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class CSVHolderDTO
    {
        public IEnumerable<StatsDTO> stats { get; set; }

        public IEnumerable<PokemonDTO> pokemon { get; set; }
        public IEnumerable<PokemonSpeciesNamesDTO> pokemon_names { get; set; }

        public IEnumerable<PokemonFormsDTO> pokemon_forms { get; set; }
        public IEnumerable<PokemonFormNamesDTO> pokemon_form_names { get; set; }

        public IEnumerable<TypeNamesDTO> type_names { get; set; }
        public IEnumerable<PokemonTypesDTO> pokemon_types { get; set; }

        public IEnumerable<MovesDTO> moves { get; set; }
        public IEnumerable<MoveNamesDTO> move_names { get; set; }

        public IEnumerable<PokemonMovesDTO> pokemon_moves { get; set; }
        public IEnumerable<PokemonStatsDTO> pokemon_stats { get; set; }

        public IEnumerable<AbilitiesDTO> abilities { get; set; }
        public IEnumerable<AbilityNameDTO> ability_names { get; set; }
        public IEnumerable<PokemonAbilityDTO> pokemon_abilities { get; set; }
    }
}