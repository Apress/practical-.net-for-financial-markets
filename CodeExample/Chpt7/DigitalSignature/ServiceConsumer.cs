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

			//Digitally Sign the Contract Note
			//SignContractNote(postTradeSvc);
			
			//Create new contract info. and submit it to STP-Provider A web service
			contractNote.Symbol = "MSFT";
			contractNote.Price = 25;
			contractNote.Quantity=100;
			contractNote.BuySell = STPProvider.BuySellEnum.Buy;
			
			int ackId =postTradeSvc.SubmitContractNote(contractNote);
			
			//Verify the response received from STP-Provider A
			Console.WriteLine("Verify : " +VerifyAckResponse(postTradeSvc));
			Console.WriteLine("Acknowledgement ID : " +ackId);
		}

		public static bool VerifyAckResponse(STPProvider.PostTradeServiceWse postTradeSvc)
		{
			SoapContext respCtx = postTradeSvc.ResponseSoapContext;
			
			//Iterate through all Security elements 
			foreach(ISecurityElement secElement in respCtx.Security.Elements)
			{
				//Check if message is digitally signed
				if ( secElement is MessageSignature)
				{
					MessageSignature signature = (MessageSignature)secElement;
					X509SecurityToken signingToken = signature.SigningToken as X509SecurityToken;
					//Authenticate the Sender using any one of the attributes of Certificate
					//More secure way is to verify using STP-Provider A public key
					if ( signingToken != null && signingToken.Certificate.FriendlyDisplayName == "STP-Provider A" )
					{
						return true;
					}
				}
			}
			return false;

		}

		public static void SignContractNote(STPProvider.PostTradeServiceWse postTradeSvc)
		{
			//Open the current user certificate store and look for Personal category
			X509CertificateStore localStore = X509CertificateStore.CurrentUserStore(X509CertificateStore.MyStore);
			localStore.OpenRead();

			//Find STP-Provider B Certificate
			X509CertificateCollection certCollection = localStore.FindCertificateBySubjectString("STP-Provider B");
			X509Certificate provCert = certCollection[0];
			
			//Create a new security token that is of X509 type
			//Token represent claim (authentication information)
			X509SecurityToken token = new X509SecurityToken(provCert);
			postTradeSvc.RequestSoapContext.Security.Tokens.Add(token);

			//Instruct WSE inbound filter to sign the message before it is transmitted over wire
			//The signature is computed based on a security token
			postTradeSvc.RequestSoapContext.Security.Elements.Add(new MessageSignature(token));
		}
	}
}
