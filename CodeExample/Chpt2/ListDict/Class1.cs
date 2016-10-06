using System;
using System.Collections.Specialized;

	class ListDict
	{
		class Order
		{
			public string Instrument;
			public Order(string inst)
			{
				Instrument = inst;
			}
		}
		static void Main(string[] args)
		{
			//create empty list dictionary
			ListDictionary listDict = new ListDictionary();
			//add MSFT order
			listDict.Add("MSFT",new Order("MSFT"));
			//add CSCO order
			listDict.Add("CSCO",new Order("CSCO"));
			//retrieve MSFT order
			Order order = listDict["MSFT"] as Order;
			Console.WriteLine(order.Instrument);
		}
	}
