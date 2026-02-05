using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dem_v0
{
    internal class Geografica
    {
        public static void AreaGeografica(int i, string input)
        {
            // obtengo latitud (paralelo al ecuador)

            // luego obtengo longitud (paralelo a greenwich)

        }

        public static int PuntoGeografico(int i, string input, out bool valid) // lo uso para socorro (grados y minutos)
        {
            int j = 0;
            List<int> PuntoGeo = new List<int>();
            List<int> same = new List<int>();
            List<int> fail = new List<int> { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            string ventana;
            int mensajeInt;

            for (int k = 0; k < 100; k += 10)
            {
                ventana = input.Substring(i + k, 10);
                mensajeInt = Convert.ToInt32(ventana, 2);
                Decodificador.TryDecodificarMensaje(mensajeInt, out int valor);
                PuntoGeo.Add(valor);  // aca obtengo el 1234567890 ahora debo aplicar "mascaras" / elimino las posiciones impares
                if (Decodificador.DxRx(input, i + k))
                {
                    same.Add(valor);
                }
            }

            EliminarPosicionesImpares(PuntoGeo);
            bool mismoContenido = !PuntoGeo.Except(same).Any() && !same.Except(PuntoGeo).Any();

            // Ahora con PuntoGeo puedo decodificar toda la data
            // AGREGAR: Formato lindo para cada uno de los valores NE/NW/SE/SW

            if (mismoContenido)
            {
                Console.WriteLine("Coordenadas DX/RX coinciden");
                Console.WriteLine($"Ubicacion: {string.Join(" | ", PuntoGeo)}");
                valid = true;
            }
            else
            {
                Console.WriteLine("Coordenadas DX/RX NO coinciden");
                Console.WriteLine($"Ubicacion desconocida: {string.Join(" | ", fail)}");
                valid = false;
            }

            j = i + 100;
            return j;





        }

        public static void EliminarPosicionesImpares(List<int> lista)
        {
            // Recorrer de atrás hacia adelante para evitar problemas al eliminar
            for (int i = lista.Count - 1; i >= 0; i--)
            {
                if (i % 2 != 0) // Si la posición es impar
                {
                    lista.RemoveAt(i);
                }
            }
        }

        public static int UTC(int i, string input)
        {
            // obtengo hora UTC
            int j = 0;
            List<int> UTC = new List<int>();
            string ventana;
            int mensajeInt;

            for (int k = 0; k < 40; k += 10)
            {
                ventana = input.Substring(i + k, 10);
                mensajeInt = Convert.ToInt32(ventana, 2);
                Decodificador.TryDecodificarMensaje(mensajeInt, out int valor);
                UTC.Add(valor);
            }
            EliminarPosicionesImpares(UTC);
            Console.WriteLine($"Hora UTC: {string.Join(" | ", UTC)}");
            j= i + 40;

            return j;
        }



    }
}
