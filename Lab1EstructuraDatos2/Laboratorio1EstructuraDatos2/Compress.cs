using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Laboratorio1EstructuraDatos2
{
    public class Compress
    {
        static string pathRLE = ".rlex";
        public static void compressRLE(string path)
        {
            Byte[] buffer = File.ReadAllBytes(path);
            List<int> valores = new List<int>();
            bool termino = false;
            for (int i = 0; !termino; i++)
            {
                bool hayRepetidos = true;
                int centinela = 0;
                byte actual = buffer[i];
                for (int r = i; centinela < 256 && hayRepetidos; r++)
                {
                    if (r == buffer.Length)
                    {
                        termino = true;
                        hayRepetidos = false;
                    }
                    else if (actual.Equals(buffer[r]))
                        centinela++;
                    else
                        hayRepetidos = false;
                    i = r - 1;
                }
                valores.Add((int)actual);//    #1 CARACTER A REPETIR
                valores.Add(centinela); //     #2 CANTIDAD DE REPETICIONES 
            }
            //escribir en un archivo
            Byte[] salida = new Byte[valores.Count];
            for (int i = 0; i < salida.Length; i++)
            {
                salida[i] = (byte)valores[i];
            }
            File.WriteAllBytes(path + pathRLE, salida);//escribimos el archivo

            dateDcompression(buffer, salida);
        }
        public static void decompresRLE(string path)
        {
            Byte[] buffer = File.ReadAllBytes(path), salida;
            List<Byte> salidal = new List<byte>();
            int actual = 0, cantidad = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                if ((i % 2) == 0)//par -> #2 CANTIDAD DE REPETICIONES
                {
                    cantidad = buffer[i];
                    for (int x = 0; x < cantidad; x++)
                    {
                        salidal.Add((byte)actual);
                    }
                }
                else//impar -> #1 CARACTER A REPETIR
                {
                    actual = buffer[i];
                }
            }
            salida = new Byte[salidal.Count];
            for (int i = 0; i < salida.Length; i++)
            {
                salida[i] = salidal[i];
            }
            path = path.Substring(0, path.Length - 5);
            File.WriteAllBytes(path, salida);
        }
        public static void dateDcompression(Byte[] after, Byte[] before)
        {
            double ratio = 0, factor = 0, percentage = 0;
            ratio = after.Length / before.Length;
            factor = before.Length / after.Length;
            percentage = (before.Length - after.Length) / before.Length;

            Console.WriteLine("Estadisticas del archivo generado:");
            Console.WriteLine();
            Console.WriteLine(" * Tamaño Original:       " + after.Length);
            Console.WriteLine(" * Tamaño Final:          " + before.Length);
            Console.WriteLine(" * Ratio de Compresión:   " + Math.Round(ratio, 2));
            Console.WriteLine(" * Factor de Compresión:  " + Math.Round(factor, 2));
            Console.WriteLine(" * Porcentaje Ahorrado:   " + Math.Round(percentage, 2) + "%");
        }
    }
}
