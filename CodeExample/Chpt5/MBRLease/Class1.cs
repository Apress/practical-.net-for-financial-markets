using System;
using System.Runtime.Remoting.Lifetime;

public class MBRLease : MarshalByRefObject
{
	public override object InitializeLifetimeService()
	{
		//Default lease associated with remote object is retrieved
		ILease objLease = (ILease)base.InitializeLifetimeService();
		//Initial Lease time is updated to 3 minute
		objLease.InitialLeaseTime=TimeSpan.FromMinutes(3);
		//Renewal time is updated to 1 minute
		objLease.RenewOnCallTime=TimeSpan.FromMinutes(1);
		return objLease;
	}

	public static void Main(string[] args)
	{
	}
}
