//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//
//namespace ProtocolTweakngServer
//{
//	class Class1
//	{
//		[STAThread]
//		static void Main(string[] args)
//		{
//			IPHostEntry entry = Dns.Resolve(Dns.GetHostName());
//			IPEndPoint localEP = new IPEndPoint(entry.AddressList[0],20000);
//			Console.WriteLine("Market-Data Producer Service Started - Using TCP");	
//			string mktPrice = "MSFT;25,IBM;24";	
//			Socket mdpSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
//			mdpSocket.Bind(localEP);
//			mdpSocket.Listen(10);
//			while(true)
//			{
//				Socket mdcSocket = mdpSocket.Accept();
//				mdcSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendBuffer,2000);
//				int bufferSize = (int)mdcSocket.GetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendBuffer);
//				Console.WriteLine("Buffer : " +bufferSize);
//				IPEndPoint mdcRemoteEP = mdcSocket.RemoteEndPoint as IPEndPoint;
//				Console.WriteLine("MDC EndPoint Info {0} {1} : ",mdcRemoteEP.Address.ToString(),mdcRemoteEP.Port);
//				byte[] sendBuffer = new byte[10000];
//				for(int ctr=0;ctr<=100;ctr++)
//				{
//					mdcSocket.Send(sendBuffer);
//				}
//				mdcSocket.Shutdown(SocketShutdown.Both);
//				mdcSocket.Close();
//			}
//
//		}
//	}
//}

using System;
using System.Collections.Specialized;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace MDP
{
	class MDP
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Producer Service Started");	
			string mktPrice = "MSFT;25,IBM;24";
			IPHostEntry host = Dns.Resolve(Dns.GetHostName());
			EndPoint[] mdcEndPointList = new EndPoint[]{new IPEndPoint(host.AddressList[0],30000)};
			
			/*Socket mdpSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
			byte[] sendBuffer = new byte[50000];
			//sendBuffer = Encoding.ASCII.GetBytes(mktPrice);
			
			
			mdpSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendBuffer,3);
			int swater = (int)mdpSocket.GetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendBuffer);
			Console.WriteLine(swater);
			

			

			//MultiCastTTL(mdpSocket);
			foreach(EndPoint mdcEndPoint in mdcEndPointList )
			{
				for(int ctr=0;ctr<=100;ctr++)
				{
					mdpSocket.SendTo(sendBuffer,mdcEndPoint);
				}
			}
			Console.WriteLine("Market Data Sent to all Market-Data consumer clients");
			Console.ReadLine();
			mdpSocket.Close();*/
			Thread t1,t2;
			t1=new Thread(new ThreadStart(TCPSocket));
			t2=new Thread(new ThreadStart(TCPSocketInstance2));
			//t2.Start();
			t1.Start();
			Console.ReadLine();
		}
		public static void TCPSocket()
		{
			IPHostEntry host = Dns.Resolve(Dns.GetHostName());
			Socket listeningSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			SetTimeOut(listeningSocket,10,10);
			listeningSocket.Bind(new IPEndPoint(host.AddressList[0],30000));
			listeningSocket.Listen(10);
			Socket accepSocket = listeningSocket.Accept();
			Console.ReadLine();

		}

		public static void SetTimeOut(Socket sockInstance,int recvBuffer,int sendBuffer)
		{
			sockInstance.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendTimeout,10);
			sockInstance.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReceiveTimeout,10);
		}

		public static void SetBufferSize(Socket sockInstance,int recvBuffer,int sendBuffer)
		{
			sockInstance.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendBuffer,sendBuffer);
			sockInstance.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReceiveBuffer,recvBuffer);
		}

		public static void DisableNagle(Socket sockInstance)
		{
			sockInstance.SetSocketOption(SocketOptionLevel.Tcp,SocketOptionName.NoDelay,1);
		}

		public static void TCPSocketInstance2()
		{
			IPHostEntry host = Dns.Resolve(Dns.GetHostName());
			Socket listeningSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			ReuseSocket(listeningSocket);
			listeningSocket.Bind(new IPEndPoint(host.AddressList[0],30000));
			listeningSocket.Listen(10);
			Socket accepSocket = listeningSocket.Accept();
			Console.ReadLine();
		}

		public static void MultiCastTTL(Socket sockInstance)
		{
			//Subnet Scope
			sockInstance.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.MulticastTimeToLive,3);
		}

		public static void IPTTL(Socket sockInstance)
		{
			//Set the TTL to 4
			sockInstance.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.IpTimeToLive,4);
			int ipTTL= (int)sockInstance.GetSocketOption(SocketOptionLevel.IP,SocketOptionName.IpTimeToLive);
			Console.WriteLine(ipTTL);
		}

		public static void IPFragment(Socket sockInstance)
		{
			//Disable the Fragmentation
			sockInstance.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.DontFragment,1);
			//Get Assigned Fragmentation Value
			int isFragmented = (int)sockInstance.GetSocketOption(SocketOptionLevel.IP,SocketOptionName.DontFragment);
			Console.WriteLine(isFragmented);
		}

		public static void ReuseSocket(Socket sockInstance)
		{
			sockInstance.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReuseAddress,1);
		}

		public static void DisableMulticastLoopBack(Socket sockInstance)
		{
			sockInstance.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.MulticastLoopback,0);
		}

	}
}