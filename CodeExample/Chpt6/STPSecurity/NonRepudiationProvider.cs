using System;
using System.IO;
using System.Security.Cryptography;

namespace STP.Security
{
	//Digital signature implementation 
	public class NonRepudiationProvider : Provider
	{
		RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();

		public NonRepudiationProvider(ProfileInfo profile)
		:base(profile)
		{
			//Read digital certificate information 
			StreamReader reader = new StreamReader(profile.NonRepudationKeyPath);
			string xmlContent = reader.ReadToEnd();
			rsaProvider.FromXmlString(xmlContent );
			reader.Close();
		}

		public override void Create(byte[] originalData,SecureEnvelope envelope)
		{
			//create signature
			byte[] signedData = rsaProvider.SignData(originalData,new SHA1Managed());
			//insert digital signature in secure envelope
			envelope.Sections.Add(typeof(NonRepudiationAttribute).ToString(),new NonRepudiationSection(originalData,signedData));
		}

		public override bool Verify(SecureEnvelope envelope)
		{
			//extract digital signature from secure envelope
			NonRepudiationSection nonrepSection = envelope.Sections[typeof(NonRepudiationAttribute).ToString()] as NonRepudiationSection;
			//verify digital signature 
			return rsaProvider.VerifyData(nonrepSection.Signature,new SHA1Managed(),nonrepSection.Data);
		}

	}
}
