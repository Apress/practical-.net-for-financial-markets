using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using RPC.Common;

class HostUsingConfig
{
	static void Main(string[] args)
	{
		RemotingConfiguration.Configure(@"RPC.ServiceController.exe.config");
		WellKnownClientTypeEntry[] clientEntry =
			RemotingConfiguration.GetRegisteredWellKnownClientTypes();
		IService hbService = Activator.GetObject(typeof(IService),
			clientEntry[0].ObjectUrl) as IService;
		hbService.Start();
		Console.ReadLine();
	}			
}
