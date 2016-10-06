using System;
using System.Reflection;
using System.Threading;
using LPC.Common;

namespace LPC.ServiceHost
{
	public class LogicalProcess
	{
		AppDomain appDomain;
		Thread appThread;
		IService serviceProxy;

		public LogicalProcess(string serviceName,bool shadowCopy)
		{
			//Binding decision of a new application domain is dictated by
			//creating a new instance of AppDomainSetup
			AppDomainSetup domainSetup = new AppDomainSetup();
			//Assign the list of directory from which the assemblies
			//are shadow-copied.
			domainSetup.ShadowCopyDirectories = AppDomain.CurrentDomain.BaseDirectory;
			//A boolean value that indicates whether all assemblies loaded in application
			//domain are shadow-copied.
			domainSetup.ShadowCopyFiles = shadowCopy.ToString();
			//Service name by default represents application name 
			domainSetup.ApplicationName = serviceName;
			//Cache Path represents the physical location where assemblies loaded 
			//inside application domain are mirrored and then executed from this directory
			//However, in reality the assemblies are copied 
			//into CachePath\ApplicationName directory
			domainSetup.CachePath = @"C:\CacheLocation";
			string typeName = "LPC.Services." +serviceName;
			appDomain = AppDomain.CreateDomain(serviceName,null,domainSetup);
			serviceProxy = appDomain.CreateInstanceAndUnwrap("LPC.Services",typeName) as IService;
			appThread = new Thread(new ThreadStart(serviceProxy.Start));
		}

		public LogicalProcess(string serviceName)
		{
			string typeName = "LPC.Services." +serviceName;
			appDomain = AppDomain.CreateDomain(serviceName);
			serviceProxy = appDomain.CreateInstanceAndUnwrap("LPC.Services",typeName) as IService;
			appThread = new Thread(new ThreadStart(serviceProxy.Start));
		}

		public LogicalProcess(string serviceName,string configurationFile)
		{
			AppDomainSetup domainSetup = new AppDomainSetup();
			//Custom Configuration File Path
			domainSetup.ConfigurationFile = configurationFile;

			//Dervies type to be instantiated in a new appdomain
			//It is important to specify type name along with its namespace
			string typeName = "LPC.Services." +serviceName;
			//Create a new application domain 
			appDomain = AppDomain.CreateDomain(serviceName,null,domainSetup);
			//The next step is to load LPC.Services assembly in this newly created   
			//application domain and also instantiate the appropriate type. Both this task  
			//is achieved with help of CreateInstanceAndUnwrap that returns 
			//a proxy reference which is casted back to IService interface
			serviceProxy = appDomain.CreateInstanceAndUnwrap("LPC.Services",typeName) as IService;
			//After instantiating the new service, the processing of service
			//is off-loaded to a new thread. This provides same feeling of spawing
			//a new Win32 process which by default creates a new thread and executes
			//the entry point method 
			appThread = new Thread(new ThreadStart(serviceProxy.Start));
		}

		public ServiceInfo ProcessInfo
		{
			get{return serviceProxy.QueryServiceInfo();}
		}

		public void Start()
		{
			//The newly created thread begins its execution
			//i.e. invokes the Start method of the service
			appThread.Start();
		}

		public void Stop()
		{
		}

	}
}
