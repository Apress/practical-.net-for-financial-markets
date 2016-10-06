using System;
using Microsoft.Uddi;
using Microsoft.Uddi.Api;
using Microsoft.Uddi.Business;
using Microsoft.Uddi.Service;
using Microsoft.Uddi.Binding;


namespace STPConsortium
{
	class ServiceConsumer
	{
		[STAThread]
		static void Main(string[] args)
		{
			//instantiate web service proxy
			STPProvider.PostTradeService postTradeSvc = new STPProvider.PostTradeService();
			//prepare contract note information
			STPProvider.ContractNoteInfo contractNote = new STPProvider.ContractNoteInfo();
			contractNote.Symbol = "MSFT";
			contractNote.Price = 25;
			contractNote.Quantity=100;
			contractNote.BuySell = STPProvider.BuySellEnum.Buy;

			//Fetch service endpoint information using UDDI 
			postTradeSvc.Url = GetServiceLocation();

			//submit contract note through web service 
			int ackNo=postTradeSvc.SubmitContractNote(contractNote);
			//display the ack no received from web service
			Console.WriteLine("Ack No: " +ackNo);
		}

		public static string GetServiceLocation()
		{
			Console.WriteLine("Querying UDDI Registry...");
			//Assign the network endpoint of UDDI Web services
			Inquire.Url = "http://test.uddi.microsoft.com/inquire";

			//Find the provider
			FindBusiness findProvider = new FindBusiness();
			findProvider.Names.Add("Vendor A");
			BusinessList providerList = findProvider.Send();
			BusinessInfo  provider = providerList.BusinessInfos[0];
			ServiceInfo providerService = provider.ServiceInfos[0];

			//Find the Service details 
			GetServiceDetail findService = new GetServiceDetail();
			findService.ServiceKeys.Add(providerService.ServiceKey);
			ServiceDetail sd = findService.Send();
			BusinessService service = sd.BusinessServices[0];
			BindingTemplate template =  service.BindingTemplates[0];

			//Retrieve the service URL
			Console.WriteLine("Provider Endpoint : " +template.AccessPoint.Text);
			return template.AccessPoint.Text;
		}
	}
}
