using System;
using System.Net;

namespace HostTranslator
{
	class Translator
	{
		[STAThread]
		static void Main(string[] args)
		{
			//Get Local Host Name
			string hostName = Dns.GetHostName();
			Console.WriteLine("Local HostName : " +hostName);
			//Ask user to enter IP Address or Host Name
			Console.Write("Enter IP Address or Host Name : ");
			string hostOrip =Console.ReadLine();
			//Resolve the host/ip address 
			IPHostEntry entry = Dns.Resolve(hostOrip );
			Console.WriteLine("HostName : " +entry.HostName);
            //Get the IP address list that resolves to the host names 
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
