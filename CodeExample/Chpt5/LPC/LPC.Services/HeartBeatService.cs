using System;
using System.Configuration;
using System.Threading;
using LPC.Common;

namespace LPC.Services
{
	public class HeartBeatService : MarshalByRefObject,IService
	{
		bool stopFlag=true;
		int hbInterval;
		
		public HeartBeatService()
		{
			//Heart beat interval is read from application configuration file
			//i.e. the value is read from LPC.Services.dll.config
			hbInterval = Convert.ToInt32(ConfigurationSettings.AppSettings["HeartBeatInterval"]);
		}
		
		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService ();
		}

		public void Start()
		{
			while(stopFlag)
			{
				Console.WriteLine("Checking HeartBeat");
				Thread.Sleep(hbInterval);
			}
		}

		public ServiceInfo QueryServiceInfo()
		{
			//This method publish meta-information about service
			ServiceInfo srvInfo = new ServiceInfo();
			srvInfo.FriendlyName = "Service HeartBeat Service";
			srvInfo.Description = "Checks HeartBeat of services at a regular interval of 2 seconds";
			return srvInfo;
		}

		public void Stop()
		{
			stopFlag = false;
		}
	}
}
