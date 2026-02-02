using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Dem_v0
{
    internal class Decodificador
    {
        public static int DecodificarMensaje(int mensaje10Bits)
        {
            // 1. Extraer campos
            int datos = mensaje10Bits >> 3;        // 7 bits de datos
            int control = mensaje10Bits & 0b111;     // 3 bits de control
            int valor = 0;
            int ceros = 0;

            // 2. Contar ceros en los 7 bits de datos y reconstruir carácter
            for (int i = 0; i < 7; i++)
            {
               int bit = (datos >> i) & 1;   // lee LSB → MSB
               valor |= bit << (6 - i);      // asigna peso invertido
                if (((datos >> i) & 1) == 0)
                    ceros++;
            }

            // 3. Verificar control de errores
            if (ceros != control)
                Console.WriteLine("Error de control: cantidad de ceros incorrecta");

            // 4. Devuelve el valor del carácter
            return valor;
        }

        // Nuevo: versión "try" que indica si el mensaje de 10 bits es válido.
        // Devuelve true si la verificación de control coincide y sale el valor reconstruido.
        public static bool TryDecodificarMensaje(int mensaje10Bits, out int valor)
        {
            int datos = mensaje10Bits >> 3;        // 7 bits de datos
            int control = mensaje10Bits & 0b111;   // 3 bits de control
            int val = 0;
            int ceros = 0;

            for (int i = 0; i < 7; i++)
            {
                int bit = (datos >> i) & 1;   // lee LSB → MSB
                val |= bit << (6 - i);       // asigna peso invertido
                if (bit == 0)
                    ceros++;
            }

            if (ceros != control)
            {
                valor = 0;
                return false;
            }

            valor = val;
            return true;
        }

        public static bool DxRx(string input, int i) // verificia si Dx y Rx son iguales
        { 
            string ventana = input.Substring(i, 10);
            int mensajeInt = Convert.ToInt32(ventana, 2);
            TryDecodificarMensaje(mensajeInt, out int valor);
            string ventana2 = input.Substring(i+40, 10);
            int mensajeInt2 = Convert.ToInt32(ventana2, 2);
            TryDecodificarMensaje(mensajeInt2, out int valor2);
            if (valor == valor2)
            {
                Console.WriteLine("Dx y Rx son iguales");
                return true;
            }
            else 
            {
                Console.WriteLine("Dx y Rx NO son iguales");
                return false;
            }
        }

    }
}
