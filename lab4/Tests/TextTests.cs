using System;

namespace TextTokenizer
{
    static class TextTests
    {
        public static void RunAll()
        {
            Console.WriteLine("=== ЗАПУСК ТЕСТОВ ===\n");

            TestParser();
            TestAnalyzer();

            Console.WriteLine("\n=== ТЕСТЫ ЗАВЕРШЕНЫ ===");
        }

        private static void TestParser()
        {
            Console.WriteLine("--- ТЕСТ 1: Парсер ---");
            ITextParser parser = new RegexTextParser();
            
            string input = "Привет, мир! Это тест-драйв 2024.";
            Text text = parser.Parse(input);

            Console.WriteLine($"Входной текст: \"{input}\"");
            Console.WriteLine($"Найдено предложений: {text.Sentences.Count} (ожидалось 2)");

            if (text.Sentences.Count == 2)
                Console.WriteLine("[OK] Количество предложений верное.\n");
            else
                Console.WriteLine("[FAIL] Ошибка в количестве предложений!\n");

            var sentence1 = text.Sentences[0];
            Console.WriteLine($"Первое предложение: \"{sentence1.Render()}\"");
            Console.WriteLine($"Количество токенов: {sentence1.Tokens.Count} (ожидалось 4: 'Привет', ',', 'мир', '!')");
            
            Console.Write("Токены: ");
            foreach (var t in sentence1.Tokens)
            {
                Console.Write($"[{t.Text}] ");
            }
            Console.WriteLine();

            if (sentence1.Tokens.Count == 4)
                Console.WriteLine("[OK] Разбиение на токены верное.\n");
            else
                Console.WriteLine("[FAIL] Ошибка в разбиении на токены!\n");
        }

        private static void TestAnalyzer()
        {
            Console.WriteLine("--- ТЕСТ 2: Анализатор ---");
            ITextParser parser = new RegexTextParser();
            
            string input = "Это тест. Как дела?";
            Text text = parser.Parse(input);
            TextAnalyzer analyzer = new TextAnalyzer(text);

            Console.WriteLine($"Входной текст: \"{input}\"");

            var words = analyzer.FindWordsInQuestions(3);
            Console.WriteLine($"Ищем слова длины 3 в вопросах. Найдено: [{string.Join(", ", words)}] (ожидалось 'Как')");
            
            if (words.Contains("Как"))
                Console.WriteLine("[OK] Поиск в вопросах верный.\n");
            else
                Console.WriteLine("[FAIL] Поиск в вопросах не сработал!\n");

            Console.WriteLine("Заменяем слова длины 4 в первом предложении на 'X'...");
            analyzer.ReplaceWordsInSentence(0, 4, "X");
            
            string resultText = text.Sentences[0].Render();
            Console.WriteLine($"Текст после замены: \"{resultText}\" (ожидалось \"Это X.\")");

            if (resultText.Contains("X") && !resultText.Contains("тест"))
                Console.WriteLine("[OK] Замена слов верная.\n");
            else
                Console.WriteLine("[FAIL] Замена слов не сработала!\n");
        }
    }
}