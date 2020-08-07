using System;
using System.Collections.Generic;

namespace SimpleHuffman
{
    struct WeightedLetter
    {
        public char symbol;
        public float weight;
    }

    class HuffmanNode
    {
        public List<char> letters;
        public float weight;
        public HuffmanNode leftBranch;
        public HuffmanNode rightBranch;

        /// <summary>
        /// Constructor for creating gathering node (union for leaves or other gathering nodes).
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        public HuffmanNode(HuffmanNode node1, HuffmanNode node2)
        {
            letters = GetLettersFrom2Nodes(node1, node2);
            weight = node1.weight + node2.weight;
            leftBranch = node1;
            rightBranch = node2;
        }

        /// <summary>
        /// Constructor for creating node as leaf.
        /// </summary>
        /// <param name="letter"></param>
        public HuffmanNode(WeightedLetter letter)
        {
            letters = new List<char> { letter.symbol };
            weight = letter.weight;
        }

        List<char> GetLettersFrom2Nodes(HuffmanNode node1, HuffmanNode node2)
        {
            var letters = new List<char>();
            letters.AddRange(node1.letters);
            letters.AddRange(node2.letters);
            return letters;
        }

        public Dictionary<char, bool[]> GetCharsDictionary()
        {
            var charCodes = new Dictionary<char, bool[]>();
            foreach (var letter in letters)
                charCodes.Add(letter, GetCharCode(letter, this, new List<bool>()).ToArray());
            return charCodes;
        }

        public List<bool> GetCharCode(char v, HuffmanNode node, List<bool> tempResult)
        {
            if (node.letters.Count == 1 && node.letters[0] == v)
                return tempResult;
            else if (node.leftBranch != null && node.leftBranch.letters.Contains(v))
            {
                tempResult.Add(false);
                return GetCharCode(v, node.leftBranch, tempResult);
            }
            else if (node.rightBranch != null && node.rightBranch.letters.Contains(v))
            {
                tempResult.Add(true);
                return GetCharCode(v, node.rightBranch, tempResult);
            }
            else throw new Exception();
        }

        public string GetLettersAsString()
        {
            var str = string.Empty;
            foreach (var ch in letters)
                str += ch;
            return str;
        }
    }
}

