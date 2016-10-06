using System;
using System.Collections;

namespace STP.Security
{
	//Class responsible for loading security profiles
	//from XML configuration file or database
	public class DataSecurityManager
	{
		Hashtable profileCollection = new Hashtable();

		public DataSecurityManager()
		{
			profileCollection["BrokerA"] = 
				new ProfileInfo(ConfidentialAlgo.Rijndael,IntegrityAlgo.SHA1,@"C:\PubPrivKey.txt");
		}

		public Hashtable Profiles
		{
			get{return profileCollection;}
		}

		public DataSecurity Secure(Type objType)
		{
			return new DataSecurity(this,objType);
		}

	}
}
