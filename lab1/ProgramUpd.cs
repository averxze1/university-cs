using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeneticSearch
{
    class Program
    {
        struct GeneticData
        {
            public string protein;
            public string organism;
            public string amino_acids;
        }

        static string RLDecoding(string amino_acids)
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

        static void Main(string[] args)
        {
            // --- НАСТРОЙКИ ФАЙЛОВ ---
            string sequencesFile = "sequences.0.txt"; 
            string commandsFile = "commands.0.txt";
            string outputFile = "genedata.txt";
            string myName = "Соколовский Марат"; 
            // ------------------------

            try
            {
                List<GeneticData> proteins = new List<GeneticData>();
                if (!File.Exists(sequencesFile)) { Console.WriteLine($"Файл {sequencesFile} не найден!"); return; }

                using (StreamReader sr = new StreamReader(sequencesFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        string[] parts = line.Split('\t');
                        if (parts.Length >= 3)
                        {
                            proteins.Add(new GeneticData {
                                protein = parts[0],
                                organism = parts[1],
                                amino_acids = RLDecoding(parts[2])
                            });
                        }
                    }
                }

                using (StreamWriter sw = new StreamWriter(outputFile))
                {
                    sw.WriteLine(myName);
                    sw.WriteLine("Genetic Searching");

                    if (!File.Exists(commandsFile)) { Console.WriteLine($"Файл {commandsFile} не найден!"); return; }

                    using (StreamReader sr = new StreamReader(commandsFile))
                    {
                        int cmdIdx = 1;
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if (string.IsNullOrWhiteSpace(line)) continue;

                            string[] parts = line.Split('\t');
                            string cmd = parts[0].ToLower();
                            string opNum = cmdIdx.ToString("D3");

                            sw.WriteLine("--------------------------------------------------------------------------");
                            sw.WriteLine($"{opNum}   {parts[0]}   {(parts.Length > 1 ? parts[1] : "")} {(parts.Length > 2 ? parts[2] : "")}");

                            if (cmd == "search")
                            {
                                string searchSeq = RLDecoding(parts[1]);
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
                            else if (cmd == "diff")
                            {
                                string p1Name = parts[1];
                                string p2Name = parts[2];
                                var prot1 = proteins.FirstOrDefault(p => p.protein == p1Name);
                                var prot2 = proteins.FirstOrDefault(p => p.protein == p2Name);

                                sw.WriteLine("amino-acids difference: ");
                                if (prot1.protein == null || prot2.protein == null)
                                {
                                    string missing = "";
                                    if (prot1.protein == null) missing += (missing == "" ? "" : ", ") + p1Name;
                                    if (prot2.protein == null) missing += (missing == "" ? "" : ", ") + p2Name;
                                    sw.WriteLine("MISSING: " + missing);
                                }
                                else
                                {
                                    int diff = 0;
                                    int maxLen = Math.Max(prot1.amino_acids.Length, prot2.amino_acids.Length);
                                    for (int i = 0; i < maxLen; i++)
                                    {
                                        if (i >= prot1.amino_acids.Length || i >= prot2.amino_acids.Length || prot1.amino_acids[i] != prot2.amino_acids[i])
                                            diff++;
                                    }
                                    sw.WriteLine(diff);
                                }
                            }
                            else if (cmd == "mode")
                            {
                                string pName = parts[1];
                                var prot = proteins.FirstOrDefault(p => p.protein == pName);

                                sw.WriteLine("amino-acid occurs: ");
                                if (prot.protein == null)
                                {
                                    sw.WriteLine("MISSING: " + pName);
                                }
                                else
                                {
                                    var counts = new Dictionary<char, int>();
                                    foreach (char c in prot.amino_acids)
                                    {
                                        if (counts.ContainsKey(c)) counts[c]++; else counts[c] = 1;
                                    }
                                    int maxVal = counts.Values.Max();
                                    char finalChar = counts.Where(kv => kv.Value == maxVal).Select(kv => kv.Key).OrderBy(c => c).First();
                                    sw.WriteLine($"{finalChar}          {maxVal}");
                                }
                            }
                            cmdIdx++;
                        }
                    }
                }
                Console.WriteLine("Файл genedata.txt успешно создан!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}
