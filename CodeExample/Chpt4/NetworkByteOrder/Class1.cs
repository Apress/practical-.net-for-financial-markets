using System;
using System.Net;

namespace NetworkByteOrder
{
	class NBO
	{
		[STAThread]
		static void Main(string[] args)
		{
			short quantity = 99;
			short networkOrder = IPAddress.NetworkToHostOrder(quantity);
			Console.WriteLine("Quantity Converted to Network Byte Order :" +networkOrder);
			short hostOrder = IPAddress.HostToNetworkOrder(networkOrder);
			Console.WriteLine("Quantity Converted to Host Byte Order :" +hostOrder );
			Console.ReadLine();
		}
	}
}
