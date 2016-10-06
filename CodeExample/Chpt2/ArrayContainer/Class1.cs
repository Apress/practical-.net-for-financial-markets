using System;


class ArrayContainer
{
	class Order
	{
	}

	[STAThread]
	static void Main(string[] args)
	{
		//Create orders
		Order order1 = new Order();
		Order order2 = new Order();
		Order order3 = new Order();
		//Declare array of order type
		//and add the above three order instance
		Order[] orderList = { order1,order2,order3};
		//Access the Order
		Order curOrder = orderList[1] as Order;
	}
}
