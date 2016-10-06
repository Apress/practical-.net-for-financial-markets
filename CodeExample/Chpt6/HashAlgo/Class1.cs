using System;
using System.Text;
using System.Security.Cryptography;

namespace HashAlgo
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			//compute hash using SHA-1
			HashAlgorithm hashAlgo = new SHA1Managed();
			string contractNote = "<CONTRACTNOTE>" 
				                 +"<SYMBOL>MSFT</SYMBOL>" 
				                 +"<QUANTITY>100</QUANTITY>"
				                 +"<PRICE>24</PRICE>"
				                 +"</CONTRACTNOTE>";

			byte[] contentBuffer  = Encoding.ASCII.GetBytes(contractNote);
			//compute contract note hash value
			byte[] hashedData = hashAlgo.ComputeHash(contentBuffer);
			Console.WriteLine("Data Length : " +contentBuffer.Length);
			Console.WriteLine("Hashed Data Length : " +hashedData.Length);
		}
	}
}
