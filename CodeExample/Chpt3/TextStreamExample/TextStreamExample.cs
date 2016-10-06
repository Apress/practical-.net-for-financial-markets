using System;
using System.IO;

class TextStreamExample
{
	[STAThread]
	static void Main(string[] args)
	{
		//string csvFile = @"C:\CodeExample\Chpt3\TextStreamExample\CSVISINMaster.csv";
		string csvFile = @"..\..\CSVISINMaster.csv";
		//Open the CSV file
		TextReader isinReader = new StreamReader(csvFile);
		//read the entire content of the file
		string content = isinReader.ReadToEnd();
		//display content
		Console.WriteLine(content);
		//close the stream
		isinReader.Close();
	}
}
