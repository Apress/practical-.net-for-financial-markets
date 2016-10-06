using System;
using System.Runtime.Remoting.Messaging;

class OrderProcessorCallback
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
		//callback function to be invoked when order processing is completed
		AsyncCallback processComplete = new AsyncCallback(TradeGenerated);
		//begin the processing order in an asyncrhonous fashion
		//passing the callback delegate instance
		IAsyncResult orderResult = processOrder.BeginInvoke(newOrder,processComplete,processOrder);
		Console.ReadLine();
	}

	//callback notification after successfully processing order
	public static void TradeGenerated(IAsyncResult result)
	{
		//retrieve the correct method delegate reference
		OrderHandler processOrder = ((AsyncResult)result).AsyncDelegate as OrderHandler;
		//collect the trades generated as a result of 
		//asyncrhonous order processing
		Trade[] trades = processOrder.EndInvoke(result);
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
