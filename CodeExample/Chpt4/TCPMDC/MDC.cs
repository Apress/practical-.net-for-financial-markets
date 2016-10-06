using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPMDC
{
	class MDC
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Consumer Service Started - Using TCP");	
			IPEndPoint mdpEP = new IPEndPoint(IPAddress.Loopback,20000);
			Socket mdcSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			//Establishes connection with market data server
			mdcSocket.Connect(mdpEP);
			byte[] receiveBuffer = new byte[512];
			//Receive market data 
			int bytesReceived = mdcSocket.Receive(receiveBuffer);
			string mktPrice = Encoding.ASCII.GetString(receiveBuffer,0,bytesReceived);
			Console.WriteLine(mktPrice);
			//Close connection
			mdcSocket.Shutdown(SocketShutdown.Both);
			mdcSocket.Close();
			Console.ReadLine();

		}
	}
}
