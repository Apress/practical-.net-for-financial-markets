using System;
using System.Xml;

class NodeNavigation
{
	[STAThread]
	static void Main(string[] args)
	{
		//ISINMaster XML path
		//string xmlPath = @"C:\CodeExample\Chpt3\ReadXML\ISINMaster.xml";
		string xmlPath = @"..\..\ISINMaster.xml";
		//Create XML text reader
		XmlTextReader txtReader = new XmlTextReader(xmlPath);
		//loop until we have read the entire file
		//returns true as long as there is content to be read
		while ( txtReader.Read() )  
		{
			switch(txtReader.NodeType)
			{
				case XmlNodeType.Element:
					//If Exchange node is read
					if ( txtReader.LocalName == "Exchange" )
					{
						//Iterate through all attributes of exchange element
						for(int ctr=0;ctr<=txtReader.AttributeCount-1;ctr++)
						{
							//Move to attribute with specified index
							txtReader.MoveToAttribute(ctr);
							
							//display additional unwanted attribute
							if ( !(txtReader.Name == "Code" || txtReader.Name == "ScripCode")) 
							{
								Console.WriteLine("Unknown Attribute Node " +txtReader.Name +" Found ");
								Console.WriteLine("Attribute Value : " +txtReader.Value);
							}
						}
					}
					break;
				default:
					break;
			}
		}
	}
}
