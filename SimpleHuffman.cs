using System;
using System.Collections;
using System.IO;

namespace SimpleHuffman
{
    class SimpleHuffman
    {
        static void Main()
        {
            var pathUncompressed = @"E:\TestUncompressed.txt";
            var pathCompressed = @"E:\TestCompressed.txt";

            var E = new WeightedLetter() { symbol = 'E', weight = 12.7f};
            var T = new WeightedLetter() { symbol = 'T', weight = 9.06f};
            var A = new WeightedLetter() { symbol = 'A', weight = 8.17f};
            var O = new WeightedLetter() { symbol = 'O', weight = 7.51f};
            var I = new WeightedLetter() { symbol = 'I', weight = 6.97f};
            var N = new WeightedLetter() { symbol = 'N', weight = 6.75f};
            var S = new WeightedLetter() { symbol = 'S', weight = 6.33f};
            var H = new WeightedLetter() { symbol = 'H', weight = 6.09f};
            var R = new WeightedLetter() { symbol = 'R', weight = 5.99f};
            var D = new WeightedLetter() { symbol = 'D', weight = 4.25f};
            var L = new WeightedLetter() { symbol = 'L', weight = 4.03f};
            var C = new WeightedLetter() { symbol = 'C', weight = 2.78f};
            var U = new WeightedLetter() { symbol = 'U', weight = 2.76f};
            var M = new WeightedLetter() { symbol = 'M', weight = 2.41f};
            var W = new WeightedLetter() { symbol = 'W', weight = 2.36f};
            var F = new WeightedLetter() { symbol = 'F', weight = 2.23f};
            var G = new WeightedLetter() { symbol = 'G', weight = 2.02f};
            var Y = new WeightedLetter() { symbol = 'Y', weight = 1.97f};
            var P = new WeightedLetter() { symbol = 'P', weight = 1.93f};
            var B = new WeightedLetter() { symbol = 'B', weight = 1.49f};
            var V = new WeightedLetter() { symbol = 'V', weight = 0.98f};
            var K = new WeightedLetter() { symbol = 'K', weight = 0.77f};
            var X = new WeightedLetter() { symbol = 'X', weight = 0.15f};
            var J = new WeightedLetter() { symbol = 'J', weight = 0.15f};
            var Q = new WeightedLetter() { symbol = 'Q', weight = 0.1f};
            var Z = new WeightedLetter() { symbol = 'Z', weight = 0.05F};

            var letters = new WeightedLetter[] {E,T,A,O,I,N,S,H,R,D,L,C,U,M,W,F,G,Y,P,B,V,K,X,J,Q,Z };
            var huffmanCodec = new HuffmanCodec(letters);

            var generator = new RandomWeightedStringGenerator(letters);
            var str = generator.GenerateStringWithLetterFreqiencies(100);

            File.WriteAllText(pathUncompressed, str);
            var encoded = huffmanCodec.Encode(str);
            SaveBitArrayToFile(encoded, pathCompressed);

            var toBeDecoded = ReadBitArrayFromFile(pathCompressed);
            var decoded = huffmanCodec.Decode(toBeDecoded);

            var treeToPrint = huffmanCodec.GetTreeAsStrings();

            for(var i = 0; i < treeToPrint.Count; i++)
            {
                for (var j = 0; j < treeToPrint[i].Count; j++)
                    Console.Write($"{treeToPrint[i][j]}  ");
                Console.WriteLine();
            }

            Console.WriteLine($"Decoded:  {decoded}");
            Console.WriteLine();
            Console.WriteLine($"Oririnal: {str}");

            Console.ReadKey();
        }

        static void SaveBitArrayToFile(BitArray bits, string path)
        {
            byte[] bytes = new byte[(int)Math.Ceiling((double)bits.Length / 8)];
            bits.CopyTo(bytes, 0);
            File.WriteAllBytes(path, bytes);
        }

        static BitArray ReadBitArrayFromFile(string path)
        {
            var bytesRead = File.ReadAllBytes(path);
            return new BitArray(bytesRead);
        }
    }
}
