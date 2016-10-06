using System;
using System.IO;

namespace BinaryExample
{
	struct ISINRecord
	{
		public string isinCode;
		public char securityType;
		public double faceValue;
		public long lotSize;
	}
	class BinaryExample
	{
		[STAThread]
		static void Main(string[] args)
		{
			string filePath = @"isin.dat";
			
			//Initialize the ISIN data
			ISINRecord newRecord = new ISINRecord();
			newRecord.isinCode = "US5949181045";
			newRecord.faceValue = 10;
			newRecord.lotSize = 100;

			//Open binary file for writing
			FileStream fStream = new FileStream(filePath,FileMode.CreateNew,FileAccess.Write);

			//Create a binary writer
			BinaryWriter bwrt = new BinaryWriter(fStream);
			
			//write isin data
			bwrt.Write(newRecord.isinCode);
			bwrt.Write(newRecord.securityType);
			bwrt.Write(newRecord.faceValue);
			bwrt.Write(newRecord.lotSize);

			//Close the stream
			fStream.Close();

			ISINRecord isinRecord; 
			//Open the binary file
			fStream = new FileStream(filePath,FileMode.Open,FileAccess.Read);
			//Create a binary reader 
			BinaryReader br = new BinaryReader(fStream);
			//read isin code
			isinRecord.isinCode= br.ReadString();
			//read security type
			isinRecord.securityType= br.ReadChar();
			//read face value
			isinRecord.faceValue= br.ReadDouble();
			//read lot size
			isinRecord.lotSize = br.ReadInt32();
		}
	}
}
