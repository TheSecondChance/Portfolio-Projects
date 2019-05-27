using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimal_encoding__Huffman_
{
    class Node
    {
        public char character;
        public double probability;
        public Node left;
        public Node right;

        public Node(double prob)
        {
            probability = prob;
        }

        public Node(char ch, double prob)
        {
            character = ch;
            probability = prob;
        }

        public void Visit(ref string code, ref Dictionary<char, string> codes)
        {
            if (left != null)
            {
                code += "0";
                left.Visit(ref code, ref codes);
                code = code.Substring(0, code.Length - 1);
            }
            
            if (right != null)
            {
                code += "1";
                right.Visit(ref code, ref codes);
                code = code.Substring(0, code.Length - 1);
            }
            
            if (left == null && right == null)
            {
                Console.WriteLine("{0}: {1} : {2}", character, probability, code);
                codes.Add(character, code);
            }
        }
    }
    class HuffmanEncoding
    {
        private Node root;
        
        private void createHuffmanTree(Dictionary<char, double> probs)
        {
            List<Node> nodesList = new List<Node>();

            foreach (KeyValuePair<char, double> item in probs)
                nodesList.Add(new Node(item.Key, item.Value));

            while (nodesList.Count != 1)
            {
                Node[] leastNodes = getLeastNodes(ref nodesList);
                Node pair = new Node(leastNodes[0].probability + leastNodes[1].probability);
                pair.right = leastNodes[0];
                pair.left = leastNodes[1];

                nodesList.Add(pair);
            }

            root = nodesList.ElementAt(0);
        }

        public Dictionary<char, string> getCodeWords(Dictionary<char, double> probabilities)
        {
            createHuffmanTree(probabilities);

            Dictionary<char, string> codes = new Dictionary<char, string>();
            string code = "";

            root.Visit(ref code, ref codes);

            Dictionary<char, string> sortedCodes = new Dictionary<char, string>();

            foreach (KeyValuePair<char, double> item in probabilities)
            {
                sortedCodes.Add(item.Key, codes[item.Key]);
            }

            return sortedCodes;
        }

        private Node[] getLeastNodes(ref List<Node> nodes)
        {
            Node[] leastNodes = new Node[2];
            int leastIndex = 0;
            leastNodes[0] = nodes.ElementAt(0);

            for (int i = 1; i < nodes.Count; i++)
            {
                if (nodes.ElementAt(i).probability <= leastNodes[0].probability)
                {
                    leastNodes[0] = nodes.ElementAt(i);
                    leastIndex = i;
                }
            }
            nodes.RemoveAt(leastIndex);

            leastIndex = 0;
            leastNodes[1] = nodes.ElementAt(0);
            for (int i = 1; i < nodes.Count; i++)
            {
                if (nodes.ElementAt(i).probability <= leastNodes[1].probability)
                {
                    leastNodes[1] = nodes.ElementAt(i);
                    leastIndex = i;
                }
            }
            nodes.RemoveAt(leastIndex);

            return leastNodes;
        }
    }
}
