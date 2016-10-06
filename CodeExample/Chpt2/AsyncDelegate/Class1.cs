using System;

class AsyncDelegate
{
	//Order Domain class
	public class Order{}
	//Trade Domain class
	public class Trade{}
	
	//Delegate used to proccess order which in turn returns trades 
	//generatd as a result of this new order
	public delegate Trade[] OrderHandler(Order order);
	static void Main(string[] args)
	{
		//instantiate a new order
		Order newOrder = new Order();
		//create a delegate instance that refers to processing order 
		//function
		OrderHandler processOrder = new OrderHandler(ProcessOrder);
		//begin the processing order in an asyncrhonous fashion
		IAsyncResult orderResult = processOrder.BeginInvoke(newOrder,null,null);
		//blocks the current thread until the processing of order 
		//which is executed on a threadpool threads is completed
		orderResult.AsyncWaitHandle.WaitOne();
		//collect the trades generated as a result of 
		//asyncrhonous order processing
		Trade[] trades = processOrder.EndInvoke(orderResult);
		//display the trades
		Console.WriteLine("Total Trade Generated : " +trades.Length);
	}

	//order processing 
	public static Trade[] ProcessOrder(Order order)
	{
		//Process the order
		//ideally submit it to matching engine
		//and get the trades 

		//Let's assume we hit some trades for this order
		return new Trade[]{new Trade()};
	}

}
