using System;
using System.Collections.Specialized;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MDC
{
	class MDC
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Consumer Service Started");	
			byte[] receiveBuffer = new byte[512];
			EndPoint bindInfo = new IPEndPoint(IPAddress.Loopback,30000);
			Socket mdcSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
			try
			{
				//Associates socket with a particular local endpoint
				mdcSocket.Bind(bindInfo);
				EndPoint endPoint = new IPEndPoint(IPAddress.Any,0);
				//receives a datagram, and the call blocks until data is received
				int bytesReceived = mdcSocket.ReceiveFrom(receiveBuffer,ref endPoint);
				//market data sender information is recorded
				IPEndPoint mdpEndPoint = (IPEndPoint)endPoint;
				string mktPrice = Encoding.ASCII.GetString(receiveBuffer,0,bytesReceived);
				Console.WriteLine("Market-Data Received : " +mktPrice);
				Console.WriteLine("Market Data Producer IP Address {0} Port {1} " ,mdpEndPoint.Address.ToString(), mdpEndPoint.Port);
			}
			catch(SocketException e)
			{
				Console.WriteLine(e.ToString());
			}
			Console.ReadLine();			
			mdcSocket.Close();
			
		}
	}
}
