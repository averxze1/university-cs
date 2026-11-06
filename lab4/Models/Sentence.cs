using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextTokenizer
{
    class Sentence
    {
        private readonly List<Token> tokens = new List<Token>();

        public IReadOnlyList<Token> Tokens => tokens;
        public IEnumerable<Word> Words => tokens.OfType<Word>();

        public int WordCount => Words.Count();
        public int Length => Render().Length;
        public bool IsQuestion => Render().TrimEnd().EndsWith("?");

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

        public void Add(Token token)
        {
            tokens.Add(token);
        }

        public void Remove(Token token)
        {
            tokens.Remove(token);
        }

        public void Replace(Token oldToken, Token newToken)
        {
            int index = tokens.IndexOf(oldToken);
            if (index >= 0)
                tokens[index] = newToken;
        }
    }
}