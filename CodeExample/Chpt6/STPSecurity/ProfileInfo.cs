using System;

namespace STP.Security
{
	public enum IntegrityAlgo
	{
		SHA1,
		MD5
	}

	public enum ConfidentialAlgo
	{
		DES,
		Rijndael
	}


	//This class represents object-oriented representation
	//of security profile information stored in XML configuration 
	//or database
	public class ProfileInfo
	{
		IntegrityAlgo integrityAlgo;
		ConfidentialAlgo confidentialAlgo;
		string nonRepKeyPath;
		string profileName;

		public IntegrityAlgo Integrity
		{
			get{return integrityAlgo;}	
		}

		public ConfidentialAlgo Confidential
		{
			get{return confidentialAlgo;}
		}

		public string ProfileName
		{
			get{return profileName;}
		}

		public string NonRepudationKeyPath
		{
			get{return nonRepKeyPath;}
		}

		public ProfileInfo(ConfidentialAlgo confalgo,IntegrityAlgo intalgo,string nonrepKey)
		{
			confidentialAlgo=confalgo;
			integrityAlgo=intalgo;
			nonRepKeyPath= nonrepKey;
		}

	}
}
