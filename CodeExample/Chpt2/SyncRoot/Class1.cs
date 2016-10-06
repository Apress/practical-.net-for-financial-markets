using System;
using System.Threading;
using System.Collections;

class SyncRoot
{
	//Order Domain model
	class Order{}
	//Order Book
	class OrderBook
	{
		//create thread-safe list
		ArrayList orderList = ArrayList.Synchronized(new ArrayList());

		public void Add(object order)
		{
			//Add order
			orderList.Add(order);
		}
		public void Remove(object order)
		{
			//Remove order
		}
		
		public ArrayList TopFive()
		{
			//create temporary list to hold top five order
			ArrayList topFive = new ArrayList();
			//Lock the collection so that the orders
			//returned are accurate
			lock(orderList.SyncRoot)
			{
				//Iterate and retrieve top five order
				int ctr=0;
				foreach(Order order in orderList)
				{
					topFive.Add(order);
					if ( ctr > 5 ) 
						break;
					else
						ctr++;
				}
			}
			return topFive;
		}

	}
	static void Main(string[] args)
	{
		//create order book
		OrderBook orderBook = new OrderBook();
		//start inserting orders on different thread
		Order newOrder = new Order();
		ThreadPool.QueueUserWorkItem(new WaitCallback(orderBook.Add),newOrder);

		//create another new order
		newOrder = new Order();
		ThreadPool.QueueUserWorkItem(new WaitCallback(orderBook.Add),newOrder);

		//Retrieve top five order on different thread
		ThreadPool.QueueUserWorkItem(new WaitCallback(TopFiveOrder),orderBook);

	}

	public static void TopFiveOrder(object oBook)
	{
		//Retrieve top five order
		OrderBook orderBook = oBook as OrderBook;
		ArrayList topFive = orderBook.TopFive();
	}
}
