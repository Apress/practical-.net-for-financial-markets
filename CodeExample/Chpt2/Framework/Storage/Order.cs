using System;
using System.Threading;

namespace OME.Storage
{
	public abstract class Order
	{
		string instrument;
		string buySell;
		string orderType;
		double price;
		int quantity;
		static long globalOrderId;
		long orderId;
		DateTime orderTimeStamp;
		
		public Order()
		{
			//Generate Default Values 
			//Global unique order ID
			orderId = Interlocked.Increment(ref globalOrderId);
			//Order Time Stamp
			orderTimeStamp = DateTime.Now;
		}

		public DateTime TimeStamp
		{
			get{return orderTimeStamp;}
		}

		public string Instrument
		{	get{return instrument;}
			set{instrument=value;}
		}

		public string OrderType
		{
			get{return orderType;}
			set{orderType=value;}
		}

		public string BuySell
		{
			get{return buySell;}
			set{buySell=value;}
		}

		public double Price
		{	get{return price;}
			set{price=value;}
		}
		
		public int Quantity
		{
			get	{return quantity;}
			set{
				if ( value < 0 ) 
					quantity = 0;
				else
					quantity=value;
			}
		}
		
		public long OrderID
		{
			get	{return orderId;}
			set{orderId=value;}
		}
	}
}
