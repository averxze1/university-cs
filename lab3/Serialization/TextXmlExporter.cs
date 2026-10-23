using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace TextTokenizer
{
    static class TextXmlExporter
    {
        public static void Export(Text text, string path)
        {
            TextXml dto = ToDto(text);
            XmlSerializer serializer = new XmlSerializer(typeof(TextXml));

            using (StreamWriter writer = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                serializer.Serialize(writer, dto);
            }
        }

        private static TextXml ToDto(Text text)
        {
            TextXml dto = new TextXml();

            foreach (Sentence sentence in text.Sentences)
            {
                SentenceXml sentenceXml = new SentenceXml();

                foreach (ISentenceToken token in sentence.Tokens)
                {
                    if (token is Word word)
                        sentenceXml.Items.Add(new WordXml(word.Text));
                    else if (token is Punctuation punctuation)
                        sentenceXml.Items.Add(new PunctuationXml(punctuation.Text));
                }

                dto.Sentences.Add(sentenceXml);
            }

            return dto;
        }
    }
}
