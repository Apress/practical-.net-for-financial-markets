using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace SymmetricAlgo
{
	class SymmetricExample
	{
		[STAThread]
		static void Main(string[] args)
		{

			//perform symmetric encryption using RijndaelManaged algorithm
			SymmetricAlgorithm algoProvider = RijndaelManaged.Create();
			Console.WriteLine("Crypto Provider Information");
			Console.WriteLine("--------------------");
			Console.WriteLine("Cipher Mode : " + algoProvider.Mode);
			Console.WriteLine("Padding Mode : " +algoProvider.Padding);
			Console.WriteLine("Block Size : " +algoProvider.BlockSize);
			Console.WriteLine("Key Size : " +algoProvider.KeySize);

			Console.WriteLine("Contract Note Encryption Stage - Broker end");
			Console.WriteLine("-------------------------------------------");
			//Generate Symmetric Key
			algoProvider.GenerateKey();
			//Generate IV
			algoProvider.GenerateIV();

			//create file which stores encrypted content of contract note
			FileStream fileStream = new FileStream(@"C:\ContractNote.enc",FileMode.Create);
			//create symmetric encryptor object
			ICryptoTransform cryptoTransform = algoProvider.CreateEncryptor();
			//create crypto stream
			CryptoStream cryptoStream = new CryptoStream(fileStream,cryptoTransform,CryptoStreamMode.Write);
			string contractNote = "<CONTRACTNOTE>" 
						          +"<SYMBOL>MSFT</SYMBOL>" 
							      +"<QUANTITY>100</QUANTITY>"
                                  +"<PRICE>24</PRICE>"
                                  +"</CONTRACTNOTE>";
			byte[] contentBuffer  = Encoding.ASCII.GetBytes(contractNote);
			//write encrypted data
			cryptoStream.Write(contentBuffer,0,contentBuffer.Length);
			cryptoStream.Close();
			fileStream.Close();
			
			Console.WriteLine("Contract Note Decryption Stage - Fund Manager end");
			Console.WriteLine("-------------------------------------------------");
			//open encrypted content of contract note
			fileStream = new FileStream(@"C:\ContractNote.enc",FileMode.Open);
			//create symmetric decryptor object
			cryptoTransform = algoProvider.CreateDecryptor();
			cryptoStream = new CryptoStream(fileStream,cryptoTransform,CryptoStreamMode.Read);
			byte[] readBuffer = new byte[fileStream.Length];
			//decrypt data
			cryptoStream.Read(readBuffer,0,readBuffer.Length);
			string decryptedText = Encoding.ASCII.GetString(readBuffer,0,readBuffer.Length);
			Console.WriteLine(decryptedText);

		}
	}
}
