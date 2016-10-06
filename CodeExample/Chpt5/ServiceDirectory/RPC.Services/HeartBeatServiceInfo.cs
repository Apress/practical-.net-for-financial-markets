using System;
using RPC.Common;

namespace RPC.Services
{
	//This remoteable class provides meta-information about heartbeart service
	public class HeartBeatServiceInfo : MarshalByRefObject, IServiceInfo
	{
		ServiceInfo srvInfo = new ServiceInfo();
		public HeartBeatServiceInfo()
		{
			srvInfo.FriendlyName = "Service HeartBeat Service";
			srvInfo.Description = "Checks HeartBeat of services at a regular interval of 2 seconds";
			//This is a important attribute because it represents 
			//the remote location of the actual heartbeat service
			srvInfo.Location = "tcp://localhost:15000/HeartBeatService.rem";
			
		}

		public ServiceInfo QueryServiceInfo
		{
			get{return srvInfo;}
		}

	}
}
