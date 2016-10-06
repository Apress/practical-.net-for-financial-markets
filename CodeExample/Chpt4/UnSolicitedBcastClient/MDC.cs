using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UnSolicitedBcastClient
{
	class MDC
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Consumer Service Started - (Unsolicited Broadcast)");	
			byte[] receiveBuffer = new byte[512];
			IPHostEntry hostEntry = Dns.GetHostByName(Dns.GetHostName());
			EndPoint bindInfo = new IPEndPoint(hostEntry.AddressList[0],30001);
			Socket mdcSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
			mdcSocket.Bind(bindInfo);
			EndPoint endPoint = new IPEndPoint(IPAddress.Any,0);
			int bytesReceived = mdcSocket.ReceiveFrom(receiveBuffer,ref endPoint);
			IPEndPoint mdpEndPoint = (IPEndPoint)endPoint;
			string mktPrice = Encoding.ASCII.GetString(receiveBuffer,0,bytesReceived);
			Console.WriteLine("Market-Data Received : " +mktPrice);
			Console.WriteLine("Market Data Producer IP Address {0} Port {1} " ,mdpEndPoint.Address.ToString(), mdpEndPoint.Port);
			Console.ReadLine();			
			mdcSocket.Close();
		}
	}
}
