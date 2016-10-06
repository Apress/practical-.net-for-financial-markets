using System;
using System.Collections;

namespace OME.Storage
{
	public delegate void OrderEventHandler(object sender,OrderEventArgs e);

	public class OrderBook
	{
		//Event invoked before inserting order - active order
		public event OrderEventHandler OrderBeforeInsert;
		//Event invoked after inserting order - passive order
		public event OrderEventHandler OrderInsert;

		//Order ranking logic
		private IComparer orderPriority;
		//This variable holds the root node of the order tree 
		//that in turn allows navigating the entire tree.
		private ContainerCollection bookRoot;

		public ContainerCollection Containers
		{
			get{return bookRoot;}

		}

		//Internal method to trigger active order notification 
		//to external business component
		internal void OnOrderBeforeInsert(OrderEventArgs e)
		{
			if ( OrderBeforeInsert != null ) 
				OrderBeforeInsert(this,e);
		}

		//Internal method to trigger passive order notification 
		//to external business component
		internal void OnOrderInsert(OrderEventArgs e)
		{
			if ( OrderInsert != null ) 
				OrderInsert(this,e);
		}

		public IComparer OrderPriority
		{
			get{return orderPriority;}
			set{orderPriority=value;}
		}

		public OrderBook() 
		{
			//instantiate the root container of the order tree
			bookRoot = new ContainerCollection();
		}

		private Container ProcessContainers(ContainerCollection contCollection,string name,Order order,Container parent)
		{
			//Check for presence of this specific container
			//in case it is not found then create a new container
			if ( contCollection.Exists(name) == false ) 
				contCollection[name] = new OME.Storage.Container(this,name,parent);

			OME.Storage.Container currentContainer = contCollection[name];
			//invoke the order processing on that container
			currentContainer.ProcessOrder(order);
			return currentContainer;
		}

		//This method looks after the arrangement of order in order tree, 
		//based on key attributes of the order it seeks appropriate node 
		//in tree, in case if a node doesn’t exists it creates a new node 
		//by instantiating the appropriate Container Class. 
		//The logic deviates a bit when it comes to creation of leaf node of 
		//the tree (i.e. Buy or Sell Node), we fallback to LeafContainer 
		//class that is where the actual order is rested. 
		public void Process(Order order)
		{
			Container container = ProcessContainers(bookRoot,order.Instrument,order,null);
			container = ProcessContainers(container.ChildContainers,order.OrderType,order,container);
			
			//Logic deviates a bit, if it is a buy or sell node
			//then leafcontainer is created which actually holds the order
			if ( container.ChildContainers.Exists(order.BuySell.ToString()) == false ) 
			{
				//create buy and sell leaf container
				LeafContainer buyContainer = new LeafContainer(this,"B",container);
				LeafContainer sellContainer = new LeafContainer(this,"S",container);
				container.ChildContainers["B"] = buyContainer;
				container.ChildContainers["S"] = sellContainer;
			}

			//Based on the buy/sell attribute of the order 
			//access the underlying leaf container
			LeafContainer leafContainer = container.ChildContainers[order.BuySell.ToString()] as LeafContainer;
			//process the order
			leafContainer.ProcessOrder(order);
		}

	}
}
