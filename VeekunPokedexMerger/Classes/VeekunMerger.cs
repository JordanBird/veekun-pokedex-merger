using System;
using System.Collections.Generic;
using System.Text;

namespace VeekunPokedexMerger.Classes
{
    public static class VeekunMerger
    {
        public static void Merge()
        {
            PokemonMerger.Merge();
            MoveMerger.Merge();
        }
    }
}