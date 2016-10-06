using System;
using System.Collections;
using System.Xml;
using System.Text;

class WritingXML
{
	public class ExchangeInfo
	{
		public string ExchangeCode;
		public string ScripCode;
	}

	public class ISINInfo
	{
		public string Symbol;
		public double FaceValue;
		public int MarketLot;
		public ArrayList exchangeList = new ArrayList();
	}

	[STAThread]
	static void Main(string[] args)
	{
		//initialize in-memory isin data store
		ArrayList isinList = new ArrayList();
		
		//create isin 
		ISINInfo isinInfo = new ISINInfo();
		isinInfo.Symbol ="MSFT";
		isinInfo.FaceValue = 10;
		isinInfo.MarketLot = 5;

		//create exchange 
		ExchangeInfo nasdaqInfo = new ExchangeInfo();
		nasdaqInfo.ExchangeCode = "NASDAQ";
		nasdaqInfo.ScripCode = "MSFT.O";

		//add exchange to isin exchange list
		isinInfo.exchangeList.Add(nasdaqInfo);
		//add isin to array list
		isinList.Add(isinInfo);

		//create XML text writer
		XmlTextWriter xmlWriter = new XmlTextWriter(@"ISINMaster.xml",Encoding.UTF8);
		xmlWriter.Formatting = Formatting.Indented;
		//write the root element
		xmlWriter.WriteStartElement("ISINMaster");
		//write ISINS start tag
		xmlWriter.WriteStartElement("ISINS");
		//iterate through individual isin 
		foreach(ISINInfo curIsin  in isinList)
		{
			//write isin element
			xmlWriter.WriteStartElement("ISIN");
			//write attributes of isin
			xmlWriter.WriteAttributeString("Symbol",curIsin.Symbol);
			xmlWriter.WriteAttributeString("FaceValue",XmlConvert.ToString(curIsin.FaceValue));
			xmlWriter.WriteAttributeString("MarketLot",XmlConvert.ToString(curIsin.MarketLot));
			//write parent element of exchange
			xmlWriter.WriteStartElement("Exchanges");
			//iterate through individual exchange
			foreach(ExchangeInfo curExchange in curIsin.exchangeList)
			{
				//write exchange element
				xmlWriter.WriteStartElement("Exchange");
				//write attributes of exchange
				xmlWriter.WriteAttributeString("Code",curExchange.ExchangeCode);
				xmlWriter.WriteAttributeString("ScripCode",curExchange.ScripCode);
				xmlWriter.WriteEndElement();
			}
			//exchange end tag
			xmlWriter.WriteEndElement();
			//exchanges end tag
			xmlWriter.WriteEndElement();
		}
		//ISINS end tag
		xmlWriter.WriteEndElement();
		//root end tag
		xmlWriter.WriteEndElement();
		//close xml text writer
		xmlWriter.Close();
	}
}
