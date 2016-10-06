using System;
using System.Collections;

class HashTbl
{
	//Order Domain class
	public class Order
	{}
	static void Main(string[] args)
	{
		//create empty hash table
		Hashtable orderHash = new Hashtable();
		
		//add multiple order, order id is the key
		//and the actua instance of Order is the value
		orderHash.Add("1",new Order());
		orderHash.Add("2",new Order());
		orderHash.Add("3",new Order());

		//locate a specific order using order id
		Order order = orderHash["1"] as Order;
		
		//Remove a particular order
		orderHash.Remove("1");

		//check if order exist with a particular id
		if ( orderHash.ContainsKey("2") == true ) 
		{
			Console.WriteLine("This order already exist");
		}

		
	}
}
