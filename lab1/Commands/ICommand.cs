using System.Collections.Generic;
using System.IO;

namespace GeneticSearch
{
    interface ICommand
    {
        string Name { get; }

        void Execute(List<GeneticData> proteins, string[] parameters, StreamWriter sw);
    }
}
