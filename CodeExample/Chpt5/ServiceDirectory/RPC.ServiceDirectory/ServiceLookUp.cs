using System;
using System.Collections;
using RPC.Common;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;


namespace RPC.ServiceDirectory
{
	public class ServiceLookUp : MarshalByRefObject,ILookUp
	{
		Hashtable connectedServices = new Hashtable();
		
		public ServiceLookUp()
		{
			BinaryClientFormatterSinkProvider cltFormatter = new BinaryClientFormatterSinkProvider();
			TcpClientChannel cltChannel = new TcpClientChannel("ControllerChannel",cltFormatter);
			ChannelServices.RegisterChannel(cltChannel);
			IService hbService = Activator.GetObject(typeof(IService),"tcp://localhost:15000/HeartBeatService.rem") as IService;
			connectedServices.Add("HeartBeatService",hbService);
		}
		
		public override object InitializeLifetimeService()
		{
			return null;
		}

		#region ILookUp Members
		public IService LookUp(string serviceName)
		{
			Console.WriteLine("Lookup Request Received For : " +serviceName);
			return connectedServices[serviceName] as IService;
		}
		#endregion
	}
}
