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

            string ventana = input.Substring(i+20, 10);
            int mensajeInt = Convert.ToInt32(ventana, 2);
            Decodificador.TryDecodificarMensaje(mensajeInt, out int valor);

                if (form==valor) // es el primer format recibido
                {
                    // decodifico el MMSI en base a esta posicion
                    i = i + 40;
                    for (int k = 0; k < 100; k += 10)
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
                    for (int k = 0; k < 100; k += 10)
                    {
                        ventana = input.Substring(i + k, 10);
                        mensajeInt = Convert.ToInt32(ventana, 2);
                        Decodificador.TryDecodificarMensaje(mensajeInt, out valor);
                        MMSI.Add(valor);
                    }
                }

                for (int w = 0; w < MMSI.Count; w += 2) // Incrementa de 2 en 2
                {
                Console.WriteLine($"{MMSI[w]}");
                }

                j = i + 100; // lo deveria dejar en la posicion del mensaje 1 (nature of distress)

            return j;

        }

        public static void NatureofDistress(int i, string input)
        {
            string ventana = input.Substring(i, 10);
            int mensajeInt = Convert.ToInt32(ventana, 2);
            Decodificador.TryDecodificarMensaje(mensajeInt, out int valor);
                switch (valor)
                {
                    case 100: 
                        Console.WriteLine("Incendio/Explosión");
                        break;
                    case 101: 
                        Console.WriteLine("Inundación");
                        break;
                    case 102: 
                        Console.WriteLine("Colision");
                        break;
                    case 103: 
                        Console.WriteLine("Encallado");
                        break;
                    case 104: 
                        Console.WriteLine("Peligro de zozobra");
                        break;
                    case 105: 
                        Console.WriteLine("Hundimiento");  
                        break;
                    case 106: 
                        Console.WriteLine("Deshabilitado y a la deriva");
                        break;
                    case 107: 
                        Console.WriteLine("Socorro sin designar");
                        break;
                    case 108: 
                        Console.WriteLine("Abandonando la nave");
                        break;
                    case 109: 
                        Console.WriteLine("Pirateria/Robo a mano armada");
                        break;
                    case 110: 
                        Console.WriteLine("Hombre al agua");
                        break;
                    case 112:
                        Console.WriteLine("EPIRB emitido");
                        break;
                    default:
                    // no hacer nada
                        break;
                }
        }

    }
}
