using System;
using System.Threading.Tasks;

namespace Droplex.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Task a = DroplexPackage.Drop(App.python3_9_1);

            Task b = DroplexPackage.Drop(App.Everything1_3_4_686);

            Task.WaitAll(a, b);
        }
    }
}
