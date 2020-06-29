using System;

namespace SimpleHuffman
{
    class RandomWeightedStringGenerator
    {
        float[] thresholds;
        Random rand;
        Letter[] letters;

        public RandomWeightedStringGenerator(Letter[] letters)
        {
            this.letters = letters;
            float sumWeight = 0;
            foreach (var letter in letters)
                sumWeight += letter.weight;
            thresholds = new float[letters.Length + 1];
            for (var i = 1; i < thresholds.Length; i++)
                thresholds[i] = thresholds[i - 1] + letters[i - 1].weight / sumWeight;
            rand = new Random();
        }

        public string GenerateStringWithLetterFreqiencies(int length)
        {
            var str = string.Empty;
            var letterGenerator = new RandomWeightedStringGenerator(letters);
            for (var i = 0; i < length; i++)
                str += (letters[letterGenerator.GetLetterIdAccordingToFrequency()].symbol);
            return str;
        }

        int GetLetterIdAccordingToFrequency()
        {
            var randomReal = rand.NextDouble();
            for (var i = 0; i < thresholds.Length; i++)
                if (randomReal < thresholds[i]) return i - 1;
            return thresholds.Length - 2;
        }

        public void PrintThresholds()
        {
            foreach (var threshold in thresholds)
                Console.WriteLine(threshold);
        }
    }
}
