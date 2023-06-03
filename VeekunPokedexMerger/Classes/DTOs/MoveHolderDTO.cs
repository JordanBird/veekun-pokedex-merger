using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes.DTOs
{
    public class MoveHolderDTO
    {
        public IEnumerable<MovesDTO> moves { get; set; }
        public IEnumerable<MoveNamesDTO> move_names { get; set; }

        public IEnumerable<TypeNamesDTO> type_names { get; set; }
    }
}