using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace NetPing
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Ping pingSender = new Ping();

            //Ping apress web site with timeouf of 1 seconds (1000 milliseconds)
            //The result of ping is stored in instance of PingReply
            PingReply reply =  pingSender.Send("www.apress.com",1000);
            
            //Analyze the result
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Roundtrip Time : " + reply.RoundtripTime);
            }
            
        }
    }
}
