using System;
using System.Collections;

class OrderStack
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
		//create empty stack
		Stack orderStack = new Stack();
		//push MSFT order
		orderStack.Push(new Order("MSFT"));
		//push CSCO order
		orderStack.Push(new Order("CSCO"));
		//pop CSCO order
		Order poppedOrder = orderStack.Pop() as Order;
	}
}
