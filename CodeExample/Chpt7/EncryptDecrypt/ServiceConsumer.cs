using System;
using System.Net;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using Microsoft.Web.Services2.Security.X509;

namespace SecureSTPConsumer
{
	class ServiceConsumer
	{
		[STAThread]
		static void Main(string[] args)
		{
			STPProvider.PostTradeServiceWse postTradeSvc= new STPProvider.PostTradeServiceWse();
			STPProvider.ContractNoteInfo contractNote = new STPProvider.ContractNoteInfo();

			
			//Encrypt the Contract Note
			//EncryptContractNote(postTradeSvc);

			//Create new contract info. and submit it to STP-Provider A web service
			contractNote.Symbol = "MSFT";
			contractNote.Price = 25;
			contractNote.Quantity=100;
			contractNote.BuySell = STPProvider.BuySellEnum.Buy;
			
			int ackId =postTradeSvc.SubmitContractNote(contractNote);
			Console.WriteLine("Acknowledgement ID : " +ackId);
		}

		public static void EncryptContractNote(STPProvider.PostTradeServiceWse postTradeSvc)
		{
			//Open the current user certificate store and look for Personal category
			X509CertificateStore localStore = X509CertificateStore.CurrentUserStore(X509CertificateStore.MyStore);
			localStore.OpenRead();

			//Find STP-Provider A Certificate
			X509CertificateCollection certCollection = localStore.FindCertificateBySubjectString("STP-Provider A");
			X509Certificate provCert = certCollection[0];
			
			//Create a new security token that is of X509 type
			//Token represent claim (authentication information)
			X509SecurityToken token = new X509SecurityToken(provCert);
			
			postTradeSvc.RequestSoapContext.Security.Tokens.Add(token);

			//Instruct WSE inbound filter to encrypt the message before it is transmitted over wire
			postTradeSvc.RequestSoapContext.Security.Elements.Add(new EncryptedData(token));
		}

	}
}
