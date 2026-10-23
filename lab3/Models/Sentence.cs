using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextTokenizer
{
    class Sentence
    {
        private readonly List<ISentenceToken> tokens = new List<ISentenceToken>();

        public IReadOnlyList<ISentenceToken> Tokens => tokens;

        public IEnumerable<Word> Words => tokens.OfType<Word>();

        public int WordCount => Words.Count();

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var token in tokens)
            {
                if (token.HasLeadingSpace && sb.Length > 0)
                    sb.Append(' ');
                sb.Append(token.Text);
            }
            return sb.ToString();
        }

        public int Length => Render().Length;

        public bool IsQuestion => Render().TrimEnd().EndsWith("?");

        public void Add(ISentenceToken token)
        {
            tokens.Add(token);
        }

        public void Remove(ISentenceToken token)
        {
            tokens.Remove(token);
        }

        public void Replace(ISentenceToken oldToken, ISentenceToken newToken)
        {
            int index = tokens.IndexOf(oldToken);
            if (index >= 0)
                tokens[index] = newToken;
        }
    }
}
