using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MCastServer
{
	class MDP
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Producer Service Started - (Using MultiCast)");	
			string mktPrice = "MSFT;25,IBM;24";	
			
			//IP Multicast address
			IPAddress groupAddress =IPAddress.Parse("224.5.6.7");
			IPEndPoint mcastEP = new IPEndPoint(groupAddress,30002);
			Socket mdpSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
			byte[] sendBuffer = new byte[512];
			sendBuffer = Encoding.ASCII.GetBytes(mktPrice);
			
			//Set multicast TTL
			mdpSocket.SetSocketOption(SocketOptionLevel.IP,
									  SocketOptionName.MulticastTimeToLive, 3);
			//Send data to multicast address
			mdpSocket.SendTo(sendBuffer,mcastEP);
			mdpSocket.Close();
			Console.WriteLine("Market Data sent to group of consumers");
			Console.ReadLine();
		}
	}
}
