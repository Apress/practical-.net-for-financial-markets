using System;
using System.Net;
using System.Net.NetworkInformation;

namespace NetStat
{
    class Program
    {
        static void Main(string[] args)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            Console.WriteLine("Domain Name : " + properties.DomainName);
            Console.WriteLine("Host Name : " + properties.HostName);
            
            //Get Active TCP Connections
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

            //Get Active TCP Listener
            IPEndPoint[] endPointsTCP= properties.GetActiveTcpListeners();

            //Get Active UDP Listener
            IPEndPoint[] endPointsUDP = properties.GetActiveUdpListeners();

            //Get IP statistics information
            IPGlobalStatistics ipstat = properties.GetIPv4GlobalStatistics();

            //Get TCP statistical information
            TcpStatistics tcpstat = properties.GetTcpIPv4Statistics();
            
            //Get UDP statistical information
            UdpStatistics udpStat = properties.GetUdpIPv4Statistics();
        }
    }
}
