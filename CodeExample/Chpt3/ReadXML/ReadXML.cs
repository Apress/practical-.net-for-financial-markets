using System;
using System.Collections;
using System.Xml;

class ReadXML
{
	//ISIN Domain Model
	public class ISINInfo
	{
		public string Symbol;
		public double FaceValue;
		public int MarketLot;
		public ArrayList exchangeList = 
			new ArrayList();
	}

	//Exchange Domain Model
	//that holds exchange specific instrument
	//code for a particular ISIN
	public class ExchangeInfo
	{
		public string ExchangeCode;
		public string ScripCode;
	}


	[STAThread]
	static void Main(string[] args)
	{
		//declare arraylist for our in-memory data store  
		ArrayList isinDataStore = new ArrayList();

		//ISINMaster XML path
		//string xmlPath = @"C:\CodeExample\Chpt3\ReadXML\ISINMaster.xml";
		string xmlPath = @"..\..\ISINMaster.xml";
		//Create XML text reader
		XmlTextReader txtReader = new XmlTextReader(xmlPath);

		//loop until we have read the entire file
		//returns true as long as there is content to be read
		while ( txtReader.Read() )  
		{
			//check the type of node that we just read to be an Element type
			switch(txtReader.NodeType)
			{
				case XmlNodeType.Element:
					//check the name of the current node being read
					//If ISIN node is read
					if ( txtReader.LocalName == "ISIN" )
					{
						//create an instance of the ISINInfo class and 
						//assign various properties by querying attribute 
						//nodes of ISIN elemeent
						ISINInfo isinInfo = new ISINInfo();
						isinInfo.Symbol = txtReader.GetAttribute("Symbol");
						isinInfo.FaceValue =     
							XmlConvert.ToDouble(txtReader.GetAttribute("FaceValue"));
						isinInfo.MarketLot = 
							XmlConvert.ToInt32(txtReader.GetAttribute("MarketLot"));
						isinDataStore.Add(isinInfo);
					}
					//If Exchange node is read
					if ( txtReader.LocalName == "Exchange" )
					{
						//Get reference to latest isin instance added in arraylist
						ISINInfo isinInfo = isinDataStore[isinDataStore.Count - 1] as ISINInfo;
						//create instance of exchange and assign various properties by querying
						//attribute node of exchange element
						ExchangeInfo exchInfo = new ExchangeInfo();
						exchInfo.ExchangeCode = txtReader.GetAttribute("Code");
						exchInfo.ScripCode = txtReader.GetAttribute("ScripCode");
						//add exchange instance into isin exchange list
						//reflects isin-exchange mapping
						isinInfo.exchangeList.Add(exchInfo);
					}
					break;
				default:
					break;
			}
		}
		
		//close our textreader
		txtReader.Close();

		//Display the ISIN
		foreach(ISINInfo isin in isinDataStore)
		{
			Console.WriteLine("Symbol :" +isin.Symbol);
			//Display Exchange
			foreach(ExchangeInfo exchange in isin.exchangeList)
			{
				Console.WriteLine("Exchange {0} Scrip Code {1} ",
					               exchange.ExchangeCode,exchange.ScripCode);
			}
		}

	}
}
