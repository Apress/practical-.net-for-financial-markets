using System;
using System.IO;
using System.Text;

class StreamExample
{
	[STAThread]
	static void Main(string[] args)
	{
		//File to read
		//string csvFile = @"C:\CodeExample\Chpt3\StreamExample\CSVISINMaster.csv";
		string csvFile = @"..\..\CSVISINMaster.csv";
		//Open a file stream in read access mode
		FileStream isinStream = new FileStream(csvFile ,FileMode.Open,FileAccess.Read);
		//allocate a byte array of size 10
		byte[] byteBuffer = new byte[10];
		//read till the stream pointer reaches end of file
		while (isinStream.Position < isinStream.Length )			
		{
			//read data
			int byteRead= isinStream.Read(byteBuffer,0,byteBuffer.Length);
			//display data 
			Console.Write(Encoding.ASCII.GetString(byteBuffer,0,byteRead));
		}
		//close stream
		isinStream.Close();
	}
}
