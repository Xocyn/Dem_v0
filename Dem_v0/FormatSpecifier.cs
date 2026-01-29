using Dem_v0;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dem_v0
{
    internal static class FormatSpecifier
    {
        // necesito filtrar primero los valores que pueden quedar del phasing
        public static void Filtro(int f_msj, out int j)
        {
            j = 0; // Inicializar obligatoriamente

            if (PhasingSequence.TryCaracter(f_msj))
            {
                // descartar
                Console.WriteLine("Valor descartado");
                j = 1; // Asignar valor para salir del while
            }
            else
            {
                Console.WriteLine(Formato(f_msj));
                j = 0; // Mantener el while activo
            }
        }

        public static string Formato(int valor)
        {
            return valor switch
            {
                112 => "Socorro",
                116 => "AllShips",
                114 => "Grupo",
                120 => "Individual",
                102 => "Geografica",
                123 => "Individual2",
                _ => "Valor no reconocido" // Caso por defecto
            };
        }
    }
}

