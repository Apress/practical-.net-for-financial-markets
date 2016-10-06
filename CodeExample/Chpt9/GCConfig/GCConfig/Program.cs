using System;
using System.Runtime;

namespace GCConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            if (GCSettings.IsServerGC == true)
                Console.WriteLine("Server GC enabled");
            else
                Console.WriteLine("Workstation GC enabled");
        }
    }
}
