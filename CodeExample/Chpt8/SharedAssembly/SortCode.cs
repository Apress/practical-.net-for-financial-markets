using System;
using System.Reflection;
using System.Collections;

namespace SharedAssembly
{
	public class SortCode : IComparer
	{
		public virtual int Compare(object x, object y)
		{
			StockData data1;
			StockData data2;
			data1= x as StockData;
			data2=y as StockData;
			return data1.BidPrice.CompareTo(data2.BidPrice);
		}
	}
}
