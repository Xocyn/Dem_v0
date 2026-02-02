using System;
using System.Collections.Generic;
using System.Text;

namespace Dem_v0
{
    internal class Socorro
    {
        // El socorro es broadcast, no necesita el MMSI del receptor
        // la funcion me devuelve un int que representa cuantas posiciones debo avanzar luego de leer el MMSI
        public static int MMSI(int i, int form, string input)
        {
            int j = 0;
            List<int> MMSI = new List<int>();

            string ventana = input.Substring(i+10, 10);
            int mensajeInt = Convert.ToInt32(ventana, 2);
            Decodificador.TryDecodificarMensaje(mensajeInt, out int valor);

                if (form==valor) // es el primer format recibido
                {
                    
                }
                else // es el segundo format recibido
                {
                   
                }

            return j;

        }
    }
}
