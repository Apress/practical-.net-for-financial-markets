using System;
using System.Runtime.Remoting.Lifetime;

namespace RPC.Services
{
	public class BODEODSponsor : MarshalByRefObject, ISponsor
	{
		public BODEODSponsor()
		{
		}
		public TimeSpan Renewal(ILease lease)
		{
			//The logic here clearly determines the object lifetime based on
			//trading hours
			int tradingBod=9;
			int tradingEod=16;
			DateTime bodTime = DateTime.Now;
			if ( bodTime.Hour >= tradingBod && bodTime.Hour <= tradingEod)
			{
				DateTime eodTime =
					new DateTime(bodTime.Year,bodTime.Month,bodTime.Day,tradingEod,5,0);
				TimeSpan diffTime = eodTime-bodTime;
				Console.WriteLine(diffTime.TotalMinutes);
				return diffTime.TotalMinutes > 0 ? diffTime : TimeSpan.Zero;
			}
			return TimeSpan.Zero;
		}
		
		static void Main(string[] args)
		{
		}
	}
}