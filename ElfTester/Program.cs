using System.Runtime.InteropServices;

namespace ElfTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine(Architecture.X86);
            Console.WriteLine(Architecture.LoongArch64);
        }
    }
}
