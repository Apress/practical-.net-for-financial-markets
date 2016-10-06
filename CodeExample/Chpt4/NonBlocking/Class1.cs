using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NonBlocking
{
	class MDC
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Consumer Service Started - Using TCP (Non-Blocking Mode)");	
			IPEndPoint mdpEP = new IPEndPoint(IPAddress.Loopback,20000);
			Socket mdcSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			mdcSocket.Blocking=false;
			mdcSocket.Connect(mdpEP);
			while(true)
			{
				if ( mdcSocket.Poll(1000,SelectMode.SelectRead) == true ) 
				{
					byte[] receiveBuffer = new byte[512];
					int bytesReceived = mdcSocket.Receive(receiveBuffer);
					string mktPrice = Encoding.ASCII.GetString(receiveBuffer,0,bytesReceived);
					Console.WriteLine(mktPrice);
					mdcSocket.Shutdown(SocketShutdown.Both);
					mdcSocket.Close();
				}
				else
				{
					//Do Some Other Work
				}
			}
				Console.ReadLine();

		}
	}
}
