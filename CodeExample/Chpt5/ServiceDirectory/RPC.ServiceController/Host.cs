using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using RPC.Common;

namespace RPC.ServiceController
{
	class Host
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Service Controller Console..");
			BinaryClientFormatterSinkProvider cltFormatter = new BinaryClientFormatterSinkProvider();
			TcpClientChannel cltChannel = new TcpClientChannel("ControllerChannel",cltFormatter);
			ChannelServices.RegisterChannel(cltChannel);
			ILookUp serviceLookUp= Activator.GetObject(typeof(ILookUp),"tcp://localhost:12000/ServiceDirectory.rem") as ILookUp;
			Console.WriteLine("Querying Service Directory...");
			IService hbService = serviceLookUp.LookUp("HeartBeatService");
			Console.WriteLine("Starting HeartBeat Service...");
			hbService.Start();
			Console.ReadLine();
		}			
	}
}
