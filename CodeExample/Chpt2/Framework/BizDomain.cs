using System;
using System.Collections;
using OME.Storage;

namespace OME
{
	public class BizDomain 
	{
		//Hashtable to store order processor instances
		private Hashtable oprocItems = Hashtable.Synchronized(new Hashtable());
		//array of order processor name to be created under this biz domain
		private string[] oprocNames;
		//creation of order book
		private OrderBook orderBook = new OrderBook();
		
		public BizDomain(string domainName,string[] workNames)
		{
			oprocNames= workNames;
		}

		public OrderBook OrderBook
		{
			get{return orderBook;}
		}

		public void Start()
		{
			//Iterate thru all order processor names and 
			//create a new order processor object
			for (int ctr=0;ctr<oprocNames.Length;ctr++)
			{
				//Instantiates new order processor that in turn creates a 
				//dedicated thread and queue
				OrderProcessor wrkObj= new OrderProcessor(this,oprocNames[ctr]);
				oprocItems[oprocNames[ctr]] = wrkObj;
			}
		}

		//A façade method to the outside world, 
		//through which orders are submitted and queued up in 
		//appropriate order processor. 
		public void SubmitOrder(string procName,Order order)
		{
			OrderProcessor orderProcessor = oprocItems[procName] as OrderProcessor;
			orderProcessor.EnQueue(order);
		}
			
	}
}
