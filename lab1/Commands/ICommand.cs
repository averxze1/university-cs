using System.Collections.Generic;
using System.IO;

namespace GeneticSearch
{
    /// <summary>
    /// Общий интерфейс для всех операций (search, diff, mode).
    /// Каждая операция знает своё имя из commands.txt и умеет выполнить
    /// себя над списком белков, записав результат в выходной файл.
    /// </summary>
    interface ICommand
    {
        /// <summary>Имя команды, как оно записано в commands.txt (в нижнем регистре).</summary>
        string Name { get; }

        /// <summary>
        /// Выполняет операцию и пишет результат в sw.
        /// parameters - аргументы команды из commands.txt, без самого имени команды.
        /// </summary>
        void Execute(List<GeneticData> proteins, string[] parameters, StreamWriter sw);
    }
}
