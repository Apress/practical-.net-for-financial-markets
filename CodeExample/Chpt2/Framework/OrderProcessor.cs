using System;
using System.Threading;
using System.Collections;
using OME.Storage;

namespace OME
{
	public class OrderProcessor 
	{
		Queue msgQueue ;
		Thread msgDispatcher;
		ManualResetEvent processSignaller;
		BizDomain bizDomain;
		
		public OrderProcessor(BizDomain domain,string wspName)
		{
			//Domain under which this order processor is assigned
			bizDomain = domain;
			//create a order queue
			msgQueue = Queue.Synchronized(new Queue());
			//create a event notification object
			//which notifies the enqueuing of a new order
			processSignaller = new ManualResetEvent(false);
			//create a dedicated thread to process the order stored 
			//in queue collection
			msgDispatcher = new Thread(new ThreadStart(ProcessQueue));
			//start the processing
			msgDispatcher.Start();
		}

		public void EnQueue(object newOrder)
		{
			//Enqueue the order and signal the event object
			msgQueue.Enqueue(newOrder);
			processSignaller.Set();
		}

		private void ProcessQueue()
		{
			//start of order draining process
			while(true) 
			{
				//wait for signal notification 
				processSignaller.WaitOne(1000,false);
				//iterate through queue 
				while(msgQueue.Count > 0)
				{
					//dequeue the order
					Order order = msgQueue.Dequeue() as Order;
					//submit it to order book for further processing
					bizDomain.OrderBook.Process(order);
				}
			}
		}
	}
}
