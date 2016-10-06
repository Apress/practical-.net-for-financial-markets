using System;
using System.Threading;

namespace BCastServer
{
	public abstract class Dispatcher
	{
		StoreCollection storeCollection;
		
		public Dispatcher()
		{

		}

		//Returns the store collection which is then iterated
		//by dispatcher, de-queuing individual message from the store
		//and dispatching it to its subscriber
		public StoreCollection Stores
		{
			set{storeCollection=value;}
			get{return storeCollection;}
		}

		//This is an abstract method which basically determines
		//the strategy for dispatching broadcast data
		public abstract void Schedule();

	}
}
