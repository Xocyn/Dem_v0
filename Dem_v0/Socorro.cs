using System;
using System.Collections;
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
                    // decodifico el MMSI en base a esta posicion
                    i = i + 40;
                    for (int k = 0; k < 90; k += 10)
                    {
                        ventana = input.Substring(i + k, 10);
                        mensajeInt = Convert.ToInt32(ventana, 2);
                        Decodificador.TryDecodificarMensaje(mensajeInt, out valor);
                        MMSI.Add(valor);
                    }
                }
                else // es el segundo format recibido
                {
                    // decodifico el MMSI en base a esta posicion
                    i = i + 20;
                    for (int k = 0; k < 90; k += 10)
                    {
                        ventana = input.Substring(i + k, 10);
                        mensajeInt = Convert.ToInt32(ventana, 2);
                        Decodificador.TryDecodificarMensaje(mensajeInt, out valor);
                        MMSI.Add(valor);
                    }
                }

                for (int w = 0; w < MMSI.Count; w += 2) // Incrementa de 2 en 2
                {
                Console.WriteLine($"{MMSI[i]}");
                }

                j = i + 100;

            return j;

        }
    }
}
