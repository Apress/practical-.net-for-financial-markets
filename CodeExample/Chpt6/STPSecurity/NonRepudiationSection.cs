using System;

namespace STP.Security
{
	public class NonRepudiationSection : SectionData	
	{
		byte[] signature;

		public NonRepudiationSection(byte[] data,byte[] hashedData)
		:base(data)
		{
			signature = hashedData;
		}

		public byte[] Signature
		{
			get{return signature;}
		}
	}
}
