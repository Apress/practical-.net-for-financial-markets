using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ProtocolTweakingClient
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Consumer Service Started - Using TCP");	
			IPHostEntry entry = Dns.Resolve(Dns.GetHostName());
			IPEndPoint mdpEP = new IPEndPoint(entry.AddressList[0],20000);
			Socket mdcSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			int bufferSize = (int)mdcSocket.GetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReceiveBuffer);
			Console.WriteLine(bufferSize);
			mdcSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReceiveBuffer,2000);
			bufferSize = (int)mdcSocket.GetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReceiveBuffer);
			IPFragment(mdcSocket);
			Console.WriteLine(bufferSize);
			mdcSocket.Connect(mdpEP);
			int optionLength=3;
			byte[] receiveBuffer = new byte[512];
			int bytesReceived = mdcSocket.Receive(receiveBuffer);
			Console.WriteLine(bytesReceived);
			string mktPrice = Encoding.ASCII.GetString(receiveBuffer,0,bytesReceived);
			Console.WriteLine(mktPrice);
			mdcSocket.Shutdown(SocketShutdown.Both);
			mdcSocket.Close();
			Console.ReadLine();
		}

		public static void IPFragment(Socket sockInstance)
		{
			int isFragmented = (int)sockInstance.GetSocketOption(SocketOptionLevel.IP,SocketOptionName.DontFragment);
			Console.WriteLine(isFragmented);
		}
	}
}
