using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace RemoteServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure(@"RemoteServer.exe.config",false);
            Console.WriteLine("Remote Container Started...");
            Console.ReadLine();
        }
    }
}
