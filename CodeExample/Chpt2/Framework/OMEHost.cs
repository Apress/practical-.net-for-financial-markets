using System;
using OME.Storage;
using OME;

namespace EquityMatchingEngine
{
	class OMEHost
	{
		[STAThread]
		static void Main(string[] args)
		{
			BizDomain equityDomain;
			//Create equity domain with 3 order processor dedicated to process
			//MSFT, IBM and GE orders
			equityDomain = new BizDomain("Equity Domain",new string[]{"MSFT","IBM","GE"});
			//Assign the order ranking logic
			equityDomain.OrderBook.OrderPriority = new PriceTimePriority();
			//Assign the business component
			EquityMatchingLogic equityMatchingLogic = new EquityMatchingLogic(equityDomain);
			//Start the matching engine
			equityDomain.Start();
			//Submit buy order
			equityDomain.SubmitOrder("MSFT",new EquityOrder("MSFT","Regular","B",20,3));
			//Submit sell order
			//this will also generate a trade because 
			//there is a matching counter buy order
			equityDomain.SubmitOrder("MSFT",new EquityOrder("MSFT","Regular","S",20,2));
			Console.WriteLine("Press any key to Stop");
			Console.ReadLine();
		}
	}
}
