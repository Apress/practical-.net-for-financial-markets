using System;
using System.Threading;

class InterLock
{
	//Order Domain Model
	public class Order
	{
		public int OrderID;
	}
	//Order Book
	class OrderBook
	{
		//Static variable that keeps track of last order id generated
		public static int orderId;

		//factory method to create new order
		public Order CreateOrder()
		{
			//create order
			Order newOrder = new Order();
			//create unique order id 
			//increment the shared variable vlaue in an atomic manner
			int newOrderId = Interlocked.Increment(ref orderId);
			//assign the new order id
			newOrder.OrderID = newOrderId;
			return newOrder;
		}
	}
	static void Main(string[] args)
	{
		//create order book
		OrderBook orderBook = new OrderBook();
		//create new order
		Order newOrder = orderBook.CreateOrder();
	}
}
