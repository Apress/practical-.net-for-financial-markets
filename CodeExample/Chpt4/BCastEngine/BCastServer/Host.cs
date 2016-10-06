using System;

namespace BCastServer
{
	public class Host
	{
		
		public static void Main(string[] args)
		{
			StoreCollection storeCollection = new StoreCollection();
			//Create a dedicated store for MSFT,YHOO,GE
			storeCollection.CreateStore(@"store\MSFT");
			storeCollection.CreateStore(@"store\YHOO");
			storeCollection.CreateStore(@"store\GE");

			//Create the Message Dispatching Scheduler
			RoundRobinDispatcher dispatcher = new RoundRobinDispatcher();
			dispatcher.Stores = storeCollection;
			dispatcher.Schedule();
			
			//Enqueue market data message in MSFT store
			MktDataMessage mktData= new MktDataMessage("MSFT",24.5,100,50,25);
			IMessageStore msgStore = storeCollection[@"store\" +mktData.Underlying];
			msgStore.EnQueue(mktData);

			//Enqueue market data message in GE store
			mktData= new MktDataMessage("GE",24.5,100,50,25);
			msgStore = storeCollection[@"store\" +mktData.Underlying];
			msgStore.EnQueue(mktData);
			
			Console.ReadLine();

		}

	}
}
