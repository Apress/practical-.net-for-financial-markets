using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace MktInfoCacheServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure(@"MktInfoCacheServer.exe.config");
            Console.WriteLine("Market Information Cache Server started.. ");
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}
