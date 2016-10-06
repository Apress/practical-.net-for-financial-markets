using System;

class ArrayCopy
{
	class Order{}
	[STAThread]
	static void Main(string[] args)
	{
		//Create a order array
		Order[] orderList = { new Order(),new Order(),
						      new Order(),new Order()};

		//Create a temp array of exactly same size 
		//as original order container
		Order[] tempList = new Order[4];
		//copy the actual items stored in order array
		//to temp order array
		Array.Copy(orderList,0,tempList,0,4);
		//re-size the order array
		orderList = new Order[5];
		//copy the order items from temp order array
		//to original order array
		Array.Copy(tempList,0,orderList,0,4);
	}
}
