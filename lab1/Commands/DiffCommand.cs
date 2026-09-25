using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeneticSearch
{
    /// <summary>
    /// Операция diff: считает, в скольких позициях различаются цепочки
    /// аминокислот двух указанных белков.
    /// </summary>
    class DiffCommand : ICommand
    {
        public string Name => "diff";

        public void Execute(List<GeneticData> proteins, string[] parameters, StreamWriter sw)
        {
            string p1Name = parameters[0];
            string p2Name = parameters[1];

            var prot1 = proteins.FirstOrDefault(p => p.protein == p1Name);
            var prot2 = proteins.FirstOrDefault(p => p.protein == p2Name);

            sw.WriteLine("amino-acids difference: ");

            if (prot1.protein == null || prot2.protein == null)
            {
                string missing = "";
                if (prot1.protein == null) missing += (missing == "" ? "" : ", ") + p1Name;
                if (prot2.protein == null) missing += (missing == "" ? "" : ", ") + p2Name;
                sw.WriteLine("MISSING: " + missing);
                return;
            }

            int diff = 0;
            int maxLen = Math.Max(prot1.amino_acids.Length, prot2.amino_acids.Length);
            for (int i = 0; i < maxLen; i++)
            {
                if (i >= prot1.amino_acids.Length || i >= prot2.amino_acids.Length ||
                    prot1.amino_acids[i] != prot2.amino_acids[i])
                    diff++;
            }
            sw.WriteLine(diff);
        }
    }
}
