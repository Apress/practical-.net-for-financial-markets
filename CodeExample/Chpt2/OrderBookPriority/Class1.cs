using System;
using System.Threading;

class OrderBookPriority
{
	class OrderBook
	{
		//dedicated thread to process orders 
		private Thread orderSweeper;

		public OrderBook(string instrument,bool highPriority)
		{
			orderSweeper = new Thread(new ThreadStart(Process));

			//if it is a high prioirty order book
			//then we need to ensure that this order book
			//gets maximum processing time
			if ( highPriority == true ) 
				orderSweeper.Priority = ThreadPriority.Highest;

			//start thread execution
			orderSweeper.Start();
		}

		
		public void Process()
		{
			//order processing code goes here
		}
	}
	
	static void Main(string[] args)
	{
		//create MSFT order book 
		//we want to make sure that the orders of microsoft are quickly processed
		//and therefore we raised the thread priority to highest
		OrderBook orderBook = new OrderBook("MSFT",true);
		Console.ReadLine();
	}
}
