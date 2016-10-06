using System;
using System.Collections;

//Stock Domain Model
public class StockData
{
	public string Symbol;
	public double AskPrice;
	public double BidPrice;
}

//Custom Comparer to sort stock data
public class StockSorter : IComparer
{
	string fldName;
	
	public StockSorter(string fld)
	{
		//since we want to provide sorting on individual field
		//of stock class, therefore the name of the field on 
		//which sort is performed is accepted as constructor 
		//argument
		fldName=fld;
	}

	public int Compare(object x, object y)
	{
		StockData leftObj= x as StockData;
		StockData rightObj=y as StockData;
		
		//If sorting is to be done on symbol field
		if ( fldName == "Symbol" ) 
		{
			return leftObj.Symbol.CompareTo(rightObj.Symbol);
		}
		//If sorting is to be done on ask price field
		if ( fldName == "AskPrice" ) 
		{
			return leftObj.AskPrice.CompareTo(rightObj.AskPrice);
		}
		return 1;
	}
}

class SortNormal
{
	static void Main(string[] args)
	{
		//create stock list
		ArrayList stockList = new ArrayList();
		//create msft stock
		StockData stkData1 = new StockData();
		stkData1.Symbol = "MSFT";
		//create ibm stock
		StockData stkData2= new StockData();
		stkData2.Symbol = "IBM";
		//add both msft and ibm stock
		stockList.Add(stkData1);
		stockList.Add(stkData2);

		while(true)
		{
			//prompt for field name on which stock items
			//stored in arraylist are sorted
			Console.WriteLine("Enter name of the field to sort on : ");
			string fldName = Console.ReadLine();
			//instantiate the custom comparer, passing the field name
			StockSorter stockSorter = new StockSorter(fldName);
			//sort the list
			stockList.Sort(stockSorter);
			//display the sorted stock item
			Console.WriteLine(fldName +" -----------------------" );
			foreach(StockData stkData in stockList)
			{
				Console.WriteLine("Symbol {0} AskPrice {1} BidPrice {2}",stkData.Symbol,stkData.AskPrice,stkData.BidPrice);
				}
			Console.WriteLine("-------------------------------");
		}
	}
}
