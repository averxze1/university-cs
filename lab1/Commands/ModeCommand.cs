using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeneticSearch
{
    /// <summary>
    /// Операция mode: находит наиболее часто встречающуюся аминокислоту
    /// в цепочке указанного белка (при равенстве - первую по алфавиту).
    /// </summary>
    class ModeCommand : ICommand
    {
        public string Name => "mode";

        public void Execute(List<GeneticData> proteins, string[] parameters, StreamWriter sw)
        {
            string pName = parameters[0];
            var prot = proteins.FirstOrDefault(p => p.protein == pName);

            sw.WriteLine("amino-acid occurs: ");

            if (prot.protein == null)
            {
                sw.WriteLine("MISSING: " + pName);
                return;
            }

            var counts = new Dictionary<char, int>();
            foreach (char c in prot.amino_acids)
            {
                if (counts.ContainsKey(c)) counts[c]++;
                else counts[c] = 1;
            }

            int maxVal = counts.Values.Max();
            char finalChar = counts.Where(kv => kv.Value == maxVal)
                                    .Select(kv => kv.Key)
                                    .OrderBy(c => c)
                                    .First();

            sw.WriteLine($"{finalChar}          {maxVal}");
        }
    }
}
