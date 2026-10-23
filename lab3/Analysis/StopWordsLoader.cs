using System;
using System.Collections.Generic;
using System.IO;

namespace TextTokenizer
{
    static class StopWordsLoader
    {
        public static HashSet<string> Load(string path)
        {
            HashSet<string> stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (string line in File.ReadAllLines(path))
            {
                string trimmed = line.Trim();
                if (trimmed.Length > 0)
                    stopWords.Add(trimmed);
            }

            return stopWords;
        }
    }
}
