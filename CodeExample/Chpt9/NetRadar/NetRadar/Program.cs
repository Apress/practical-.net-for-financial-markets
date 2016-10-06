using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace NetRadar
{
    class Program
    {
        static void Main(string[] args)
        {
            //This event is raised when IP address of network is changed
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(NetworkChange_NetworkAddressChanged);

            //This event is raised when network availability is changed
            //For example when a network cable gets disconnected then application will be able to get notification
            //by subscribing to this event
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
            Console.ReadLine();
        }

        static void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            Console.WriteLine("Network Availability Changed");
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                Console.WriteLine("   {0} is {1}", n.Name, n.OperationalStatus);
            }
        }

        static void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            Console.WriteLine("IP Address Changed");
        }
    }
}
