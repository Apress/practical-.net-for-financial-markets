using System;
using System.Net.Sockets;
using System.Net;

namespace BCastServer
{
	public class TransportModule : IModule
	{
		Socket serverSocket;
		IPEndPoint mcastEP;

		public TransportModule()
		{
			//Create a multi-cast ip address
			IPAddress bcastAddress =IPAddress.Parse("224.5.6.7");
			mcastEP = new IPEndPoint(bcastAddress ,30002);
			serverSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
			serverSocket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.MulticastTimeToLive, 3);
		}

		public object Process(PipeContext pipeCtx)
		{
			//data is broad-casted after it is received
			//from serializer module
			DataSerializerContext szCtx =  pipeCtx.Data as DataSerializerContext;
			serverSocket.BeginSendTo(szCtx.Data,0,szCtx.Data.Length,SocketFlags.None,mcastEP,
				new AsyncCallback(SendData),null);
			return null;
		}

		private void SendData(IAsyncResult result)
		{
			serverSocket.EndSend(result);
		}

	}
}
