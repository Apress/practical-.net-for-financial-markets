using System;

namespace LPC.Common
{
	public interface IService
	{
		void Start();
		void Stop();
		ServiceInfo QueryServiceInfo();
	}
}
