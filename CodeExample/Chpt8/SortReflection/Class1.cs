using System;
using System.Collections;
using SharedAssembly;

  
	class SortReflection
	{
		static void Main(string[] args)
		{
			//create stock list
			ArrayList stockList = new ArrayList();
			//add MSFT order
			StockData stkData1 = new StockData();
			stkData1.Symbol = "MSFT";
			stkData1.AskPrice = 10;
			stkData1.BidPrice = 12;
			//add IBM order
			StockData stkData2= new StockData();
			stkData2.Symbol = "IBM";
			stkData2.AskPrice = 12;
			stkData2.BidPrice = 9;
			//add GE order
			StockData stkData3 = new StockData();
			stkData3.Symbol = "GE";
			stkData3.AskPrice = 13;
			stkData3.BidPrice = 10;
			//add stock items
			stockList.Add(stkData1);
			stockList.Add(stkData2);
			stockList.Add(stkData3);

			while(true)
			{
				//prompt name of the field to sort 
				Console.WriteLine("Enter name of the field to sort on : ");
				string fldName = Console.ReadLine();
				//custom comparer code using reflection
				SortByReflection sort = new SortByReflection(fldName);
				//sort the list
				stockList.Sort(sort.GetComparer());
				//display the sorted stock item
				Console.WriteLine(fldName +" -----------------------" );
				foreach(StockData stkData in stockList)
				{
					Console.WriteLine("Symbol {0} AskPrice {1} BidPrice {2} ",stkData.Symbol,stkData.AskPrice,stkData.BidPrice);
					}
				Console.WriteLine("-------------------------------");
			}
		}
	}
