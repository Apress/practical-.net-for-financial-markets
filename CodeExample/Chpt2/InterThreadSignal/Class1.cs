using System;
using System.Collections;
using System.Threading;

class InterThreadSignal
{
	public class Order
	{}
	public class OrderBook
	{
		Thread orderSweeper;
		//event object intially set to non-signal state
		ManualResetEvent manualEvent = new ManualResetEvent(false);
		//create a thread-safe version of queue
		Queue orderQueue = Queue.Synchronized(new Queue());
		
		public OrderBook()
		{
			//create order sweeper thread
			orderSweeper = new Thread(new ThreadStart(Process));
			//start thread execution
			orderSweeper.Start();
		}

		public void Add(Order order)
		{
			//enqueue the order
			orderQueue.Enqueue(order);
			//signal the sweeper thread about arrival of new order
			manualEvent.Set();
		}

		public void Process()
		{
			while(true)
			{
				//wait for order to be enqueued
				manualEvent.WaitOne();
				//set the event to non-signal state
				manualEvent.Reset();
				//process the order
				while(orderQueue.Count > 0 )
				{
					Console.WriteLine("Processing Order");
					//dequeue the order 
					orderQueue.Dequeue();
				}
			}
		}
	}
	
	static void Main(string[] args)
	{
		//create order book
		OrderBook orderBook = new OrderBook();
		//start pumping orders 
		//that will be concurrently processed by sweeper thread
		for(int ctr=0;ctr<=10;ctr++)
		{
			orderBook.Add(new Order());
		}
		Console.ReadLine();
	}
}
