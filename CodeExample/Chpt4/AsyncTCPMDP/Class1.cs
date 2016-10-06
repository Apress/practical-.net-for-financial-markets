using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace AsyncTCPMDP
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
	class MDP
	{
		[STAThread]
		static void Main(string[] args)
		{
			ManualResetEvent shutDownSignal = new ManualResetEvent(false);
			IPEndPoint localEP = new IPEndPoint(IPAddress.Loopback,20000);
			Console.WriteLine("Market-Data Producer Service Started –Using TCP (Async Model)");
			Console.WriteLine("Main Thread : " +Thread.CurrentThread.GetHashCode());
			Socket mdpSocket = new
				Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			mdpSocket.Bind(localEP);
			mdpSocket.Listen(10);
			//Starts listening to client connection in an asynchronous mode
			mdpSocket.BeginAccept(new AsyncCallback(AcceptConnection),
				new AsyncStateInfo(mdpSocket));
			shutDownSignal.WaitOne();
		}
		//Method invoked to accept an incoming connection attempt
		public static void AcceptConnection(IAsyncResult result)
		{
			Console.WriteLine("Connection Request Thread : "+Thread.CurrentThread.GetHashCode());
			AsyncStateInfo stateInfo = result.AsyncState as AsyncStateInfo;
			//Accepts client connection
			Socket mdcSocket = stateInfo.socket.EndAccept(result);
			//Starts listening to client connection
			stateInfo.socket.BeginAccept(new AsyncCallback(AcceptConnection),
				new AsyncStateInfo(stateInfo.socket));
			AsyncStateInfo mdcStateInfo = new AsyncStateInfo(mdcSocket);
			string mktPrice = "MSFT;25,IBM;24";
			byte[] dataBuf = Encoding.ASCII.GetBytes(mktPrice);

			//copy data buffer
			Buffer.BlockCopy(dataBuf,0,mdcStateInfo.dataBuffer,0,dataBuf.Length);

			//Sends data asynchronously
			mdcSocket.BeginSend(mdcStateInfo.dataBuffer,0,512,SocketFlags.None,
				new AsyncCallback(SendData),mdcStateInfo);
		}
		public static void SendData(IAsyncResult result)
		{
			Console.WriteLine("Data Sending Thread : "
				+Thread.CurrentThread.GetHashCode());
			AsyncStateInfo stateInfo = result.AsyncState as AsyncStateInfo;
			//Completes asynchronous send
			stateInfo.socket.EndSend(result);
		}
	}
}