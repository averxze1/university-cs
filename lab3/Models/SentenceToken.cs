namespace TextTokenizer
{
    class Word : Token
    {
        public Word(string text, bool hasLeadingSpace) 
            : base(text, hasLeadingSpace) 
        { 
        }
    }

    class Punctuation : Token
    {
        public Punctuation(string text, bool hasLeadingSpace) 
            : base(text, hasLeadingSpace) 
        { 
        }
    }
}