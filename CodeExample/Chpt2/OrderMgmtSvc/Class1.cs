using System;
using System.Threading;

class OrderMgmtSvc
{
	class Order{}
	class OrderBook
	{
		//create system named mutex
		Mutex syncExchange = new Mutex(false,"SyncExchange");
		
		//send order to exchange 
		public void SendToExchange(Order order)
		{
			//only one request is allowed to submit to exchange
			//therefore it is important to syncrhonize this access
			//among all services
			//acquire exclusive ownership 
			syncExchange.WaitOne();

			//send order to exchange
			Console.WriteLine("Order sent to Exchange");
			Console.ReadLine();
			
			//relase the lock allowing other service
			//such as exchange mkt data to interact 
			//with exchange
			syncExchange.ReleaseMutex();
		}
	}
	static void Main(string[] args)
	{
		//create order book
		OrderBook orderBook = new OrderBook();
		//create order
		Order order = new Order();
		//send order to exchange
		orderBook.SendToExchange(order);
	}
}
