using System.Collections;
using System.Collections.Generic;

namespace SimpleHuffman
{
    class HuffmanCodec
    {
        HuffmanNode rootOfTree;

        public HuffmanCodec(Letter[] letters) =>
            rootOfTree = GetHuffmanTreeFrom(letters);

        HuffmanNode GetHuffmanTreeFrom(Letter[] letters)
        {
            var nodes = ConvertLettersToNodes(letters);
            return ConstructHuffmanTree(new List<HuffmanNode>(nodes));
        }

        HuffmanNode[] ConvertLettersToNodes(Letter[] letters)
        {
            HuffmanNode[] trees = new HuffmanNode[letters.Length];
            for (var i = 0; i < trees.Length; i++)
                trees[i] = new HuffmanNode(letters[i]);
            return trees;
        }

        HuffmanNode ConstructHuffmanTree(List<HuffmanNode> nodes)
        {
            while (nodes.Count > 1)
            {
                var node1 = FindMin(nodes);
                var node2 = FindMinExcept(nodes, node1);
                var newNode = new HuffmanNode(node1, node2);
                nodes.Add(newNode);
                nodes.Remove(node1);
                nodes.Remove(node2);
            }
            return nodes[0];
        }

        HuffmanNode FindMin(List<HuffmanNode> nodes)
        {
            var min = nodes[0];
            for (int i = 1; i < nodes.Count; i++)
                if (nodes[i].weight < min.weight) min = nodes[i];
            return min;
        }

        HuffmanNode FindMinExcept(List<HuffmanNode> nodes, HuffmanNode nodeToExcept)
        {
            HuffmanNode min = nodes[0];
            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] != nodeToExcept)
                {
                    min = nodes[i];
                    break;
                }
            }
            for (int i = 1; i < nodes.Count; i++)
            {
                if (nodes[i].weight < min.weight && nodes[i] != nodeToExcept) min = nodes[i];
            }
            return min;
        }

        public BitArray Encode(string str)
        {
            Dictionary<char, bool[]> charCodes = rootOfTree.GetCharsDictionary();

            var tempResult = new List<bool>();
            for (var i = 0; i < str.Length; i++)
            {
                var tmpChar = charCodes[str[i]];
                foreach (var bit in tmpChar)
                    tempResult.Add(bit);
            }
            var result = new BitArray(tempResult.Count);
            for (var i = 0; i < result.Length; i++)
                result[i] = tempResult[i];
            return result;
        }

        public string Decode(BitArray bits)
        {
            var result = string.Empty;
            HuffmanNode currentNode = rootOfTree;
            foreach (bool bit in bits)
            {
                if (bit) currentNode = currentNode.rightBranch;
                else currentNode = currentNode.leftBranch;
                if (currentNode.letters.Count == 1)
                {
                    result += currentNode.letters[0];
                    currentNode = rootOfTree;
                }
            }
            return result;
        }
    }
}


