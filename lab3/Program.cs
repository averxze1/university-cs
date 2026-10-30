using System;
using System.Collections.Generic;
using System.IO;

namespace TextTokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            TextTests.RunAll();
            
            string inputFile      = args.Length > 0 ? args[0] : "Data/text.txt";
            string stopWordsFile  = args.Length > 1 ? args[1] : "Data/stopwords_ru.txt";
            string reportFile     = args.Length > 2 ? args[2] : "Data/Report.txt";
            string xmlOutputFile  = args.Length > 3 ? args[3] : "Data/text_export.xml";

            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Файл {inputFile} не найден!");
                return;
            }

            if (!File.Exists(stopWordsFile))
            {
                Console.WriteLine($"Файл {stopWordsFile} не найден!");
                return;
            }

            try
            {
                string content = File.ReadAllText(inputFile);

                ITextParser parser = new RegexTextParser();
                Text text = parser.Parse(content);

                TextAnalyzer analyzer = new TextAnalyzer(text);

                using (StreamWriter sw = new StreamWriter(reportFile))
                {
                    WriteSentencesByWordCount(sw, analyzer);
                    WriteSentencesByLength(sw, analyzer);
                    WriteQuestionWords(sw, analyzer);
                    WriteConsonantRemoval(sw, analyzer, text);
                    WriteReplacement(sw, analyzer, text);
                    WriteStopWordsRemoval(sw, analyzer, text, stopWordsFile);
                }

                TextXmlExporter.Export(text, xmlOutputFile);

                Console.WriteLine($"Файлы {reportFile} и {xmlOutputFile} успешно созданы!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void WriteSentencesByWordCount(StreamWriter sw, TextAnalyzer analyzer)
        {
            sw.WriteLine("1. Предложения по возрастанию количества слов:");
            foreach (Sentence sentence in analyzer.SentencesByWordCountAscending())
                sw.WriteLine($"[{sentence.WordCount}] {sentence.Render()}");
            sw.WriteLine();
        }

        static void WriteSentencesByLength(StreamWriter sw, TextAnalyzer analyzer)
        {
            sw.WriteLine("2. Предложения по возрастанию длины:");
            foreach (Sentence sentence in analyzer.SentencesByLengthAscending())
                sw.WriteLine($"[{sentence.Length}] {sentence.Render()}");
            sw.WriteLine();
        }

        static void WriteQuestionWords(StreamWriter sw, TextAnalyzer analyzer)
        {
            int questionWordLength = 5;
            List<string> words = analyzer.FindWordsInQuestions(questionWordLength);

            sw.WriteLine($"3. Слова длины {questionWordLength} в вопросительных предложениях:");
            sw.WriteLine(words.Count > 0 ? string.Join(", ", words) : "NOT FOUND");
            sw.WriteLine();
        }

        static void WriteConsonantRemoval(StreamWriter sw, TextAnalyzer analyzer, Text text)
        {
            int consonantWordLength = 7;
            int removed = analyzer.RemoveWordsStartingWithConsonant(consonantWordLength);

            sw.WriteLine($"4. Удалено слов длины {consonantWordLength}, начинающихся с согласной: {removed}");
            foreach (Sentence sentence in text.Sentences)
                sw.WriteLine(sentence.Render());
            sw.WriteLine();
        }

        static void WriteReplacement(StreamWriter sw, TextAnalyzer analyzer, Text text)
        {
            int sentenceIndex = 3;
            int wordLength = 6;
            string replacement = "СЛОВО";
            int replaced = analyzer.ReplaceWordsInSentence(sentenceIndex, wordLength, replacement);

            sw.WriteLine($"5. В предложении {sentenceIndex} заменено слов длины {wordLength} на \"{replacement}\": {replaced}");
            sw.WriteLine(text.Sentences[sentenceIndex].Render());
            sw.WriteLine();
        }

        static void WriteStopWordsRemoval(StreamWriter sw, TextAnalyzer analyzer, Text text, string stopWordsFile)
        {
            HashSet<string> stopWords = StopWordsLoader.Load(stopWordsFile);
            int removed = analyzer.RemoveStopWords(stopWords);

            sw.WriteLine($"6. Удалено стоп-слов: {removed}");
            foreach (Sentence sentence in text.Sentences)
                sw.WriteLine(sentence.Render());
            sw.WriteLine();

            sw.WriteLine("7. Текст экспортирован в XML (см. Data/text_export.xml)");
        }
    }
}
