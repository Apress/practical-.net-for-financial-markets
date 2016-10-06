using System;
using System.Runtime.Remoting;
using System.Collections;

namespace RPC.Common
{
	[Serializable]
	public class ServiceInfo
	{
		public string FriendlyName;
		public string Description;
		public ArrayList DependentServices;
		public string Location;
	}
}
