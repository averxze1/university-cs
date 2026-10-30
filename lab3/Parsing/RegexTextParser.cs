using System.Text.RegularExpressions;

namespace TextTokenizer
{
    class RegexTextParser : ITextParser
    {
        public Text Parse(string content)
        {
            Text text = new Text();

            foreach (Match sentenceMatch in Rules.SentenceRegex.Matches(content))
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

            foreach (Match tokenMatch in Rules.TokenRegex.Matches(raw))
            {
                string value = tokenMatch.Value;
                bool hasLeadingSpace = tokenMatch.Index > prevEnd;
                prevEnd = tokenMatch.Index + tokenMatch.Length;

                bool isWord = Rules.WordStartRegex.IsMatch(value) || char.IsDigit(value[0]);

                Token token = isWord
                    ? new Word(value, hasLeadingSpace)
                    : new Punctuation(value, hasLeadingSpace);
                    
                sentence.Add(token);
            }

            return sentence;
        }
    }
}
