using System.Collections.Generic;
using System.Xml.Serialization;

namespace TextTokenizer
{
    [XmlRoot("text")]
    public class TextXml
    {
        [XmlElement("sentence")]
        public List<SentenceXml> Sentences { get; set; } = new List<SentenceXml>();
    }

    public class SentenceXml
    {
        [XmlElement("word", typeof(WordXml))]
        [XmlElement("punctuation", typeof(PunctuationXml))]
        public List<object> Items { get; set; } = new List<object>();
    }

    public class WordXml
    {
        [XmlText]
        public string Value { get; set; } = "";

        public WordXml() { }

        public WordXml(string value)
        {
            Value = value;
        }
    }

    public class PunctuationXml
    {
        [XmlText]
        public string Value { get; set; } = "";

        public PunctuationXml() { }

        public PunctuationXml(string value)
        {
            Value = value;
        }
    }
}
