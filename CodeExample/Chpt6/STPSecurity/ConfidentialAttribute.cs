using System;

namespace STP.Security
{
	//This attribute is annotated at class level
	//to indicate that data needs to be encrypted
	[AttributeUsage(AttributeTargets.Class)]
	public class ConfidentialAttribute : Attribute
	{
		public ConfidentialAttribute()
		{
		}
	}
}
