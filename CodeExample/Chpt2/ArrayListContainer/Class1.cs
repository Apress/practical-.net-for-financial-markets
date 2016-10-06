using System;
using System.Collections;

class ArrayListContainer
{
	class Order
	{}
	class LimitOrder
	{}
	class IOCOrder
	{}
	[STAThread]
	static void Main(string[] args)
	{
		ArrayList orderContainer;
		orderContainer  = new ArrayList();
		//Add regular order
		Order order = new Order();
		orderContainer.Add(order);
		//Add limit order 
		LimitOrder limOrder = new LimitOrder();
		orderContainer.Add(limOrder);
		//Add IOC order
		IOCOrder iocOrder =new IOCOrder();
		orderContainer.Add(iocOrder );

		//Access limit Order
		limOrder = orderContainer[0] as LimitOrder;
		//Remove limit order
		orderContainer.RemoveAt(0);
		//Display total elements
		Console.WriteLine("Total Elements : " +orderContainer.Count);


	}
}
