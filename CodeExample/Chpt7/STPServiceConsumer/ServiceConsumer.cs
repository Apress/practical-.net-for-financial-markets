using System;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using Microsoft.Web.Services2.Security.X509;

namespace STPServiceConsumer
{
	class ServiceConsumer
	{
		[STAThread]
		static void Main(string[] args)
		{
			//intantiate web service proxy
			STPProvider.PostTradeService postTradeSvc = new STPProvider.PostTradeService();
			//SignData(postTradeSvc);
			//prepare contract note information
			STPProvider.ContractNoteInfo contractNote = new STPProvider.ContractNoteInfo();
			contractNote.Symbol = "MSFT";
			contractNote.Price = 25;
			contractNote.Quantity=100;
			contractNote.BuySell = STPProvider.BuySellEnum.Buy;
			//submit contract note information through web service
			int ackId =postTradeSvc.SubmitContractNote(contractNote);
			//display the ack no received from web service
			Console.WriteLine("Acknowledgement Id: " +ackId);
		}

	}
}
