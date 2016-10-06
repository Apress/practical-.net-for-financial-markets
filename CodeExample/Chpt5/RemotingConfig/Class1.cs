using System;
using System.Runtime.Remoting;

class RemotingConfig
{
 [STAThread]
static void Main(string[] args)
{
	string configFile = null;
	//Assign valid name of the configuration file
	//configFile = "C:\RemotingConfig.config"
	
	//Configure the Remoting Infrastructure 
	RemotingConfiguration.Configure(configFile);
}
}

