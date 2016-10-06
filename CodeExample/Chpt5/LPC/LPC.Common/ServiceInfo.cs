using System;
using System.Collections;

namespace LPC.Common
{
	[Serializable]
	public class ServiceInfo
	{
		//User Friendly Name of this service specifically used to 
		//uniquely identify this service
		public string FriendlyName;
		//A very detailed description of features offered by this service
		public string Description;
		//List of dependent services
		public ArrayList DependentServices;
		//Indicates the start date and time of the service
		//useful for audit purpose
		public DateTime StartDate;
	}
}
