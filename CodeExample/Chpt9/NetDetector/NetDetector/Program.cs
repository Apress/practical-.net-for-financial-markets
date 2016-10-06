using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace NetDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            //This event is raised when IP address of network is changed
            NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;

            //This event is raised when network availability is changed
            //For example application will be able to get notification about network cable disconnect 
            //by subscribing to this event
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            Console.ReadLine();
        }

        static void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            Console.WriteLine("Network Disconnected");
        }

        static void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            Console.WriteLine("IP Address Changed");
        }
    }
}
