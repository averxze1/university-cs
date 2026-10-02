using System;

namespace GeneticSearch
{
    static class DecodingTests
    {
        public static void RunAll()
        {
            // Тест 1
            string result1 = AminoAcidCodec.RLDecoding("MLQSIIK");
            Console.WriteLine("Тест 1: " + result1);

            // Тест 2
            string result2 = AminoAcidCodec.RLDecoding("4A");
            Console.WriteLine("Тест 2: " + result2);

            // Тест 3
            string result3 = AminoAcidCodec.RLDecoding("AAGAT4AGTG");
            Console.WriteLine("Тест 3: " + result3);

            // Тест 4
            string result4 = AminoAcidCodec.RLDecoding("8ATA3TCGC4TC5A");
            Console.WriteLine("Тест 4: " + result4);

            // Тест 5
            string result5 = AminoAcidCodec.RLDecoding("FK3I");
            Console.WriteLine("Тест 5: " + result5);
        }
    }
}