using System;

namespace STP.Security
{
	//The information about cryptography implementation 
	//used to achieve data integrity, non-repudiation and confidential 
	//are stored in a XML file or database and are identified
	//by profile name
	[AttributeUsage(AttributeTargets.Class)]	
	public class SecurityProfileAttribute : Attribute
	{
		private string profileName;
		public SecurityProfileAttribute(string name)
		{
			profileName=name;
		}

		public string Profile
		{
			get{return profileName;}
		}
	}
}
