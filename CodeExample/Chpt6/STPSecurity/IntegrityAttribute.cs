using System;

namespace STP.Security
{
	//This attribute is annotated at class level
	//to indicate data needs to be protected by 
	//computing a strong hash value
	[AttributeUsage(AttributeTargets.Class)]
	public class IntegrityAttribute : Attribute
	{
		public IntegrityAttribute()
		{
		}
	}
}
