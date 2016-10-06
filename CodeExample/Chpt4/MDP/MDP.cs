using System;
using System.Collections.Specialized;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MDP
{
	class MDP
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Producer Service Started");	
			//Market Data
			string mktPrice = "MSFT;25,IBM;24";	
			//Market Data Receipient List
			EndPoint[] mdcEndPointList = new EndPoint[]{new IPEndPoint(IPAddress.Loopback,30000)};
			//Build a network data conduit
			Socket mdpSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
			//Convert the data into array of bytes
			byte[] sendBuffer = new byte[512];
			sendBuffer = Encoding.ASCII.GetBytes(mktPrice);
			//Iterate through recipient list and transmit the data 
			foreach(EndPoint mdcEndPoint in mdcEndPointList )
			{
				mdpSocket.SendTo(sendBuffer,mdcEndPoint);
			}
			Console.WriteLine("Market Data Sent to all Market-Data consumer clients");
			Console.ReadLine();
			//Free the resources
			mdpSocket.Close();
		}

		public static void IPFragment(Socket sockInstance)
		{
			//Disable the Fragmentation
			sockInstance.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.DontFragment,1);

			//Get Assigned Fragmentation Value
			int isFragmented = (int)sockInstance.GetSocketOption(SocketOptionLevel.IP,SocketOptionName.DontFragment);
			Console.WriteLine(isFragmented);

		}


	}
}