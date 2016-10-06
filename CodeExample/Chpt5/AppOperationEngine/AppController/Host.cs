using System;
using Common;
using System.Runtime.Remoting;

namespace AppController
{
	class Host
	{
		[STAThread]
		static void Main(string[] args)
		{
			//Start Primary Controller 
			PrimaryController primaryController = new PrimaryController();
			RemotingConfiguration.Configure(@"AppController.exe.config");	
			RemotingServices.Marshal(primaryController ,"PrimaryController.ref",typeof(PrimaryController));
			primaryController.Start();
			Console.WriteLine("Primary Controller Started");
			//Access trading agent and invoke the application management service
			Console.WriteLine("Starting App Management Service..");
			AgentInfo agentInfo =  primaryController["tcp://localhost:20001/TradingEngineAgent.rem"];
			DomainApp omeApp =  agentInfo.Applications["Order Matching"] as DomainApp;
			omeApp.AppManagement.Start();
			Console.ReadLine();
		}
	}
}
