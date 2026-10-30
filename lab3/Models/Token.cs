namespace TextTokenizer
{
    abstract class Token
    {
        public string Text { get; }
        public bool HasLeadingSpace { get; }

        protected Token(string text, bool hasLeadingSpace)
        {
            Text = text;
            HasLeadingSpace = hasLeadingSpace;
        }
    }
}