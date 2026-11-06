using System.Collections.Generic;
using System.Linq;

namespace TextTokenizer
{
    class Concordance
    {
        private readonly Dictionary<string, WordInfo> data = new Dictionary<string, WordInfo>();

        public void Build(Text text)
        {
            for (int i = 0; i < text.Sentences.Count; i++)
            {
                int lineNumber = i + 1;
                Sentence sentence = text.Sentences[i];

                foreach (Word word in sentence.Words)
                {
                    string cleanWord = word.Text.ToLowerInvariant(); 

                    if (!data.ContainsKey(cleanWord))
                    {
                        data[cleanWord] = new WordInfo();
                    }

                    data[cleanWord].Frequency++;
                    data[cleanWord].Lines.Add(lineNumber); 
                }
            }
        }

        public List<string> GetReport()
        {
            List<string> report = new List<string>();

            foreach (var pair in data.OrderBy(p => p.Key))
            {
                string word = pair.Key;
                WordInfo info = pair.Value;
                string linesString = string.Join(" ", info.Lines);
                report.Add($"{word}. {info.Frequency}: {linesString}");
            }

            return report;
        }
    }

    class WordInfo
    {
        public int Frequency { get; set; } = 0;
        public SortedSet<int> Lines { get; set; } = new SortedSet<int>();
    }
}