using System;

namespace TextTokenizer
{
    static class TextTests
    {
        public static void RunAll()
        {
            Console.WriteLine("=== Запуск тестов ===\n");

            TestParser();
            TestAnalyzer();

            Console.WriteLine("\n=== Тесты завершены ===");
        }

        private static void TestParser()
        {
            Console.WriteLine("Тест Парсера:");
            ITextParser parser = new RegexTextParser();

            string input = "Привет, мир! Это тест-драйв 2024.";
            Text text = parser.Parse(input);

            if (text.Sentences.Count == 2)
                Console.WriteLine("[OK] Парсер правильно определил количество предложений.");
            else
                Console.WriteLine($"[FAIL] Ожидалось 2 предложения, найдено {text.Sentences.Count}");

            var sentence1 = text.Sentences[0];
            if (sentence1.Tokens.Count == 4)
                Console.WriteLine("[OK] Парсер правильно разбил первое предложение на токены.");
            else
                Console.WriteLine($"[FAIL] Ожидалось 4 токена в первом предложении, найдено {sentence1.Tokens.Count}");
            
            Console.WriteLine();
        }

        private static void TestAnalyzer()
        {
            Console.WriteLine("Тест Анализатора:");
            ITextParser parser = new RegexTextParser();
            
            string input = "Как дела? Я делаю уроки.";
            Text text = parser.Parse(input);
            TextAnalyzer analyzer = new TextAnalyzer(text);

            var wordsInQuestions = analyzer.FindWordsInQuestions(3);
            if (wordsInQuestions.Count == 1 && wordsInQuestions[0] == "Как")
                Console.WriteLine("[OK] Поиск слов в вопросах работает верно.");
            else
                Console.WriteLine("[FAIL] Поиск слов в вопросах работает некорректно.");

            analyzer.ReplaceWordsInSentence(1, 4, "X");
            
            string secondSentence = text.Sentences[1].Render();
            
            if (secondSentence.Contains("X"))
                Console.WriteLine("[OK] Замена слов работает верно.");
            else
                Console.WriteLine("[FAIL] Замена слов не сработала.");

            Console.WriteLine();
        }
    }
}