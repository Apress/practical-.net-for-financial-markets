using System;
using System.Threading;
using System.Diagnostics;

class InstrumentBalancing
{
	//Enumerated flag used to specify 
	//the list of processor on which threads are 
	//affinitized
	[Flags]
	public enum Processor
	{
		CPU1=1, //1st bit
		CPU2=2, //2nd bit
		CPU3=4, //3rd bit 
		CPU4=8  //4th bit
	}

	class OrderBook
	{
		//dedicated thread to process orders 
		private Thread orderSweeper;
		Processor procMask;

		public OrderBook(string instrument,Processor mask)
		{
			procMask = mask;

			//create order sweeper thraed
			orderSweeper = new Thread(new ThreadStart(OrderProcess));
			orderSweeper.Start();
		}

		
		public void OrderProcess()
		{
			
			//Get current running process instance
			Process curProcess = Process.GetCurrentProcess();

			//Get the list of threads running in this process
			foreach(ProcessThread osThread in curProcess.Threads)
			{
				//ProcessThread represents an operating system thread
				//whereas Thread represents managed thread
				//we need to find the corresponding OS thread for the
				//current managing thread
				//Get managed thread id
				int threadId = AppDomain.GetCurrentThreadId();

				//check thread id with current os thread id
				if ( osThread.Id == threadId ) 
				{	
					int mask = (int)procMask;
					//Set processor affinity
					osThread.ProcessorAffinity = (IntPtr)mask ;
				}
			}

			//start processing the order
		}
	}

	static void Main(string[] args)
	{
		//Allocate first CPU  for processing MSFT orders
		OrderBook msftBook = new OrderBook("MSFT",Processor.CPU1);
		//Allocate second CPU  for processing IBM orders
		OrderBook ibmBook  = new OrderBook("IBM",Processor.CPU2);
		//Allocate third and fourth CPU for processing GE orders 
		OrderBook geBook= new OrderBook("GE",Processor.CPU3 | Processor.CPU4 );
		Console.ReadLine();
	}
}
