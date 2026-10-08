using System.Text;

namespace GeneticSearch
{
    static class AminoAcidCodec
    {
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
    }
}
