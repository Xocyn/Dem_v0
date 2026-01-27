using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dem_v0
{
    internal class Program
    {
        // Lee la secuencia binaria desde un archivo. Si se pasa un argumento, se usa como ruta;
        // si no, se usa "prueba_bits_1_2.txt" en el directorio actual.
        static void Main(string[] args)
        {
            string path = args.Length > 0 ? args[0] : "prueba_bits_1_2.txt";

            if (!File.Exists(path))
            {
                Console.WriteLine($"Archivo no encontrado: {path}");
                return;
            }

            string raw = File.ReadAllText(path);

            // Filtrar sólo caracteres '0' y '1' (elimina saltos de línea, espacios, etc.)
            string input = new string(raw.Where(c => c == '0' || c == '1').ToArray());

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("El archivo no contiene datos binarios (0/1).");
                return;
            }

            Console.WriteLine($"Usando archivo: {path}");
            Console.WriteLine($"Secuencia (longitud {input.Length}): {input}");

            List<(int Index, int Value)> encontrados = new List<(int, int)>();
            int i = 0;

            // Ventana deslizante: intenta decodificar 10 bits; si no es válido o no es caracter de phasing,
            // desplaza 1 bit; si es válido y es caracter de phasing, consume los 10 bits.

            // NO ME CONVENCE, TENGO QUE PREGUNTAR SI EL ARCHIVO QUE VOY A UTILIZAR VA A SER VALIDO SIEMPRE.

            while (i + 10 <= input.Length)
            {
                string ventana = input.Substring(i, 10);
                int mensajeInt = Convert.ToInt32(ventana, 2);

                if (Decodificador.TryDecodificarMensaje(mensajeInt, out int valor))
                {
                    // mensaje de 10 bits válido según el control
                    if (PhasingSequence.TryCaracter(valor))
                    {
                        // Es un carácter válido de phasing: registrar y consumir los 10 bits
                        encontrados.Add((i, valor));
                        i += 10;

                        // nose si va a leer todo el vector para verificar
                        // por ahora lo dejo asi

                        const int ventanaDetect = 3;
                        if (encontrados.Count >= ventanaDetect)
                        {
                            //var ultimaVentana = valores.Skip(valores.Count - ventanaDetect).ToList();
                            if (PhasingSequence.TryDetect(encontrados, out var pattern))
                            {
                                Console.WriteLine($"Patrón de phasing detectado: {pattern}");
                            }
                            // No modificar 'encontrados' aquí; seguir buscando en la secuencia cuando corresponda.
                        }
                    }
                    else
                    {
                        // Decodificable pero no es el carácter esperado: desplazar 1 bit
                        i += 1;
                    }

                }
                else
                {
                    // No decodificable: desplazar 1 bit
                    i += 1;
                }
            }

            if (encontrados.Count == 0)
            {
                Console.WriteLine("No se detectaron mensajes válidos en la secuencia.");
            }
            else
            {
                Console.WriteLine($"Detectados {encontrados.Count} mensaje(s):");
                foreach (var e in encontrados)
                {
                    string printable = (e.Value >= 32 && e.Value <= 126) ? ((char)e.Value).ToString() : ".";
                    Console.WriteLine($"- Offset {e.Index}: valor numérico = {e.Value}, carácter imprimible = {printable}");
                }
            }
        }
    }
}