using System;

namespace BCastServer
{
	public enum StoreState
	{
		Idle,
		Busy
	}

	public interface IMessageStore
	{
		//Enqueue broadcast message
		void EnQueue(IBCastMessage bcastMessage);
		//Dequeue broadcast message
		IBCastMessage DeQueue();
		StoreState State{get;set;}
		//Total message in the store
		int Count{get;}
		//User friendly name of the store
		string Name{get;}

	}
}
