using System;
using System.Collections;

namespace BCastServer
{
	public class InMemoryStore : IMessageStore
	{
		Queue msgStore = Queue.Synchronized(new Queue());
		StoreState storeState;
		string storeName;

		public InMemoryStore(string name)
		{
			
			storeName = name;

		}

		public string Name
		{
			get{return storeName;}
		}
		
		public int Count
		{
			get{return msgStore.Count;}
		}

		public void EnQueue(IBCastMessage bcastMessage)
		{
			msgStore.Enqueue(bcastMessage);
		}
			

		public IBCastMessage DeQueue()
		{
			return msgStore.Dequeue() as IBCastMessage;
		}

		public StoreState State
		{
			get{return storeState;}
			set{storeState=value;}
		}

	}
}
