using System;
using System.Threading;

class DeadLockFree
{
	//Order Domain Model
	public class Order
	{}

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

	public class PositionBook
	{
		public object posSync = new object();
		public OrderBook OBook;



		public void UpdateOrder(object order)
		{
			//try to obtain position book lock
			if ( !Monitor.TryEnter(posSync,TimeSpan.FromSeconds(5)))
				throw new ApplicationException("Failed to obtain Position Book Lock");

	 		try
			{
				//try to obtain order book lock
				if ( !Monitor.TryEnter(OBook.orderSync,TimeSpan.FromSeconds(5)))
					throw new ApplicationException("Failed to obtain Order Book Lock");

				try
				{
					//update order book
				}
				finally
				{
					//release order book lock
					Monitor.Exit(OBook.orderSync);
				}
			}
			finally
			{
				//release position book lock
				Monitor.Exit(posSync);
			}
		}
	}

	public class OrderBook
	{
		public PositionBook PBook;
		public object orderSync = new object();

		public void Add(object order)
		{
			//try to obtain order book lock
			if ( !Monitor.TryEnter(orderSync,TimeSpan.FromSeconds(5)))
				throw new ApplicationException("Faild to obtain Order Book Lock");

			try
			{
				//try to obtain position book lock
				if ( !Monitor.TryEnter(PBook.posSync,TimeSpan.FromSeconds(5)))
					throw new ApplicationException("Failed to obtain Position Book Lock");

				try
				{
					//update position book
				}
				finally
				{
					//release position book lock
					Monitor.Exit(PBook.posSync);
				}
			}
			finally
			{
				//release order book lock
				Monitor.Exit(orderSync);
			}
		}
	}
}
