using System;
using System.IO;

namespace CatAndMouse
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile  = args.Length > 0 ? args[0] : "Data/1.ChaseData.txt";
            string outputFile = args.Length > 1 ? args[1] : "Data/NewPursuitLog.txt";

            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Файл {inputFile} не найден!");
                return;
            }

            try
            {
                Game.InputFile = inputFile;
                Game.OutFile = outputFile;

                int size = Game.ReadSize(inputFile);
                Game game = new Game(size);
                game.Run();

                Console.WriteLine($"Файл {outputFile} успешно создан!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}
