using System;

namespace CatAndMouse
{
    static class PlayerTests
    {
        public static void RunAll()
        {
            Console.WriteLine("=== Тесты Player.Move ===\n");

            Player p1 = new Player("Test");
            p1.Move(7, 16);
            Console.WriteLine($"Тест 1 (стартовая позиция): location={p1.location}, state={p1.state}, distance={p1.distanceTraveled}");

            Player p2 = new Player("Test");
            p2.Move(5, 16);   
            p2.Move(3, 16);   
            Console.WriteLine($"Тест 2 (ход вперёд): location={p2.location}, distance={p2.distanceTraveled}");

            Player p3 = new Player("Test");
            p3.Move(16, 16);  
            p3.Move(1, 16);   
            Console.WriteLine($"Тест 3 (перескок): location={p3.location}, distance={p3.distanceTraveled}");

            Console.WriteLine("\nПроверка:");
            Console.WriteLine("Тест 1: должно быть location=7, state=Playing, distance=0");
            Console.WriteLine("Тест 2: должно быть location=8, distance=3");
            Console.WriteLine("Тест 3: должно быть location=1, distance=1");
        }
    }
}