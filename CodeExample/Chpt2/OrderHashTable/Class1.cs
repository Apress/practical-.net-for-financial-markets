using System;
using System.Collections;
class OrderHashTable
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
		//Create empty hash table
		Hashtable orderHashCol= new Hashtable();
		//insert IBM order
		orderHashCol["IBM"] = new Order("IBM");
		//insert MSFT order
		orderHashCol["MSFT"] = new Order("MSFT");
		//Access MSFT order
		Order orderItem = orderHashCol["MSFT"] as Order;
		//Display the instrument name
		Console.WriteLine(orderItem.Instrument);
		//Remove MSFT items
		orderHashCol.Remove("MSFT");
	}
}
