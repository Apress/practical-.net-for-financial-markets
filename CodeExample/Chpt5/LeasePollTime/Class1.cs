using System;
using System.Runtime.Remoting.Lifetime;

class LeasePollTime
{
	[STAThread]
	static void Main(string[] args)
	{
		LifetimeServices.LeaseManagerPollTime=TimeSpan.FromSeconds(20);
	}
}
