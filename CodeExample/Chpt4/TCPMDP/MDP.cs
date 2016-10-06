using System;
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
			string mktPrice = "MSFT;25,IBM;24";	
			Socket mdpSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			mdpSocket.Bind(localEP);
			mdpSocket.Listen(10);
			Socket mdcSocket = mdpSocket.Accept();
			byte[] sendBuffer = new byte[512];
			sendBuffer = Encoding.ASCII.GetBytes(mktPrice);
			mdcSocket.Send(sendBuffer);
			mdcSocket.Shutdown(SocketShutdown.Both);
			mdcSocket.Close();
		}
	}
}
