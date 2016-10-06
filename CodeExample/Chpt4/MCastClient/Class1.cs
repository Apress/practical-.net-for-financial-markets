using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MCastClient
{
	class MDC
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Consumer Service Started - (Using MultiCast)");	
			byte[] receiveBuffer = new byte[512];
			IPHostEntry entry = Dns.GetHostByName(Dns.GetHostName());
			EndPoint localEP = new IPEndPoint(entry.AddressList[0],30002);
			Socket mdcSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
			mdcSocket.Bind(localEP);
			
			//Start receiving multicast data by subscribing
			//to below multicast address
			IPAddress groupAddress = IPAddress.Parse("224.5.6.7");
			MulticastOption mcastOption = new MulticastOption(groupAddress);
			mdcSocket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.AddMembership,mcastOption);
			EndPoint endPoint = new IPEndPoint(IPAddress.Any,0);
			
			int bytesReceived = mdcSocket.ReceiveFrom(receiveBuffer,ref endPoint);
			IPEndPoint mdpEndPoint = (IPEndPoint)endPoint;
			string mktPrice = Encoding.ASCII.GetString(receiveBuffer,0,bytesReceived);
			Console.WriteLine("Market-Data Received : " +mktPrice);
			Console.WriteLine("Market Data Producer IP Address {0} Port {1} " ,mdpEndPoint.Address.ToString(), mdpEndPoint.Port);
			mdcSocket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.DropMembership,mcastOption);
			mdcSocket.Close();
			Console.ReadLine();			
		}

		public static void DisableMulticastLoopBack(Socket sockInstance)
		{
			sockInstance.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.MulticastLoopback,0);
		}
	}
}
