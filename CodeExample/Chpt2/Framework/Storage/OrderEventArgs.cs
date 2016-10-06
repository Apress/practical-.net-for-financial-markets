using System;

namespace OME.Storage
{
	public class OrderEventArgs
	{
		private Order order;
		private Container buyBook;
		private Container sellBook;

		public OrderEventArgs(Order newOrder,Container bBook,Container sBook)
		{
			order = newOrder;
			buyBook = bBook;
			sellBook = sBook;
		}

		public Order Order
		{
			get{return order;}
		}

		public Container BuyBook
		{
			get{return buyBook;}
		}

		public Container SellBook
		{
			get{return sellBook;}
		}

	}
}
