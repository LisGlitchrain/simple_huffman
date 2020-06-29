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

            var E = new Letter() { symbol = 'E', weight = 12.7f};
            var T = new Letter() { symbol = 'T', weight = 9.06f};
            var A = new Letter() { symbol = 'A', weight = 8.17f};
            var O = new Letter() { symbol = 'O', weight = 7.51f};
            var I = new Letter() { symbol = 'I', weight = 6.97f};
            var N = new Letter() { symbol = 'N', weight = 6.75f};
            var S = new Letter() { symbol = 'S', weight = 6.33f};
            var H = new Letter() { symbol = 'H', weight = 6.09f};
            var R = new Letter() { symbol = 'R', weight = 5.99f};
            var D = new Letter() { symbol = 'D', weight = 4.25f};
            var L = new Letter() { symbol = 'L', weight = 4.03f};
            var C = new Letter() { symbol = 'C', weight = 2.78f};
            var U = new Letter() { symbol = 'U', weight = 2.76f};
            var M = new Letter() { symbol = 'M', weight = 2.41f};
            var W = new Letter() { symbol = 'W', weight = 2.36f};
            var F = new Letter() { symbol = 'F', weight = 2.23f};
            var G = new Letter() { symbol = 'G', weight = 2.02f};
            var Y = new Letter() { symbol = 'Y', weight = 1.97f};
            var P = new Letter() { symbol = 'P', weight = 1.93f};
            var B = new Letter() { symbol = 'B', weight = 1.49f};
            var V = new Letter() { symbol = 'V', weight = 0.98f};
            var K = new Letter() { symbol = 'K', weight = 0.77f};
            var X = new Letter() { symbol = 'X', weight = 0.15f};
            var J = new Letter() { symbol = 'J', weight = 0.15f};
            var Q = new Letter() { symbol = 'Q', weight = 0.1f};
            var Z = new Letter() { symbol = 'Z', weight = 0.05F};

            Letter[] letters = new Letter[] {E,T,A,O,I,N,S,H,R,D,L,C,U,M,W,F,G,Y,P,B,V,K,X,J,Q,Z };
            var huffmanCodec = new HuffmanCodec(letters);

            var generator = new RandomWeightedStringGenerator(letters);
            var str = generator.GenerateStringWithLetterFreqiencies(100);

            File.WriteAllText(pathUncompressed, str);
            var encoded = huffmanCodec.Encode(str);
            SaveBitArrayToFile(encoded, pathCompressed);

            var toBeDecoded = ReadBitArrayFromFile(pathCompressed);
            var decoded = huffmanCodec.Decode(toBeDecoded);

            Console.WriteLine($"Decoded:  {decoded}");
            Console.WriteLine();
            Console.WriteLine($"Oririnal: {str}");

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
