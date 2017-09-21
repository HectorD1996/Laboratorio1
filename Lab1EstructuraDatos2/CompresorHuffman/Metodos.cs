using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace CompresorHuffman
{
    public class Metodos
    {
        int CountBinary = 0;
        string codeString = "";
        List<byte> BytesToWrite = new List<byte>();
        BinaryWriter bw = new BinaryWriter(File.Create("output.comp"));
        public void Binary()
        {
            string code = codeString;
            string adder = "";
            foreach (char binary1 in code)
            {
                CountBinary++;
                if (CountBinary != 9)
                {
                    adder += binary1;
                }
                else
                {
                    byte b1 = (byte)Convert.ToInt32(adder);

                    BytesToWrite.Add(b1);
                    adder = "" + binary1;
                    CountBinary = 1;
                }

            }

            int j = adder.Length;
            for (int i = 0; i < 8 - j; i++)
            {
                adder += "0";
            }

            if (adder != "")
            {

                byte c1 = (byte)Convert.ToInt32(adder);
                BytesToWrite.Add(c1);

            }


            foreach (byte bytewrite in BytesToWrite)
            {

                bw.Write(bytewrite);
            }
            bw.Flush();
            
        }
        //  Creates a Node List that reading the characters on the file.
        public List<HuffmanNode> getListFromFile()
        {
            List<HuffmanNode> nodeList = new List<HuffmanNode>();  // Node List.

            
            Console.WriteLine("Example file: \"a.txt\"\n");
            
            Console.Write("Enter the path of the file: ");
            String filename = Console.ReadLine();
            try
            {
                // Creating a new unique node that reading from the file.
                // If it is the same character, increase the frequency of the value. It is possiple with "frequencyIncreas()" method.
                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                for (int i = 0; i < stream.Length; i++)
                {
                    string read = Convert.ToChar(stream.ReadByte()).ToString();
                    if (nodeList.Exists(x => x.symbol == read)) // Checking the value that have you created before?
                        nodeList[nodeList.FindIndex(y => y.symbol == read)].frequencyIncrease(); // If is yes, find the index of the Node and increase the frequency of the Node.
                    else
                        nodeList.Add(new HuffmanNode(read));   // If is no, create a new node and add to the List of Nodes
                }
                nodeList.Sort();   // Sort the Nodes on the List according to their frequency value.
                return nodeList;
            }
            catch (Exception)
            {
                return null;
            }

        }


        //  Creates a Tree according to Nodes(frequency, symbol)
        public void getTreeFromList(List<HuffmanNode> nodeList)
        {
            while (nodeList.Count > 1)  // 1 because a tree need 2 leaf to make a new parent.
            {
                HuffmanNode node1 = nodeList[0];    // Get the node of the first index of List,
                nodeList.RemoveAt(0);               // and delete it.
                HuffmanNode node2 = nodeList[0];    // Get the node of the first index of List,
                nodeList.RemoveAt(0);               // and delete it.
                nodeList.Add(new HuffmanNode(node1, node2));    // Sending the constructor to make a new Node from this nodes.
                nodeList.Sort();        // and sort it again according to frequency.
            }
        }


        // Setting the codes of the nodes of tree. Recursive method.
        public void setCodeToTheTree(string code, HuffmanNode Nodes)
        {
            if (Nodes == null)
                return;
            if (Nodes.leftTree == null && Nodes.rightTree == null)
            {
                Nodes.code = code;
                return;
            }
            setCodeToTheTree(code + "0", Nodes.leftTree);
            setCodeToTheTree(code + "1", Nodes.rightTree);
        }


        // Printing all Tree! Recursive method.
        public void PrintTree(int level, HuffmanNode node)
        {
            if (node == null)
                return;
            for (int i = 0; i < level; i++)
            {
                Console.Write("\t");
            }
            Console.Write("[" + node.symbol + "]");
            
            Console.WriteLine("(" + node.code + ")");
            
            PrintTree(level + 1, node.rightTree);
            PrintTree(level + 1, node.leftTree);
        }


        //  Printing the Node's information on the nodeList
        public void PrintInformation(List<HuffmanNode> nodeList)
        {
            foreach (var item in nodeList)
                Console.WriteLine("Symbol : {0} - Frequency : {1}", item.symbol, item.frequency);
        }


        // Printing the symbols and codes of the Nodes on the tree.
        public void PrintfLeafAndCodes(HuffmanNode nodeList)
        {
            if (nodeList == null)
                return;
            if (nodeList.leftTree == null && nodeList.rightTree == null)
            {

                Console.WriteLine("Symbol : {0} -  Code : {1}", nodeList.symbol, nodeList.code);
                bw.Write(nodeList.symbol);
                bw.Write(nodeList.code);

                codeString += nodeList.code;
                //Binary(nodeList.code);
                return;
            }
            PrintfLeafAndCodes(nodeList.leftTree);
            PrintfLeafAndCodes(nodeList.rightTree);
        }
    }
}
