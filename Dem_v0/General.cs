using MathNet.Numerics.Providers.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Dem_v0
{
    internal class General
    {
        static public int Categoria(int i, int form, string input, List<int> ECC)
        {
            int j = 0;
            string ventana = input.Substring(i + 20, 10);
            int mensajeInt = Convert.ToInt32(ventana, 2);
            Decodificador.TryDecodificarMensaje(mensajeInt, out int valor);
            if (form == valor) // es el primer format recibido
            {
                // decodifico el categoria en base a esta posicion
                i = i + 40;
                ventana = input.Substring(i, 10);
                mensajeInt = Convert.ToInt32(ventana, 2);
                Decodificador.TryDecodificarMensaje(mensajeInt, out valor);
                if (valor == 108)
                {
                    Console.WriteLine("Seguridad");
                }
                else if (valor == 110)
                {
                    Console.WriteLine("Urgencia");
                }
                else
                {
                    Console.WriteLine("Categoria corrupta");
                }
            }
            else // es el segundo format recibido
            {
                // decodifico el categoria en base a esta posicion
                i = i + 20;
                ventana = input.Substring(i, 10);
                mensajeInt = Convert.ToInt32(ventana, 2);
                Decodificador.TryDecodificarMensaje(mensajeInt, out valor);
                if (valor == 108)
                {
                    Console.WriteLine("Seguridad");
                }
                else if (valor == 110)
                {
                    Console.WriteLine("Urgencia");
                }
                else
                {
                    Console.WriteLine("Categoria corrupta");
                }
            }
            j = i + 20;
            return j;
        }

        static public int MMSI_2(int i, string input, List<int> ECC)
        {
            int j = 0;
            List<int> MMSI = new List<int>();
            List<int> same = new List<int>();
            List<string> fail = new List<string> { "X", "X", "X", "X", "X", "X", "X", "X", "X" };
            string ventana;
            int mensajeInt;
            int valor;

            for (int k = 0; k < 100; k += 10)
            {
                ventana = input.Substring(i + k, 10);
                mensajeInt = Convert.ToInt32(ventana, 2);
                Decodificador.TryDecodificarMensaje(mensajeInt, out valor);
                MMSI.Add(valor);
                if (Decodificador.DxRx(input, i + k))
                {
                    same.Add(valor);
                }
            }

            Geografica.EliminarPosicionesImpares(MMSI);
            bool mismoContenido = !MMSI.Except(same).Any() && !same.Except(MMSI).Any();

            foreach (int valorMMSI in MMSI)
            {
                ECC.Add(valorMMSI);
            }

            if (mismoContenido)
            {
                Console.WriteLine("MMSI DX/RX coinciden");
                Console.WriteLine($"MMSI: {string.Join(" | ", MMSI)}");

            }
            else
            {
                Console.WriteLine("MMSI DX/RX NO coinciden");
                Console.WriteLine($"MMSI desconocido: {string.Join(" | ", fail)}");

            }
            j = i + 100;
            return j;
        }

        static public int Frecuencia(int i, string input, List<int> ECC)
        {
            int j = 0;



            return j;
        }

    }
}
