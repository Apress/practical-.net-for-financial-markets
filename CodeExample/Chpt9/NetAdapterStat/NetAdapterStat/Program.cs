using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace NetAdapterStat
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            
            foreach (NetworkInterface adapter in nics)
            {
                Console.WriteLine("Adapter Name : " +adapter.Name);
                Console.WriteLine("Physical Address : " + adapter.GetPhysicalAddress().ToString());
                Console.WriteLine("Data Transfer Speed :" + adapter.Speed +" bits per second ") ;
                Console.WriteLine("Operational Status :" + adapter.OperationalStatus);
                Console.WriteLine("");
            }

        }
    }
}
