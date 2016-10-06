using System;
using OME.Storage;

namespace EquityMatchingEngine
{
	public class EquityOrder : Order
	{
		public EquityOrder(string instrument,string orderType,string buySell,double price,int quantity)
		{
			this.Instrument = instrument;
			this.OrderType = orderType;
			this.BuySell = buySell;
			this.Price = price;
			this.Quantity = quantity;
		}
	}
}
