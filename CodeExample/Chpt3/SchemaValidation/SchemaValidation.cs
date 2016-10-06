using System;
using System.Xml;
using System.Xml.Schema;

class SchemaValidation
{
	public static bool isDocumentValid=true;
	[STAThread]
	static void Main(string[] args)
	{
		//string path = @"C:\CodeExample\Chpt3\SchemaValidation\";
		string path = @"..\..\";
		//read isin xml 
		XmlTextReader reader = new XmlTextReader(path +"ISINMaster.xml");
		//create schema validator
		XmlValidatingReader validateReader = new XmlValidatingReader(reader);
		//associate validation event handler
		validateReader.ValidationEventHandler +=new System.Xml.Schema.ValidationEventHandler(ValidationEventHandler);
		//add schema file path
		validateReader.Schemas.Add("",path +"ISINSchema.xsd");
		
		//validate the xml file with xsd
		while(validateReader.Read())
		{
			if ( isDocumentValid == false ) 
				break;
		}
		
		//check boolean value, the value of this variable
		//is assigned in validation handler code
		if ( isDocumentValid == true ) 
		{
			Console.WriteLine("Document is Valid...");
		}
	}

	private static void ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
	{
		//error in xml document
		isDocumentValid=false;
		//display the error message
		Console.WriteLine(e.Message);
	}
}
