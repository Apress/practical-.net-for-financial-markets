using System;

namespace STP.Security
{
	//Orchestrates the cryptography process
	public class DataSecurity
	{
		Type objType;
		DataSecurityManager securityMgr;
		Provider nonrepProvider;
		bool isConfidential;
		bool isNonRepudiation;
		bool isIntegrity;
		ProfileInfo profInfo;

		public DataSecurity(DataSecurityManager mgr, Type type)
		{
			objType = type;
			securityMgr=mgr;
			ExtractAttributes();
		}

		private void ExtractAttributes()
		{
			//Retrieve the security profile attribute
			//to retrieve the name of the profile
			object[] attributes = objType.GetCustomAttributes(typeof(SecurityProfileAttribute),true);
			SecurityProfileAttribute profAttr = attributes[0] as SecurityProfileAttribute;
			profInfo= securityMgr.Profiles[profAttr.Profile] as ProfileInfo;
			
			//Check for confidential attribute
			attributes = objType.GetCustomAttributes(typeof(ConfidentialAttribute),true);
			isConfidential = (attributes.Length == 0 ? false : true);

			//Check for non-repudiation attribute
			attributes = objType.GetCustomAttributes(typeof(NonRepudiationAttribute),true);
			isNonRepudiation  = (attributes.Length == 0 ? false : true);

			//Check for integrity attribute
			attributes = objType.GetCustomAttributes(typeof(IntegrityAttribute),true);
			isIntegrity  = (attributes.Length == 0 ? false : true);

			//Instantiate the non repudation provider 
			//and pass on the profile information
			nonrepProvider = new NonRepudiationProvider(profInfo);
		}

		public SecureEnvelope Create(byte[] data)
		{
			//Create a new secure envelope
			SecureEnvelope envelope = new SecureEnvelope(profInfo.ProfileName);
			
			//Based on attribute declared, we instantiate
			//appropriate provider 
			if ( isNonRepudiation == true ) 
				nonrepProvider.Create(data,envelope);
			return envelope;
		}

		public bool Verify(SecureEnvelope envelope)
		{
			//invoke the appropriate provider to verify data
			return nonrepProvider.Verify(envelope);
		}

	}
}
