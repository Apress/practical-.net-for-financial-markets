using System;
using System.Net;
using System.Net.Sockets;

class ProtocolTweaking
{
	[STAThread]
	static void Main(string[] args)
	{
	}

	public void IPFragment(Socket sockInstance)
	{
		//Disable the Fragmentation
		sockInstance.SetSocketOption(SocketOptionLevel.IP,
			SocketOptionName.DontFragment,1);
		//Get Assigned Fragmentation Value
		int isFragmented = 
			(int)sockInstance.GetSocketOption(SocketOptionLevel.IP,
			SocketOptionName.DontFragment);
		Console.WriteLine(isFragmented);
	}

	public void MultiCastTTL(Socket sockInstance)
	{
		//Subnet Scope
		sockInstance.SetSocketOption(SocketOptionLevel.IP,
			SocketOptionName.MulticastTimeToLive,3);
	}

	public void IPTTL(Socket sockInstance)
	{
		//Set the TTL to 4
		sockInstance.SetSocketOption(SocketOptionLevel.IP,
			SocketOptionName.IpTimeToLive,4);
		int ipTTL= (int)sockInstance.GetSocketOption(SocketOptionLevel.IP,
			SocketOptionName.IpTimeToLive);
		Console.WriteLine(ipTTL);
	}

	public void DisableMulticastLoopBack(Socket sockInstance)
	{
		//disable multicast loopback
		sockInstance.SetSocketOption(SocketOptionLevel.IP,
			SocketOptionName.MulticastLoopback,0);
	}

	public void ReuseSocket(Socket sockInstance)
	{
		sockInstance.SetSocketOption(SocketOptionLevel.Socket,
			SocketOptionName.ReuseAddress,1);
	}

	public void SetBufferSize(Socket sockInstance,int recvBuffer,int sendBuffer)
	{
		sockInstance.SetSocketOption(SocketOptionLevel.Socket,
			SocketOptionName.SendBuffer,sendBuffer);
		sockInstance.SetSocketOption(SocketOptionLevel.Socket,
			SocketOptionName.ReceiveBuffer,recvBuffer);
	}

	public void SetTimeOut(Socket sockInstance,int recvBuffer,int sendBuffer)
	{
		sockInstance.SetSocketOption(SocketOptionLevel.Socket,
			SocketOptionName.SendTimeout,10);
		sockInstance.SetSocketOption(SocketOptionLevel.Socket,
			SocketOptionName.ReceiveTimeout,10);
	}

	public void DisableNagle(Socket sockInstance)
	{
		sockInstance.SetSocketOption
			(SocketOptionLevel.Tcp,SocketOptionName.NoDelay,1);
	}
 

}
