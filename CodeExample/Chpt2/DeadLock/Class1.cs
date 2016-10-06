using System;
using System.Collections;
using System.Threading;

class DeadLock
{
	//Order Domain Model
	public class Order
	{}

	//Position book that maintains instrument net position
	public class PositionBook
	{
		//position book syncrhonization object
		public object posSync = new object();
		public OrderBook OBook;
		
		//update and re-evaluate instrument position
		public void UpdateOrder(object order)
		{
			//acquire exclusive ownership on position book
			lock(posSync)
			{
				//acquire exclusive ownership on order book
				lock(OBook.orderSync)
				{}
			}
		}
	}

	//Order book that stores individual order
	public class OrderBook
	{
		public PositionBook PBook;
		//order book syncrhonization object
		public object orderSync = new object();

		public void Add(object order)
		{
			//acquire exclusive ownership on order book
			lock(orderSync)
			{
				//acquire exclusive ownership on position book
				lock(PBook.posSync)
				{}
			}
		}
	}

	static void Main(string[] args)
	{
		//create order book
		OrderBook orderBook = new OrderBook();
		//create position book
		PositionBook posBook = new PositionBook();

		//assign reference to respective books
		orderBook.PBook = posBook;
		posBook.OBook = orderBook;

		//create order
		Order order = new Order();
		//update order book
		ThreadPool.QueueUserWorkItem(new WaitCallback(orderBook.Add),order);
		//update position service
		ThreadPool.QueueUserWorkItem(new WaitCallback(posBook.UpdateOrder),order);
		Console.ReadLine();
	}

}
