using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TextTokenizer
{
    static class Rules
    {
        public static readonly Regex SentenceRegex = new Regex(@"[^.!?]+[.!?]*", RegexOptions.Singleline);
        public static readonly Regex TokenRegex = new Regex(@"[^\W\d_]+(?:-[^\W\d_]+)*|\d+|[^\s\w]+", RegexOptions.Compiled);
        public static readonly Regex WordStartRegex = new Regex(@"^[^\W\d_]", RegexOptions.Compiled);

        public static readonly HashSet<char> Vowels = new HashSet<char>("aeiouyаеёиоуыэюя");
    }
}