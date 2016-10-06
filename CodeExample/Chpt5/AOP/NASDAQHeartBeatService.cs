using System;
using System.Threading;

namespace AOP
{
	/// Sends heartbeat message to NYSE 
	public class NASDAQHeartBeatService
	{
		public NASDAQHeartBeatService()
		{
		}

		public void Start()
		{
			if ( !Thread.CurrentPrincipal.IsInRole("Manager"))
				throw new ApplicationException("Access Denied");

			//Exchange Specific Operation
		}

		public void Stop()
		{
			if ( !Thread.CurrentPrincipal.IsInRole("Manager"))
				throw new ApplicationException("Access Denied");

			//Exchange Specific Operation
		}

	}
}
