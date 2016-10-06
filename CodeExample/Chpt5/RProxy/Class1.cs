using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;

class RProxy
{
	[STAThread]
	static void Main(string[] args)
	{
		object mbrObj=null;
		
		//Get a reference to mbrObj
		//mbrObj = <MBR Object>

		//Real Proxy instance
		RealProxy rp=RemotingServices.GetRealProxy(mbrObj);
	}
}
