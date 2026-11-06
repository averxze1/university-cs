using System;
using System.Linq;

namespace TextTokenizer
{
    static class ConcordanceTests
    {
        public static void RunAll()
        {
            Console.WriteLine("=== ЗАПУСК ТЕСТА КОНКОРДАНСА ===\n");

            string input = "Hello world. Hello again.";
            ITextParser parser = new RegexTextParser();
            Text text = parser.Parse(input);

            Concordance concordance = new Concordance();
            concordance.Build(text);

            var report = concordance.GetReport();

            Console.WriteLine("Результат работы конкорданса:");
            foreach (string line in report)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();

            bool isAgainCorrect = report.Any(r => r.Contains("again. 1: 2"));
            bool isHelloCorrect = report.Any(r => r.Contains("hello. 2: 1 2"));
            bool isWorldCorrect = report.Any(r => r.Contains("world. 1: 1"));

            if (isAgainCorrect && isHelloCorrect && isWorldCorrect)
            {
                Console.WriteLine("[OK] Тест пройден! Конкорданс считает правильно.");
            }
            else
            {
                Console.WriteLine("[FAIL] Тест не пройден! Ошибка в подсчетах.");
            }
        }
    }
}