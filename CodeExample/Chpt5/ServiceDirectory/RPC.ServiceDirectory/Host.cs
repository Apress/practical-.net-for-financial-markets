using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;

namespace RPC.ServiceDirectory
{
	class Host
	{
		[STAThread]
		static void Main(string[] args)
		{
			ServiceLookUp serviceLookUp = new ServiceLookUp();
			BinaryServerFormatterSinkProvider svrFormatter = new BinaryServerFormatterSinkProvider();
			TcpServerChannel svrChannel = new TcpServerChannel("ServiceChannel",12000,svrFormatter);
			RemotingServices.Marshal(serviceLookUp,"ServiceDirectory.rem");
			Console.WriteLine("LookUp Service Started...");
			Console.ReadLine();
		}
	}
}
