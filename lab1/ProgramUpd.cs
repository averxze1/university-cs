using System;
using System.Collections.Generic;
using System.IO;

namespace GeneticSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            string sequencesFile = "Data/sequences.0.txt";
            string commandsFile = "Data/commands.0.txt";
            string outputFile = "FinalGenedata.txt";
            string myName = "Соколовский Марат";

            List<ICommand> availableCommands = new List<ICommand>
            {
                new SearchCommand(),
                new DiffCommand(),
                new ModeCommand()
            };

            Dictionary<string, ICommand> commandsByName = new Dictionary<string, ICommand>();
            foreach (var cmd in availableCommands)
                commandsByName[cmd.Name] = cmd;

            try
            {
                List<GeneticData> proteins = LoadProteins(sequencesFile);
                if (proteins == null) return;

                using (StreamWriter sw = new StreamWriter(outputFile))
                {
                    sw.WriteLine(myName);
                    sw.WriteLine("Genetic Searching");

                    if (!File.Exists(commandsFile))
                    {
                        Console.WriteLine($"Файл {commandsFile} не найден!");
                        return;
                    }

                    RunCommands(commandsFile, commandsByName, proteins, sw);
                }

                Console.WriteLine("Файл genedata.txt успешно создан!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static List<GeneticData> LoadProteins(string sequencesFile)
        {
            if (!File.Exists(sequencesFile))
            {
                Console.WriteLine($"Файл {sequencesFile} не найден!");
                return null;
            }

            List<GeneticData> proteins = new List<GeneticData>();
            using (StreamReader sr = new StreamReader(sequencesFile))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('\t');
                    if (parts.Length >= 3)
                    {
                        proteins.Add(new GeneticData
                        {
                            protein = parts[0],
                            organism = parts[1],
                            amino_acids = AminoAcidCodec.RLDecoding(parts[2])
                        });
                    }
                }
            }
            return proteins;
        }

        static void RunCommands(string commandsFile, Dictionary<string, ICommand> commandsByName,
                                 List<GeneticData> proteins, StreamWriter sw)
        {
            using (StreamReader sr = new StreamReader(commandsFile))
            {
                int cmdIdx = 1;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('\t');
                    string cmdName = parts[0].ToLower();
                    string opNum = cmdIdx.ToString("D3");

                    sw.WriteLine("--------------------------------------------------------------------------");
                    sw.WriteLine($"{opNum}   {parts[0]}   {(parts.Length > 1 ? parts[1] : "")} {(parts.Length > 2 ? parts[2] : "")}");

                    ICommand command;
                    if (commandsByName.TryGetValue(cmdName, out command))
                    {
                        string[] parameters;
                        if (parts.Length > 1)
                        {
                            parameters = new string[parts.Length - 1];
                            Array.Copy(parts, 1, parameters, 0, parameters.Length);
                        }
                        else
                        {
                            parameters = Array.Empty<string>();
                        }

                        command.Execute(proteins, parameters, sw);
                    }

                    cmdIdx++;
                }
            }
        }
    }
}
