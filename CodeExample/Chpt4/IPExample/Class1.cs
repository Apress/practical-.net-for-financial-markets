using System;
using System.Net;

namespace HostTranslator
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			string hostName = Dns.GetHostName();
			Console.WriteLine("Local HostName : " +hostName);
			Console.Write("Enter IP Address or Host Name : ");
			string hostOrip =Console.ReadLine();
			IPHostEntry entry = Dns.Resolve(hostOrip );
			Console.WriteLine("HostName : " +entry.HostName);
			foreach(IPAddress address in entry.AddressList)
			{
				Console.WriteLine("IP Address : " +address.ToString());
				byte[] addressBytes = address.GetAddressBytes();
				for(int ctr=0;ctr<addressBytes.Length;ctr++)
				{
					Console.WriteLine("Byte : " +ctr +" : " +addressBytes[ctr]);
				}
			}
			
		}
	}
}
