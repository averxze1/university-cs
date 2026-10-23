namespace TextTokenizer
{
    interface ISentenceToken
    {
        string Text { get; }
        bool HasLeadingSpace { get; }
    }

    class Word : ISentenceToken
    {
        public string Text { get; }
        public bool HasLeadingSpace { get; }

        public Word(string text, bool hasLeadingSpace)
        {
            Text = text;
            HasLeadingSpace = hasLeadingSpace;
        }
    }

    class Punctuation : ISentenceToken
    {
        public string Text { get; }
        public bool HasLeadingSpace { get; }

        public Punctuation(string text, bool hasLeadingSpace)
        {
            Text = text;
            HasLeadingSpace = hasLeadingSpace;
        }
    }
}
