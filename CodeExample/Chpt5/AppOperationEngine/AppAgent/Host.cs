using System;
using System.Runtime.Remoting;

namespace AppAgent
{
	class Host
	{
		[STAThread]
		static void Main(string[] args)
		{
			RemotingConfiguration.Configure(@"AppAgent.exe.config");	
			Console.WriteLine("Primary Controller Agent Started...");
			Console.ReadLine();
		}
	}
}
