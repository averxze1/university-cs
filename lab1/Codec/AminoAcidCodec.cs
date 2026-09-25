using System.Text;

namespace GeneticSearch
{
    /// <summary>
    /// Кодирование и раскодирование цепочек аминокислот алгоритмом
    /// Run-Length Encoding (RLE). Серии из 1-2 одинаковых букв не сжимаются.
    /// </summary>
    static class AminoAcidCodec
    {
        /// <summary>
        /// Раскодирует строку вида "8ATA3TCGC..." в полную цепочку аминокислот.
        /// </summary>
        public static string RLDecoding(string amino_acids)
        {
            if (string.IsNullOrEmpty(amino_acids)) return "";

            StringBuilder decoded = new StringBuilder();
            for (int i = 0; i < amino_acids.Length; i++)
            {
                if (char.IsDigit(amino_acids[i]))
                {
                    int count = amino_acids[i] - '0';
                    if (i + 1 < amino_acids.Length)
                    {
                        char letter = amino_acids[i + 1];
                        for (int j = 0; j < count; j++) decoded.Append(letter);
                        i++;
                    }
                }
                else
                {
                    decoded.Append(amino_acids[i]);
                }
            }
            return decoded.ToString();
        }

        /// <summary>
        /// Кодирует полную цепочку аминокислот алгоритмом RLE. Серии длиной
        /// 1 или 2 одинаковых символа записываются как есть, без цифры-счётчика.
        /// </summary>
        public static string RLEncoding(string amino_acids)
        {
            if (string.IsNullOrEmpty(amino_acids)) return "";

            StringBuilder encoded = new StringBuilder();
            int i = 0;
            while (i < amino_acids.Length)
            {
                char current = amino_acids[i];
                int count = 1;
                while (i + count < amino_acids.Length && amino_acids[i + count] == current)
                    count++;

                if (count >= 3)
                    encoded.Append(count).Append(current);
                else
                    encoded.Append(current, count);

                i += count;
            }
            return encoded.ToString();
        }
    }
}
