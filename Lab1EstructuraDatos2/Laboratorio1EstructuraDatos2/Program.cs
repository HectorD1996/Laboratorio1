using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Laboratorio1EstructuraDatos2
{
    class Program
    {
        Huffman h = new Huffman();
        static void Main(string[] args)
        {
            Huffman h = new Huffman();
            Console.Write("Compress>");
            string comando = Console.ReadLine();
            try
            {
                string[] parte = comando.Split(' ');
                if (!((comando.StartsWith("rle.exe ") || comando.StartsWith("huff.exe")) && (parte[1].Equals("-c") || parte[1].Equals("-d")) && (parte[2].Equals("-f\""))))
                {
                    Console.WriteLine("Compress>Comando no valido.");
                    Console.WriteLine("rle.exe Ejecutar por el método RLE\nhuff.exe Ejecutar por el método Huffman");
                    Console.WriteLine("-c Comprimir\n-d Descomprimir");
                    Console.WriteLine("-f\"<Direccion del Archivo>\"");
                    Console.ReadKey();
                    
                }
                else if (comando.StartsWith(".exit"))
                {
                    Environment.Exit(0);//salir
                }
                string path = parte[2];//nombre del archivo -> 'path'
                path = path.Substring(3);
                path = path.Substring(0, path.Length - 1);

                if (comando.StartsWith("rle.exe"))//RLE
                {
                    if (parte[1].StartsWith("-c"))//Comprimir
                    {
                        Console.WriteLine("rle c");
                        Compress.compressRLE(path);
                    }
                    else//Descomprimir
                    {
                        Console.WriteLine("rle d");
                        Compress.decompresRLE(path);
                    }
                }
                else//HUFFMAN
                {
                    if (parte[1].StartsWith("-c"))//Comprimir
                    {
                        Console.WriteLine("huff c");
                        h.HuffmanIniciar(path);
                    }
                    else//Descomprimir
                    {
                        Console.WriteLine("huff d");
                    }
                }
                //en esta parte:
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Compress> Comando no valido.");
                Console.WriteLine("rle.exe Ejecutar por el método RLE\nhuff.exe Ejecutar por el método Huffman");
                Console.WriteLine("-c Comprimir\n-d Descomprimir");
                Console.WriteLine("-f\"<Direccion del Archivo>\"");
                Console.WriteLine("Ejemplo:");
                Console.WriteLine("Compress>rle.exe -c -f\"Texto1.txt\"");
                Console.WriteLine();
            }
        }
    }
}
