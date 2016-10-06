using System;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

public class XMLPersist
{
	[XmlRoot("ISINMaster")]
    public class ISINDataStore
	{
		[XmlArray("ISINS")]
		[XmlArrayItem("ISIN",typeof(ISINInfo))]
		public ArrayList isinStore = new ArrayList();
	}

	public class ISINInfo
	{
		[XmlAttribute("Symbol")]
		public string Symbol;
		[XmlAttribute("FaceValue")]
		public double FaceValue;
		[XmlAttribute("MarketLot")]
		public int MarketLot;
		[XmlArray("Exchanges")]
		[XmlArrayItem("Exchange",typeof(ExchangeInfo))]
		public ArrayList exchangeList = new ArrayList();
	}

	public class ExchangeInfo
	{
		[XmlAttribute("Code")]
		public string ExchangeCode;
		[XmlAttribute("ScripCode")]
		public string ScripCode;
	}

	[STAThread]
	static void Main(string[] args)
	{
		//string isinPath = @"C:\CodeExample\Chpt3\XMLSerialization\ISINMaster.xml";
		string isinPath = @"..\..\ISINMaster.xml";
		//read isin content 
		StreamReader xmlDoc = new StreamReader(isinPath);
		
		//create a new instance of XML Serializer
		XmlSerializer isinXml = new XmlSerializer(typeof(ISINDataStore));
		//de-serialize isin master 
		ISINDataStore dataStore =  isinXml.Deserialize(xmlDoc) as ISINDataStore;
		//write isin content
		StreamWriter newXmlDoc = new StreamWriter(@"C:\NewISINMaster.xml");
		//serialize isin master
		isinXml.Serialize(newXmlDoc,dataStore);
		//close the stream
		xmlDoc.Close();
		newXmlDoc.Close();
	}
}
