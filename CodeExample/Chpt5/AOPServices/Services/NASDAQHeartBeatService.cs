using System;

namespace AOPServices.Services
{
	public class NASDAQHeartBeatService
	{
		public NASDAQHeartBeatService()
		{
		}

		public virtual void Start()
		{
			//Exchange Specific Operation
			Console.WriteLine("HeartBeat Service Started");
		}

		public virtual void Stop()
		{
			//Exchange Specific Operation
			Console.WriteLine("HeartBeat Service Stopped");
		}
	}
}
