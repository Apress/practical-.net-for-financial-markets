using System;
using System.Threading;
using Common;

namespace OrderMatching
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			DomainApp serviceApp = AppDomain.CurrentDomain.GetData("SERVICE_DOMAINAPP") as DomainApp;
			serviceApp.Logger.Log("Order Matching Started");
		}
	}
}
