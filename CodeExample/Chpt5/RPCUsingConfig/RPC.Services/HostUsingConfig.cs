using System;
using System.Configuration;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;


class HostUsingConfig
{
	static void Main(string[] args)
	{
		RemotingConfiguration.Configure(@"RPC.Services.exe.config");
		Console.WriteLine("Infrastructure service host Started...");
		Console.ReadLine();
	}
}
