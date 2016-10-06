using System;
using System.Text;
using System.Security.Cryptography;

namespace DigitalSignature
{
	class DigSign
	{
		[STAThread]
		static void Main(string[] args)
		{
			
			string contractNote = "<CONTRACTNOTE>" 
				+"<SYMBOL>MSFT</SYMBOL>" 
				+"<QUANTITY>100</QUANTITY>"
				+"<PRICE>24</PRICE>"
				+"</CONTRACTNOTE>";

			//perform digital signature using RSA 
			RSACryptoServiceProvider rsCrypto = new RSACryptoServiceProvider();
			//export of private key
			RSAParameters privateRSA = rsCrypto.ExportParameters(true);
			//export of public key
			RSAParameters publicRSA = rsCrypto.ExportParameters(false);
			byte[] contentBuffer = Encoding.ASCII.GetBytes(contractNote);
			//compute digital signature of contract note using broker private key
			byte[] signedData = SignDataBroker(contentBuffer,privateRSA);
			//verify digital signature
			bool hashResult = VerifySignFM(contentBuffer,signedData,publicRSA) ;
			Console.WriteLine ( "Hash Result : " + hashResult);
		}


		public static byte[] SignDataBroker(byte[] data,RSAParameters privateRSA)
		{
			//create RSA provider and initialize it with broker private key
			RSACryptoServiceProvider rsCrypto = new RSACryptoServiceProvider();
			rsCrypto.ImportParameters(privateRSA);
			//rsCrypto.ImportParameters(publicRSA);
			//compute hash value of contract note
			HashAlgorithm hashAlgo = new SHA1Managed();
			byte[] hashedData = hashAlgo.ComputeHash(data);
			//sign hash value using private key 
			string shaOID = CryptoConfig.MapNameToOID("SHA1");
			return rsCrypto.SignHash(hashedData,shaOID);
		}

		public static bool VerifySignFM(byte[] data,byte[] signedData,RSAParameters publicRSA)
		{
			//create RSA provider and initialize it with broker public key
			RSACryptoServiceProvider rsCrypto = new RSACryptoServiceProvider();
			rsCrypto.ImportParameters(publicRSA);
			//re-compute hash value of contract note
			HashAlgorithm hashAlgo = new SHA1Managed();
			byte[] hashedData = hashAlgo.ComputeHash(data);
			string shaOID = CryptoConfig.MapNameToOID("SHA1");
			//verify the computed hash value with the digital signature 
			return rsCrypto.VerifyHash(hashedData,shaOID,signedData);
			
		}

	}
}
