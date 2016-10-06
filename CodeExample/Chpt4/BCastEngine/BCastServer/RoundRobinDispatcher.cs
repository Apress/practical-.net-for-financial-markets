using System;
using System.Threading;

namespace BCastServer
{
	public class RoundRobinDispatcher : Dispatcher
	{
		Thread scheduleThread;
		int sleepPeriod=100;

		public RoundRobinDispatcher()
		{
			//A new schedule thread is created that starts the message 
			//dispatching process. 
			scheduleThread = new Thread(new ThreadStart(MessageDispatch));
		}

		private void MessageDispatch()
		{
            //In this section of code message store is fetched 
			//from the store collection and processing of individual 
			//store is then off-loaded on a dedicated thread made 
			//available from the thread-pool. So effectively messages 
			//from individual stores are concurrently broadcasted to recipients. 
			//The schedule thread sleeps for 100 ms before it again re-schedules 
			//the broadcast. Before re-scheduling takes place we make sure that 
			//we don’t face the reentrancy problem. This problem is tackled by 
			//associating a state to a store. 
			while(true)
			{
				foreach(IMessageStore store in Stores)
				{
					if ( store.State == StoreState.Idle ) 
					{
						store.State = StoreState.Busy;
						ThreadPool.QueueUserWorkItem(new WaitCallback(BCastPipe.Instance.ProcessModules),store);
					}
				}
				Thread.Sleep(sleepPeriod);
			}
		}

		public override void Schedule()
		{
			scheduleThread.Start();
		}
	}

}
