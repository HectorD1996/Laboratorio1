using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Laboratorio1EstructuraDatos2
{
    public class ProcessMethods
    {
        int CountBinary = 0;
        string codeString = "";
        List<byte> BytesToWrite = new List<byte>();
        BinaryWriter bw = new BinaryWriter(File.Create("output.comp"));
        Dictionary<char, string> TableDecompress = new Dictionary<char, string>();

        //DESCOMPRIMIR NO FUNCIONA
        public void Descomprimir(string FilenamesDes)
        {
            string Read;
            BinaryReader st = new BinaryReader(File.Open(FilenamesDes, FileMode.Open));
            do
            {
                Read = st.ReadByte().ToString();
                if (Read != null)
                {


                    // char SymbolDecompress;
                    //string CodeDecompress = "";
                    //SymbolDecompress = Read[0];
                    //if (Read.Length < 2)
                    //{
                    // for (int i = 1; i < Read.Length; i++)
                    // {
                    // CodeDecompress += Convert.ToString(Convert.ToByte(Read[i]));
                    // }
                    //  }
                    //TableDecompress.Add(SymbolDecompress, CodeDecompress);
                }

            } while (Read != "END");
        }

        public void Binary()
        {
            string code = codeString;
            string adder = "";
           
            bw.Write("\r\n");
            foreach (char binary1 in code)
            {
                CountBinary++;
                if (CountBinary != 9)
                {
                    adder += binary1;
                }
                else
                {
                    byte b1 = Convert.ToByte(adder, 2);
                 

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

                byte c1 = Convert.ToByte(adder, 2);
                
                BytesToWrite.Add(c1);

            }


            foreach (byte bytewrite in BytesToWrite)
            {

                bw.Write(bytewrite);
            }
            bw.Flush();
            bw.Close();
        }
        
        public List<HuffmanNode> getListFromFile(string Paths1)
        {
            List<HuffmanNode> nodeList = new List<HuffmanNode>();  

           
            
            
            String filename = Paths1;
            
            try
            {
                
                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                for (int i = 0; i < stream.Length; i++)
                {
                    string read = Convert.ToChar(stream.ReadByte()).ToString();
                    if (nodeList.Exists(x => x.symbol == read)) 
                        nodeList[nodeList.FindIndex(y => y.symbol == read)].frequencyIncrease(); 
                    else
                        nodeList.Add(new HuffmanNode(read));   
                }
                nodeList.Sort();   
                return nodeList;
            }
            catch (Exception)
            {
                return null;
            }

        }


        
        public void getTreeFromList(List<HuffmanNode> nodeList)
        {
            while (nodeList.Count > 1)  
            {
                HuffmanNode node1 = nodeList[0];    
                nodeList.RemoveAt(0);               
                HuffmanNode node2 = nodeList[0];    
                nodeList.RemoveAt(0);              
                nodeList.Add(new HuffmanNode(node1, node2));    
                nodeList.Sort();        
            }
        }


       
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


        
        


       
        


        
        public void PrintfLeafAndCodes(HuffmanNode nodeList)
        {
            if (nodeList == null)
                return;
            if (nodeList.leftTree == null && nodeList.rightTree == null)
            {

                
                bw.Write((byte)Convert.ToChar(nodeList.symbol));
                string codeforTable = nodeList.code;
                int k = codeforTable.Length;
                while (codeforTable.Length > 8)
                {
                    bw.Write(Convert.ToByte(codeforTable.Substring(0, 8), 2));
                    codeforTable = codeforTable.Substring(8, codeforTable.Length - 8);
                }
                for (int i = 0; i < 8 - k; i++)
                {
                    codeforTable = "0" + codeforTable;
                }
                bw.Write(Convert.ToByte(codeforTable, 2));
                bw.Write("\r\n");
                codeString += nodeList.code;
               
                return;
            }
            PrintfLeafAndCodes(nodeList.leftTree);
            PrintfLeafAndCodes(nodeList.rightTree);
        }
    }
}
