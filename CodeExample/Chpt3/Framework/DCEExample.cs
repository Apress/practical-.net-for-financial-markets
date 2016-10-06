using System;
using System.IO;
using System.Configuration;
using System.Xml.Serialization;
using DCE.Repository;
using DCE.Parser;
using DCE.Writer;
using DCE;

namespace DCE
{
	class DCEExample
	{
		[STAThread]
		static void Main(string[] args)
		{
			string filePath = @"..\..\";
			//Assign the framework rule schema
			String ruleSchema = filePath +"RuleSchema.xsd";
			//ISIN Master - comma separated
			BooleanCursor dataRdr = new BooleanCursor(new StreamReader(filePath +"CSVISINMaster.csv"));
			//Create XML Data Writer
			XMLDataWriter dataWrt= new XMLDataWriter(new StringWriter());
			//Instantiate Data Converter passing the ISIN Conversion rule file
			//DataConverter _dataConverter= new DataConverter(filePath +"ISINConversionRule.xml",ruleSchema );
			DataConverter _dataConverter= new DataConverter(@"..\..\ISINComplexRule.xml",ruleSchema );
			//Start of conversion phase
			_dataConverter.Convert(dataRdr,dataWrt);
			//Display XML output
			Console.WriteLine(dataWrt.BaseWriter.ToString());
		}
	}
}
