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
			//Identify the wire-encoding format; in this case we have selected
			//BinaryFormatter
			BinaryClientFormatterSinkProvider cltFormatter =
				new BinaryClientFormatterSinkProvider();
			
			//Identify the communication protocol used to interact with server.
			//The wire-encoding details are also specified here
			TcpClientChannel cltChannel =
				new TcpClientChannel("ControllerChannel",cltFormatter);
			
			//Registration of communication protocol and wire-encoding format to be used
			//by remoting infrastructure
			ChannelServices.RegisterChannel(cltChannel);
			
			//Instantiation of Remote type (Service MetaInformation)
			IServiceInfo serviceInfo = Activator.GetObject(typeof(IServiceInfo),
				"tcp://localhost:15000/HeartBeatServiceInfo.rem") as IServiceInfo;
			
			//Service meta-information is retrieved to determine the actual
			//location of the heartbeat service
			ServiceInfo heartBeatInfo = serviceInfo.QueryServiceInfo;
			Console.WriteLine("Starting Service : " +heartBeatInfo.FriendlyName);

			//Instantiation of heartbeat service
			IService hbService = Activator.GetObject(typeof(IService),
				heartBeatInfo.Location) as IService;
			hbService.Start();
			Console.ReadLine();
		}			
	}
}
