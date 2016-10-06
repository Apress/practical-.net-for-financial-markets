using System;
using System.Threading;

class OrderProcessor
{
	public class Order
	{
		public string Instrument;
		public Order(string inst)
		{
			Instrument=inst;
		}
	}

	static void Main(string[] args)
	{
		//Process order using thread-pool
		//Pass the method name to be executed along with data to be used by the method
		ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessOrder),new Order("MSFT"));
		ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessOrder),new Order("CSCO"));
		Console.ReadLine();
	}
	public static void ProcessOrder(object order)
	{
		Order curOrder = order as Order;
		Console.WriteLine("Processing Order :" + curOrder.Instrument);
	}
}
