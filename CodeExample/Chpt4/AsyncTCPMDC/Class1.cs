using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace AsyncTCPMDC
{
	public class AsyncStateInfo
	{
		public Socket socket;
		public byte[] dataBuffer = new byte[512];
		public AsyncStateInfo(Socket sock)
		{
			socket=sock;
		}
	}
	class MDC
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Market-Data Consumer Service Started -Using TCP(Async Model)");
			Console.WriteLine("Main Thread : " +Thread.CurrentThread.GetHashCode());
			IPEndPoint mdpEP = new IPEndPoint(IPAddress.Loopback,20000);
			Socket mdcSocket = new
				Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			AsyncStateInfo stateInfo = new AsyncStateInfo(mdcSocket);
			//Begins an asynchronous connection request
			mdcSocket.BeginConnect(mdpEP,new AsyncCallback(MDCConnected),stateInfo);
			Console.ReadLine();
			if ( mdcSocket.Connected == true )
			{
				mdcSocket.Shutdown(SocketShutdown.Both);
				mdcSocket.Close();
			}
		}
		//Callback method invoked as a result of asynchronous data receive request
		public static void ReceiveData(IAsyncResult result)
		{
			Console.WriteLine("Receiving Thread : "
				+Thread.CurrentThread.GetHashCode());
			AsyncStateInfo stateInfo = result.AsyncState as AsyncStateInfo;
			Socket mdcSocket = stateInfo.socket;
			//Successfully accepts data
			int bytesReceived = mdcSocket.EndReceive(result);
			if ( bytesReceived > 0 )
			{
				string mktPrice =
					Encoding.ASCII.GetString(stateInfo.dataBuffer,0,bytesReceived);
				Console.WriteLine(mktPrice);
				//Begins async. operation to receive more data sent by server
				mdcSocket.BeginReceive(stateInfo.dataBuffer,0,512,SocketFlags.None,
					new AsyncCallback(ReceiveData),stateInfo);
			}
		}
		//Callback method invoked as a result of asynchronous connection request
		public static void MDCConnected(IAsyncResult result)
		{
			Console.WriteLine("Connecting Thread : "
				+Thread.CurrentThread.GetHashCode());
			AsyncStateInfo stateInfo = result.AsyncState as AsyncStateInfo;
			Socket mdcSocket = stateInfo.socket;
			//Successfully connects to market data server
			mdcSocket.EndConnect(result);
			//Begins asynchronous data receive operation
			mdcSocket.BeginReceive(stateInfo.dataBuffer,0,512,SocketFlags.None,
				new AsyncCallback(ReceiveData),stateInfo);
		}
	}
}