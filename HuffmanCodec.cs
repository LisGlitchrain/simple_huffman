using System.Collections;
using System.Collections.Generic;

namespace SimpleHuffman
{
    class HuffmanCodec
    {
        HuffmanNode rootOfTree;

        public HuffmanCodec(WeightedLetter[] letters) =>
            rootOfTree = GetHuffmanTreeFrom(letters);

        HuffmanNode GetHuffmanTreeFrom(WeightedLetter[] letters)
        {
            var nodes = ConvertLettersToLeafNodes(letters);
            return ConstructHuffmanTreeFromLeafs(new List<HuffmanNode>(nodes));
        }

        HuffmanNode[] ConvertLettersToLeafNodes(WeightedLetter[] letters)
        {
            HuffmanNode[] trees = new HuffmanNode[letters.Length];
            for (var i = 0; i < trees.Length; i++)
                trees[i] = new HuffmanNode(letters[i]);
            return trees;
        }

        HuffmanNode ConstructHuffmanTreeFromLeafs(List<HuffmanNode> nodes)
        {
            while (nodes.Count > 1)
            {
                var node1 = FindNodeWithMinWeight(nodes);
                var node2 = FindNodeMinWeightExceptNode(nodes, node1);
                var newNode = new HuffmanNode(node1, node2);
                nodes.Add(newNode);
                nodes.Remove(node1);
                nodes.Remove(node2);
            }
            return nodes[0];
        }

        HuffmanNode FindNodeWithMinWeight(List<HuffmanNode> nodes)
        {
            var min = nodes[0];
            for (int i = 1; i < nodes.Count; i++)
                if (nodes[i].weight < min.weight) min = nodes[i];
            return min;
        }

        HuffmanNode FindNodeMinWeightExceptNode(List<HuffmanNode> nodes, HuffmanNode nodeToExcept)
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

        public List<List<string>> GetTreeAsStrings()
        {
            var result = new List<List<string>>();
            var listOfNodesToProcess = new Queue<HuffmanNode>();
            var listOfNextNodes = new Queue<HuffmanNode>();
            listOfNodesToProcess.Enqueue(rootOfTree);
            var depth = 0;

            while(listOfNodesToProcess.Count != 0)
            {
                result.Add(new List<string>());
                while(listOfNodesToProcess.Count != 0)
                {
                    result[depth].Add(listOfNodesToProcess.Peek().GetLettersAsString());
                    var childrenList = GetNodeChildren(listOfNodesToProcess.Dequeue());
                    foreach(var childNode in childrenList)
                        listOfNextNodes.Enqueue(childNode);
                }
                depth++;
                listOfNodesToProcess = listOfNextNodes;
                listOfNextNodes = new Queue<HuffmanNode>();
            }
 
            return result;
        }

        List<HuffmanNode> GetNodeChildren(HuffmanNode node)
        {
            var result = new List<HuffmanNode>();
            if (node.leftBranch != null) result.Add(node.leftBranch);
            if (node.rightBranch != null) result.Add(node.rightBranch);
            return result;
        }
    }
}


