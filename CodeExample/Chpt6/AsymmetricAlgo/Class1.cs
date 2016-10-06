using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;


namespace AsymmetricAlgo
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			//Generate public and private key
			GenerateKeyPair();
			//encrypt contract note using FM public key
			ContractNoteBroker();
			//decrypt contract note encrypted by broker using FM private key
			ContractNoteFM();
		}

		public static void GenerateKeyPair()
		{
			//perform asymmetric enryption and decryption using RSA algorithm
			RSACryptoServiceProvider cryptoProv = new RSACryptoServiceProvider();
			
			//extract public key
			string publicKey = cryptoProv.ToXmlString(false);
			//extract private key
			string privateKey = cryptoProv.ToXmlString(true);
			
			//persist private key
			StreamWriter writer = new StreamWriter(@"C:\PrivateKey.xml");
			writer.Write(privateKey);
			writer.Close();

			//persist public key
			writer = new StreamWriter(@"C:\PublicKey.xml");
			writer.Write(publicKey);
			writer.Close();
		}

		public static void ContractNoteBroker()
		{
			Console.WriteLine("Contract Note Encryption Stage - Broker end");
			//parameters passed to cryptographic service provider
			CspParameters param = new CspParameters();
			param.Flags = CspProviderFlags.UseDefaultKeyContainer;

			//read public key and initialize RSA with FM public key
			RSACryptoServiceProvider cryptoProv = new RSACryptoServiceProvider(param);
			StreamReader reader = new StreamReader(@"C:\PublicKey.xml");
			cryptoProv.FromXmlString(reader.ReadToEnd());

			string contractNote = "<CONTRACTNOTE>" 
				+"<SYMBOL>MSFT</SYMBOL>" 
				+"<QUANTITY>100</QUANTITY>"
				+"<PRICE>24</PRICE>"
				+"</CONTRACTNOTE>";

			byte[] contentBuffer  = Encoding.ASCII.GetBytes(contractNote);

			//encrypt contract note using public key and write it to a file
			FileStream fileStream = new FileStream(@"C:\ContractNote.enc",FileMode.Create);
			byte[] encContent = cryptoProv.Encrypt(contentBuffer ,false);
			fileStream.Write(encContent,0,encContent.Length);
			fileStream.Close();

		}

		public static void ContractNoteFM()
		{
			Console.WriteLine("Contract Note Decryption Stage - Fund Manager end");
			
			//parameters passed to cryptographic service provider
			CspParameters param = new CspParameters();
			param.Flags = CspProviderFlags.UseDefaultKeyContainer;
			RSACryptoServiceProvider cryptoProv = new RSACryptoServiceProvider(param);
			//initialize RSA with private key
			StreamReader reader = new StreamReader(@"C:\PrivateKey.xml");
			//StreamReader reader = new StreamReader(@"C:\PublicKey.xml");
			cryptoProv.FromXmlString(reader.ReadToEnd());
			reader.Close();

			//decrypt the encrypted contract note using private key
			FileStream fileStream = new FileStream(@"C:\ContractNote.enc",FileMode.Open);
			byte[] readBuffer = new byte[fileStream.Length];
			fileStream.Read(readBuffer,0,readBuffer.Length);
			byte[] decContent = cryptoProv.Decrypt(readBuffer,false);
			string contractNote = Encoding.ASCII.GetString(decContent);
			Console.WriteLine(contractNote);
		}
	}
}
