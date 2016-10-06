using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UnSolicitedBcastServer
{
	class MDP
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Producer Service Started - (Unsolicited Broadcast)");	
			string mktPrice = "MSFT;25,IBM;24";	
			Socket mdpSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);

			//Broadcast IP address
			IPEndPoint bcastEndPoint = new IPEndPoint(IPAddress.Broadcast,30001);
			//Set socket in broadcast mode
			mdpSocket.SetSocketOption( SocketOptionLevel.Socket,SocketOptionName.Broadcast, 1);

			byte[] sendBuffer = new byte[512];
			sendBuffer = Encoding.ASCII.GetBytes(mktPrice);
			mdpSocket.SendTo(sendBuffer,bcastEndPoint);
			mdpSocket.Close();
			Console.WriteLine("Market Data Broadcasted");
			Console.ReadLine();

		}
	}
}
