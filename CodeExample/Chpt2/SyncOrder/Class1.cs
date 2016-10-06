using System;
using System.Collections;
using System.Threading;

class SyncOrder
{
	//Order Domain Model
	public class Order
	{
		public string Instrument;
		public Order(string inst)
		{
			Instrument = inst;
		}
	}

	//Order book that stores individual order
	public class OrderBook
	{
		//arrays to hold orders
		ArrayList orderList = new ArrayList();
		//syncrhonization object
		private object syncObj = new object();

		public void Add(object order)
		{
			Order newOrder = order as Order;
			//acquire exclusive sychronization lock
			//start of critical section 
			lock(syncObj)
			{
				Console.WriteLine("Order Received : " +newOrder.Instrument);
				//Add order into array list
				orderList.Add(order);
				//update the downstream system
			}
			//end of critical section 
		}
	}

	static void Main(string[] args)
	{
		//create order book
		OrderBook orderBook = new OrderBook();
		//start pumping orders
		Order order = new Order("MSFT");
		Order order1 = new Order("GE");
		//start updating the order book with multiple orders on multiple threads
		ThreadPool.QueueUserWorkItem(new WaitCallback(orderBook.Add),order);
		ThreadPool.QueueUserWorkItem(new WaitCallback(orderBook.Add),order1);
		Console.ReadLine();
	}

}
