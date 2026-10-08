using System;
using System.IO;

namespace CatAndMouse
{
    enum GameState
    {
        Start,
        End
    }

    class Game
    {
        public static string InputFile = "Data/1.ChaseData.txt";
        public static string OutFile = "Data/1.PursuitLog.txt";

        public int size;
        public Player cat;
        public Player mouse;
        public GameState state;

        public Game(int size)
        {
            this.size = size;
            cat = new Player("Cat");
            mouse = new Player("Mouse");
            state = GameState.Start;
        }

        public static int ReadSize(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string? firstLine = sr.ReadLine();
                return int.Parse((firstLine ?? "0").Trim());
            }
        }

        public void Run()
        {
            using (StreamReader sr = new StreamReader(InputFile))
            using (StreamWriter sw = new StreamWriter(OutFile))
            {
                sr.ReadLine();

                sw.WriteLine("Cat and Mouse");
                sw.WriteLine();
                sw.WriteLine("Cat Mouse  Distance");
                sw.WriteLine(new string('-', 19));

                while (state != GameState.End)
                {
                    string? line = sr.ReadLine();
                    if (line == null)
                    {
                        state = GameState.End;
                        break;
                    }

                    line = line.Trim();
                    if (line.Length == 0) continue;

                    char command = line[0];
                    string rest = line.Length > 1 ? line.Substring(1).Trim() : "";

                    if (command == 'P')
                    {
                        DoPrintCommand(sw);
                    }
                    else
                    {
                        int steps = int.Parse(rest);
                        DoMoveCommand(command, steps);

                        if (cat.state == State.Playing && mouse.state == State.Playing &&
                            cat.location == mouse.location)
                        {
                            mouse.state = State.Looser;
                            cat.state = State.Winner;
                            state = GameState.End;
                        }
                    }
                }

                sw.WriteLine(new string('-', 19));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Distance traveled:   Mouse    Cat");
                sw.WriteLine($"{mouse.distanceTraveled,26}{cat.distanceTraveled,7}");
                sw.WriteLine();

                if (mouse.state == State.Looser)
                    sw.WriteLine($"Mouse caught at: {mouse.location,2}");
                else
                    sw.WriteLine("Mouse evaded Cat");
            }
        }

        private void DoMoveCommand(char command, int steps)
        {
            switch (command)
            {
                case 'M': mouse.Move(steps, size); break;
                case 'C': cat.Move(steps, size); break;
            }
        }

        private void DoPrintCommand(StreamWriter sw)
        {
            string catStr = cat.state == State.NotInGame ? "??" : cat.location.ToString();
            string mouseStr = mouse.state == State.NotInGame ? "??" : mouse.location.ToString();

            string row = $"{catStr,3}{mouseStr,6}";

            if (cat.state != State.NotInGame && mouse.state != State.NotInGame)
            {
                int distance = GetDistance();
                row += $"{distance,10}";
            }

            sw.WriteLine(row);
        }

        private int GetDistance()
        {
            return Math.Abs(cat.location - mouse.location);
        }
    }
}
