using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio1EstructuraDatos2
{
    public class Huffman
    {
        List<HuffmanNode> nodeList;
        ProcessMethods pMethods = new ProcessMethods();
        public  void HuffmanIniciar(string Path)
        {
            while (true)
            {
                Console.Clear();
                nodeList = pMethods.getListFromFile(Path);
                Console.Clear();
                if (nodeList == null)
                {
                   
                    Console.WriteLine("NO se Pudo Leer el Archivo");
                    Console.WriteLine("Pressthe any key to continue or Enter \"E\" to exit the program.");
                    
                    string choise = Console.ReadLine();
                    if (choise.ToLower() == "e")
                        break;
                    else
                        continue;
                }
                else
                {
                    Console.Clear();
                    
                    
                    
                    
                    pMethods.getTreeFromList(nodeList);
                    pMethods.setCodeToTheTree("", nodeList[0]);
                    Console.WriteLine("\n\n");







                    pMethods.PrintfLeafAndCodes(nodeList[0]);
                    pMethods.Binary();
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Compresion terminada");
                    Console.WriteLine("Cualquier tecla para continuar");
                    Console.WriteLine("Ingrese Exit para salir");
                    
                    string choise = Console.ReadLine();
                    if (choise.ToLower() == "Exit")
                        break;
                    else
                        continue;

                }
            }

        }
    }
}
