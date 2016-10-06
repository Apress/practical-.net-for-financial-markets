using System;
using System.Collections;

namespace STP.Security
{
	//This class holds data produced by 
	//applying cryptographic transformation on original data
	[Serializable]
	public class SecureEnvelope
	{
		string profileName;
		Hashtable sectionList = new Hashtable();
		
		public Hashtable Sections
		{
			get{return sectionList;}
		}

		public string Profile
		{
			get{return profileName;}
		}

		public SecureEnvelope(string profile)
		{
			profileName=profile;
		}
	}
}
