using System.Collections.Generic;
using System.IO;

namespace GeneticSearch
{
    class SearchCommand : ICommand
    {
        public string Name => "search";

        public void Execute(List<GeneticData> proteins, string[] parameters, StreamWriter sw)
        {
            string searchSeq = AminoAcidCodec.RLDecoding(parameters[0]);

            sw.WriteLine("organism\t\t\tprotein ");
            bool found = false;
            foreach (var p in proteins)
            {
                if (p.amino_acids.Contains(searchSeq))
                {
                    sw.WriteLine($"{p.organism}\t\t{p.protein}");
                    found = true;
                }
            }
            if (!found) sw.WriteLine("NOT FOUND");
        }
    }
}
