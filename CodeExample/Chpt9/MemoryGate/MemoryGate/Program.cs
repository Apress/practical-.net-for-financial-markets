using System;
using System.Runtime;

namespace MemoryGate
{
    class Program
    {
        static void Main(string[] args)
        {
            //Check whether application can allocate 20 MB of memory to perform File copy operation
            using (new MemoryFailPoint(20))
            {
                //Perform File Copy Operation
            }
        }
    }
}
