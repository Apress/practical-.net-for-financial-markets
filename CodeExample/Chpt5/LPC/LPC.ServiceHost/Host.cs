using System;
using System.Reflection;
using LPC.Common;


namespace LPC.ServiceHost
{
	class Host
	{
		[STAThread]
		static void Main(string[] args)
		{
			//HeartBeat Service is launched in a new application domain 
			LogicalProcess serviceProcess = new LogicalProcess("HeartBeatService","LPC.Services.dll.config");
			serviceProcess.Start();
			//The meta information about service is retrieved
			//and stored in instance of ServiceInfo. Although this call is processed
			//in a callee application domain, but the result is marshalled-by-value
			//in caller application domain
			ServiceInfo srvInfo = serviceProcess.ProcessInfo;
			//The meta information about service is displayed
			Console.WriteLine("Service Info");
			Console.WriteLine("------------");
			Console.WriteLine("Name : " +srvInfo.FriendlyName);
			Console.WriteLine("Description : " +srvInfo.Description);
			Console.WriteLine("Press any key to Stop Service");
			Console.ReadLine();
			serviceProcess.Stop();
		}
	}
}
