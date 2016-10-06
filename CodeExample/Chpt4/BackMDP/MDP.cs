using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPMDP
{
	class MDP
	{
		[STAThread]
		static void Main(string[] args)
		{
			IPEndPoint localEP = new IPEndPoint(IPAddress.Loopback,20000);
			Console.WriteLine("Market-Data Producer Service Started - Using TCP");	
			//Market Data
			string mktPrice = "MSFT;25,IBM;24";	
			//Create network data conduit
			Socket mdpSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			//Associate socket with particular endpoint 
			mdpSocket.Bind(localEP);
			//server starts listening for client connection
			mdpSocket.Listen(10);
			while(true)
			{
				//synchronously extracts the first pending connection request 
				Socket mdcSocket = mdpSocket.Accept();
				//connection from client is accepted 
				IPEndPoint mdcRemoteEP = mdcSocket.RemoteEndPoint as IPEndPoint;
				Console.WriteLine("MDC EndPoint Info {0} {1} : ",mdcRemoteEP.Address.ToString(),mdcRemoteEP.Port);
				//data is flatted into array of bytes and 
				//dispatched to client 
				byte[] sendBuffer = new byte[512];
				sendBuffer = Encoding.ASCII.GetBytes(mktPrice);
				mdcSocket.Send(sendBuffer);
				//client connection is closed
				mdcSocket.Shutdown(SocketShutdown.Both);
				mdcSocket.Close();
			}
		}
	}
}
