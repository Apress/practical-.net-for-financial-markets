using System;
using System.Collections;

class OrderQueue
{
	class Order
	{
		public string Instrument;
		public Order(string inst)
		{
			Instrument = inst;
		}
	}
	static void Main(string[] args)
	{
		//Create Queue collection
		Queue orderQueue = new Queue();
		//Add MSFT order
		orderQueue.Enqueue(new Order("MSFT"));
		//Add CSCO order
		orderQueue.Enqueue(new Order("CSCO"));
		//Add GE order
		orderQueue.Enqueue(new Order("GE"));
		//retrieves MSFT order
		Order dequedOrder = orderQueue.Dequeue() as Order; 
		//peek CSCO order but not removed from the queue
		Order peekedOrder = orderQueue.Peek() as Order; 
	}
}
