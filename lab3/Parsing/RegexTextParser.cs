using System.Text.RegularExpressions;

namespace TextTokenizer
{
    class RegexTextParser : ITextParser
    {
        private static readonly Regex SentenceRegex = new Regex(@"[^.!?]+[.!?]*", RegexOptions.Singleline);
        private static readonly Regex TokenRegex = new Regex(@"[^\W\d_]+(?:-[^\W\d_]+)*|\d+|[^\s\w]+", RegexOptions.Compiled);
        private static readonly Regex WordStartRegex = new Regex(@"^[^\W\d_]", RegexOptions.Compiled);

        public Text Parse(string content)
        {
            Text text = new Text();

            foreach (Match sentenceMatch in SentenceRegex.Matches(content))
            {
                string raw = sentenceMatch.Value.Trim();
                if (raw.Length == 0) continue;

                Sentence sentence = ParseSentence(raw);
                if (sentence.WordCount > 0)
                    text.Add(sentence);
            }

            return text;
        }

        private Sentence ParseSentence(string raw)
        {
            Sentence sentence = new Sentence();
            int prevEnd = 0;

            foreach (Match tokenMatch in TokenRegex.Matches(raw))
            {
                string value = tokenMatch.Value;
                bool hasLeadingSpace = tokenMatch.Index > prevEnd;
                prevEnd = tokenMatch.Index + tokenMatch.Length;

                bool isWord = WordStartRegex.IsMatch(value) || char.IsDigit(value[0]);

                Token token = isWord
                    ? new Word(value, hasLeadingSpace)
                    : new Punctuation(value, hasLeadingSpace);
                    
                sentence.Add(token);
            }

            return sentence;
        }
    }
}
