using System;
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

        public static int PuntoGeografico(int i, string input) // lo uso para socorro (grados y minutos)
        {
            int j = 0;
            List<int> PuntoGeo = new List<int>();
            string ventana;
            int mensajeInt;
          
                for (int k = 0; k < 100; k += 10) 
                {
                    ventana = input.Substring(i + k, 10);
                    mensajeInt = Convert.ToInt32(ventana, 2);
                    Decodificador.TryDecodificarMensaje(mensajeInt, out int valor);
                    PuntoGeo.Add(valor);  // aca obtengo el 1234567890 ahora debo aplicar "mascaras" / elimino las posiciones impares
                }

            EliminarPosicionesImpares(PuntoGeo);
            Console.WriteLine($"Ubicacion: {string.Join(" | ", PuntoGeo)}");
            // Ahora con PuntoGeo puedo decodificar toda la data

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
    }
}
