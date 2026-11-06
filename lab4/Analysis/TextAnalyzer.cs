using System.Collections.Generic;
using System.Linq;

namespace TextTokenizer
{
    class TextAnalyzer
    {
        private readonly Text text;

        public TextAnalyzer(Text text)
        {
            this.text = text;
        }

        public List<Sentence> SentencesByWordCountAscending()
        {
            return text.Sentences.OrderBy(s => s.WordCount).ToList();
        }

        public List<Sentence> SentencesByLengthAscending()
        {
            return text.Sentences.OrderBy(s => s.Length).ToList();
        }

        public List<string> FindWordsInQuestions(int wordLength)
        {
            return text.Sentences
                .Where(s => s.IsQuestion)
                .SelectMany(s => s.Words)
                .Where(w => w.Text.Length == wordLength)
                .Select(w => w.Text)
                .Distinct(System.StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public int RemoveWordsStartingWithConsonant(int wordLength)
        {
            int removedCount = 0;

            foreach (Sentence sentence in text.Sentences)
            {
                List<Word> toRemove = sentence.Words
                    .Where(w => w.Text.Length == wordLength && StartsWithConsonant(w.Text))
                    .ToList();

                foreach (Word word in toRemove)
                {
                    sentence.Remove(word);
                    removedCount++;
                }
            }

            text.RemoveEmptySentences();
            return removedCount;
        }

        public int ReplaceWordsInSentence(int sentenceIndex, int wordLength, string replacement)
        {
            Sentence sentence = text.Sentences[sentenceIndex];

            List<Word> targets = sentence.Words
                .Where(w => w.Text.Length == wordLength)
                .ToList();

            foreach (Word word in targets)
                sentence.Replace(word, new Word(replacement, word.HasLeadingSpace));

            return targets.Count;
        }

        public int RemoveStopWords(ISet<string> stopWords)
        {
            int removedCount = 0;

            foreach (Sentence sentence in text.Sentences)
            {
                List<Word> toRemove = sentence.Words
                    .Where(w => stopWords.Contains(w.Text))
                    .ToList();

                foreach (Word word in toRemove)
                {
                    sentence.Remove(word);
                    removedCount++;
                }
            }

            text.RemoveEmptySentences();
            return removedCount;
        }

        private static bool StartsWithConsonant(string word)
        {
            char c = char.ToLowerInvariant(word[0]);
            return char.IsLetter(c) && !Rules.Vowels.Contains(c);
        }
    }
}
