using Dem_v0;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dem_v0
{
    internal static class FormatSpecifier
    {
        // necesito filtrar primero los valores que pueden quedar del phasing

        private static bool Socorro(int v) => v == 112;
        private static bool Allships(int v) => v == 116;
        private static bool Grupo(int v) => v == 114;
        private static bool Individual(int v) => v == 120;
        private static bool Geografica(int v) => v == 102;
        private static bool Individual2(int v) => v == 123;

        public static void Filtro(int f_msj)
        {
            if (PhasingSequence.TryCaracter(f_msj))
            {
                // descartar
            }
            else
            {
                // procesar


            }

        }
    }
}

