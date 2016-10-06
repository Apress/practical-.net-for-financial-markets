using System;

namespace STP.Security
{
	//This class represents abstract implementation
	//of various cryptographic features the framework
	//is going to support.
	public abstract class Provider
	{
		ProfileInfo	 profileInfo;
		
		public Provider(ProfileInfo profile)
		{
			profileInfo = profile;
		}
		//crytographic transformaton of outgoing data
		public abstract void Create(byte[] originalData,SecureEnvelope envelope);
		//crytographic transformaton of incoming data
		public abstract bool Verify(SecureEnvelope envelope);
	}
}
