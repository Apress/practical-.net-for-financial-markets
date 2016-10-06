using System;
using System.Collections;
using OME.Storage;

namespace EquityMatchingEngine
{
	public class PriceTimePriority:IComparer
	{
		public int CompareOrder(Order orderX,Order orderY,int sortingOrder)
		{
			//Compares the current order price with another order price
			int priceComp = orderX.Price.CompareTo(orderY.Price);
			//If both price are equal then we also need to sort according to 
			//order timestamp
			if ( priceComp == 0 ) 
			{
				//Compare the current order timestamp with another order timestamp
				int timeComp = orderX.TimeStamp.CompareTo(orderY.TimeStamp);
				return timeComp;
			}
			//since the sorting order for buy and sell order book is different
			//we need to ensure that orders are arranged accordingly
			//buy order book - highest buy price occupies top position
			//sell order book - lowest sell price occupies top position
			//therefore sortingOrder helps to achieve this ranking
			//a value of -1 sorts order in descending order of price and ascending 
			//order of time 
			//similarly value of 1 sorts order in ascending order of price
			//and ascending order of time
			return priceComp * sortingOrder;
		}

		public int Compare(object x, object y)
		{
			Order orderX = x as Order;
			Order orderY = y as Order;

			//For a buy order highest buy price occupies top position
			if ( orderX.BuySell == "B" ) 
				return CompareOrder(orderX,orderY,-1);
			else
				//For a sell order lowest sell price occupies top position
				return CompareOrder(orderX,orderY,1);
		}

	}
}
