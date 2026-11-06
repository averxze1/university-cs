using System.Collections.Generic;

namespace TextTokenizer
{
    class Text
    {
        private readonly List<Sentence> sentences = new List<Sentence>();

        public IReadOnlyList<Sentence> Sentences => sentences;

        public void Add(Sentence sentence)
        {
            sentences.Add(sentence);
        }

        public void RemoveEmptySentences()
        {
            sentences.RemoveAll(s => s.WordCount == 0);
        }
    }
}
