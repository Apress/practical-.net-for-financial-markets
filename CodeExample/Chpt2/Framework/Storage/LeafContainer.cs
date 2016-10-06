using System;
using System.Collections;

namespace OME.Storage
{
	public class LeafContainer : Container , IEnumerable , IEnumerator
	{
		private int rowPos = -1;
		//The internal implementation of order is based on ArrayList
		//but remember based on performance critieria this implementation
		//can be easily changed without affecting the business component code
		ArrayList orderDataStore = ArrayList.Synchronized(new ArrayList());

		public LeafContainer(OrderBook oBook,string name,Container parent)
		:base(oBook,name,parent)
		{
		}

		public override IEnumerator GetEnumerator()
		{
			Reset();
			return this;
		}

		public override void ProcessOrder(Order newOrder)
		{
			//Access the buy order book of this instrument
			Container buyBook = parentContainer.ChildContainers["B"] ;
			//Access the sell order book of this instrument
			Container sellBook = parentContainer.ChildContainers["S"] ;
			//create a event arg object containing reference to newly created
			//order along with reference to buy and sell order book
			OrderEventArgs orderArg = new OrderEventArgs(newOrder,buyBook,sellBook);
			//Invoke the OrderBeforeInsert event which will also notify 
			//the matching business component which will then perform 
			//its internal matching 
			//the order becomes active in this stage
			orderBook.OnOrderBeforeInsert(orderArg);
			//Check the quantity of the newly created order
			//because if the order has been successfully matched by matching
			//business component then quantity will be 0
			if ( newOrder.Quantity > 0 ) 
			{
				//If order is partially or not at all matched 
				//then it is inserted in the order collection
				orderDataStore.Add(newOrder);
				//Re-sort the order collection because of addition
				//of new order
				orderDataStore.Sort(orderBook.OrderPriority);
				//Invoke the OrderInsert event 
				//which will again notify the matching business component
				//the order becomes passive in this stage
				orderBook.OnOrderInsert(orderArg);
			}
			
		}

		//This group of code is scoped towards controlling the 
		//iteration behavior. C# introduced a convenient way of 
		//iterating over elements of an array using foreach statement. 
		//We have provided similar support to Container class that allows 
		//developer to iterate thru orders stored inside Container class. 
        //In case of LeafContainer class this behaviour is overridden by 
		//implementing the IEnumerable and IEnumerator interface. We provided 
		//a custom implementation to Reset, Current and MoveNext method. 
		//The Boolean value returned by MoveNext method act as a terminator condition 
		//of a foreach loop. 
		public void Reset()
		{
			rowPos=-1;
		}

		public object Current
		{
			get{return orderDataStore[rowPos];}
		}

		public bool MoveNext()
		{
		    //The code in MoveNext method validates an order by checking 
			//its quantity. If the quantity is equal to zero then it is deleted from ArrayList 
			//and row pointer is positioned to next element in the Arraylist. 
			//This check is continuously repeated inside a loop till it encounters an 
			//Order whose quantity is greater than zero. 
			rowPos++;
			while(rowPos < orderDataStore.Count)
			{
				Order curOrder = orderDataStore[rowPos] as Order;
				if ( curOrder.Quantity == 0 ) 
					orderDataStore.RemoveAt(rowPos);
				else
					return true;
			}
			Reset();
			return false;
		}
	}
}
