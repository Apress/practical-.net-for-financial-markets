using System;
using System.Runtime.Remoting.Lifetime;

class DefaultLease
{
	[STAThread]
	static void Main(string[] args)
	{
		//Change the default lease time
		LifetimeServices.LeaseTime = TimeSpan.FromMinutes(3);
		//Change the default lease renewal time
		LifetimeServices.RenewOnCallTime = TimeSpan.FromMinutes(4);
	}
}
