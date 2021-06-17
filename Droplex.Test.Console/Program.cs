using System;
using System.Threading.Tasks;

namespace Droplex.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Task a = DroplexPackage.Drop(App.python3_9_1);
                Task b = DroplexPackage.Drop(App.Everything1_3_4_686);
                Task c = DroplexPackage.Drop(App.Putty_0_74);

                await Task.WhenAll(a, b, c);

                await DroplexPackage.Drop(App.python_3_8_9_embeddable, @"C:\temp\python-embeddable").ConfigureAwait(false);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.ReadKey();
            }
        }
    }
}