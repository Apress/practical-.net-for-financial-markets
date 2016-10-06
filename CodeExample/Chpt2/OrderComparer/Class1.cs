using System;
using System.Collections;

class OrderComparer
{
	public class Order
	{
		public string Instrument;
		public int Qty;
		public int Price;
		public Order(string inst, int price,int qty)
		{
			Instrument= inst;
			Qty= qty;
			Price= price;
		}
	}
	static void Main(string[] args)
	{
	   //order collection
       ArrayList orderCol = new ArrayList();

       //add five orders 
       orderCol.Add(new Order("MSFT",25,100));
       orderCol.Add(new Order("MSFT",25,110));
       orderCol.Add(new Order("MSFT",23,95));
       orderCol.Add(new Order("MSFT",25,105));

       //Invoke the sort function of the Arraylist and pass the custom 
	   //order comparer
       orderCol.Sort(new OrderSort());

       //Print out the result of the sort
       for ( int ctr = 0;ctr<orderCol.Count;ctr++)
       {
         Order curOrder = (Order)orderCol[ctr];
		 Console.WriteLine(curOrder.Instrument+ ":" + curOrder.Price +"-" +curOrder.Qty);
       }

	   
	}

    public class OrderSort : IComparer
	{
		public int Compare(object x, object y)
		{
			Order ox = (Order)x;
			Order oy = (Order)y;
			//Compare the price 
			int priceCompare = ox.Price.CompareTo(oy.Price);
			//Compare the quantity
			int qtyCompare = ox.Qty.CompareTo(oy.Qty);
			if ( priceCompare == 0 ) 
			{
				//return value multiplied with -1 
				//will sort quantity in descending order
				return qtyCompare * -1;
			}
			//returns indication of price comparison value
			return priceCompare;
		}
	}





}
