using System;
using System.Threading;

class ExchangeMktDataSvc
{
	class MktDataManager
	{
		//create system named mutex
		Mutex syncExchange = new Mutex(false,"SyncExchange");
		
		public void RetrieveData()
		{
			//since we know only one request at a time
			//is allowed to submit to exchange
			//therefore it is important to 
			//syncrhonize this access among all services
			syncExchange.WaitOne();

			//retrieve market data from exchange
			Console.WriteLine("Market Data Service");

			//relase the lock allowing other service
			//such as order mgmt service to interact 
			//with exchange
			syncExchange.ReleaseMutex();
		}
	}

	static void Main(string[] args)
	{
		//create market data mgr, its primary 
		//responsiblity is retrieving market data published by exchange
		MktDataManager mktData = new MktDataManager();
		//retrieve market data
		mktData.RetrieveData();
		Console.ReadLine();
	}
}
