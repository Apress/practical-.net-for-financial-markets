using System;
using System.Collections;

namespace BCastServer
{
	public class StoreCollection : IEnumerable
	{
		Hashtable storeTable = Hashtable.Synchronized(new Hashtable());
		
		public StoreCollection()
		{
		}

		public IMessageStore this[string storeName]
		{
			get{return storeTable[storeName] as IMessageStore;}
		}
		
		public void CreateStore(string storeName)
		{
			storeTable[storeName] = new InMemoryStore(storeName);
		}

		public IEnumerator GetEnumerator()
		{
			return storeTable.Values.GetEnumerator();
		}

	}
}
