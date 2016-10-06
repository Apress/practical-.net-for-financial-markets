using System;

namespace STP.Security
{
	//This attribute is annotated at class level
	//to indicate data needs to be protected by 
	//applying digital signature algorithm
	[AttributeUsage(AttributeTargets.Class)]	
	public class NonRepudiationAttribute: Attribute
	{
		public NonRepudiationAttribute()
		{
		}
	}
}
