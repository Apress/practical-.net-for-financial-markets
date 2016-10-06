using System;
using System.Configuration;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;


namespace RPC.Services
{
	class Host
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("HeartBeat Service Console..");

			BinaryServerFormatterSinkProvider svrFormatter = new BinaryServerFormatterSinkProvider();
			TcpServerChannel svrChannel = new TcpServerChannel("ServiceChannel",15000,svrFormatter);
			
			ChannelServices.RegisterChannel(svrChannel);
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(HeartBeatService),
				                                               "HeartBeatService.rem",WellKnownObjectMode.Singleton);
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(HeartBeatServiceInfo),
				"HeartBeatServiceInfo.rem",WellKnownObjectMode.SingleCall);

			Console.WriteLine("Infrastructure service Host Started...");
			Console.ReadLine();
		}
	}
}
