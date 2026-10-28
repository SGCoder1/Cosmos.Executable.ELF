using Cosmos.Executable.ELF;
using System.Runtime.InteropServices;

namespace ElfTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ElfReader reader = new(File.OpenRead("TestElf-AA64"));
            reader.Execute();
            reader.Dispose();
        }
    }
}
