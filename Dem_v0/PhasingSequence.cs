using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    // ESTO TIENE UN PROBLEMA SI NO TENGO EN FASE LOS MENSAJES QUE PRETENDO QUE ME LLEGUEN NO VA A FUNCIONAR
    // PERO BUENO, ASÍ ES LA VIDA.
    internal enum PhasingPattern
    {
        None,
        DxRxRx,
        DxDxRx,
        RxRxRx
    }

    internal static class PhasingSequence
    {
        // Predicados
        private static bool IsDx(int v) => v == 125;
        private static bool IsRx(int v) => v >= 104 && v <= 111;

        // Intenta detectar un patrón válido en cualquier ventana de 3 valores dentro de 'sequence'.
        // Devuelve true y el patrón encontrado en 'pattern' si existe; false y PhasingPattern.None en caso contrario.

        public static bool TryCaracter(int msj)
        {
            if(IsDx(msj) || IsRx(msj))
            {
                return true; 
            }
            else
            {
                return false;
            }
        }
        public static bool TryDetect(List<(int,int)> sequence, out PhasingPattern pattern)
        {
            pattern = PhasingPattern.None;
            if (sequence == null || sequence.Count < 3) return false;

            for (int i = 0; i + 3 <= sequence.Count; i++)
            {
                int a = sequence[i], b = sequence[i + 1], c = sequence[i + 2];

                if (IsDx(a) && IsRx(b) && IsRx(c))
                {
                    pattern = PhasingPattern.DxRxRx;
                    return true;
                }

                if (IsDx(a) && IsDx(b) && IsRx(c))
                {
                    pattern = PhasingPattern.DxDxRx;
                    return true;
                }

                // Todavia no contempla si Rx esta en orden
                if (IsRx(a) && IsRx(b) && IsRx(c))
                {
                    pattern = PhasingPattern.RxRxRx;
                    return true;
                }
            }

            return false;
        }

        // Versión conveniente que devuelve el tipo de patrón (PhasingPattern.None si no hay coincidencia).
        public static PhasingPattern Detect(IReadOnlyList<int> sequence)
        {
            return TryDetect(sequence, out var p) ? p : PhasingPattern.None;
        }
    }
}
