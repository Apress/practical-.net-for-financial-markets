using System;

namespace STP.Security
{
	//Secure envelope is composed of multiple sections
	//and each section is represented by this class.
	//For example, if a data supports both encryption and 
	//digital signature then it will produce two different output 
	//and both this output will be stored in a distinct
	//section of an envelope.
	[Serializable]
	public class SectionData 
	{
		public byte[] secData;

		public byte[] Data
		{
			get{return secData;}
		}

		public SectionData(byte[] data)
		{
			secData=data;
		}


	}
}
