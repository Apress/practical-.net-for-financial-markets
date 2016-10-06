using System;
using System.Collections;
using System.Data;

namespace OME.Storage
{
	public class Container : IEnumerable
	{
		//Container Name
		protected string contName;
		//Reference to Leaf items where the actual order are stored
		protected ContainerCollection leafItems = new ContainerCollection();
		protected OrderBook orderBook;
		//Reference to Parent Container
		protected Container parentContainer;

		public ContainerCollection ChildContainers
		{
			get{return leafItems;}
		}

		public Container(OrderBook oBook,string name,Container parent)
		{
			orderBook=oBook;
			contName=name;
			parentContainer=parent;
		}
		
		//This method determines the order processing logic
		public virtual void ProcessOrder(Order newOrder)
		{
		}

		//Order Iteration Support
		public virtual IEnumerator GetEnumerator()
		{
			return null;
		}

	}
}
