using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using Microsoft.Web.Services2.Security.X509;

namespace STPProvider
{
	public class ContractNoteInfo
	{
		public string Symbol;
		public int Quantity;
		public double Price;
		public BuySellEnum BuySell;
	}
	public enum BuySellEnum
	{
		Buy,
		Sell
	}
	public class PostTradeService : System.Web.Services.WebService
	{
		public PostTradeService()
		{
		}

		[WebMethod]
		public int SubmitContractNote(ContractNoteInfo contractNote)
		{
			//Verify the Sender Information ( Vendor B)
			//VerifySignatureOrigin();
			
			//Send the digitally signed response to Vendor B using Vendor A Certficate. 
			//SignAckResponse();
			//EncryptAckResponse();
			return 0;
		}

		public void EncryptAckResponse()
		{
			//Open the current user certificate store and look for Personal category
			X509CertificateStore localStore = X509CertificateStore.CurrentUserStore(X509CertificateStore.MyStore);
			localStore.OpenRead();

			//Find Vendor A Certificate
			X509CertificateCollection certCollection = localStore.FindCertificateBySubjectString("Vendor B");
			X509Certificate provCert = certCollection[0];
			
			//Create a new security token that is of X509 type
			//Token represent claim (authentication information)
			X509SecurityToken token = new X509SecurityToken(provCert);
			ResponseSoapContext.Current.Security.Tokens.Add(token);

			//Instruct WSE inbound filter to encrypt the message before it is transmitted over wire
			ResponseSoapContext.Current.Security.Elements.Add(new EncryptedData(token));

		}

		public void SignAckResponse()
		{
			//Open the current user certificate store and look for Personal category
			X509CertificateStore localStore = X509CertificateStore.CurrentUserStore(X509CertificateStore.MyStore);
			localStore.OpenRead();

			//Find Vendor A Certificate
			X509CertificateCollection certCollection = localStore.FindCertificateBySubjectString("Vendor A");
			X509Certificate provCert = certCollection[0];
			
			//Create a new security token that is of X509 type
			//Token represent claim (authentication information)
			X509SecurityToken token = new X509SecurityToken(provCert);
			ResponseSoapContext.Current.Security.Tokens.Add(token);

			//Instruct WSE outbound filter to sign the message before it is transmitted over wire
			//The signature is computed based on a security token
			ResponseSoapContext.Current.Security.Elements.Add(new MessageSignature(token));

		}

		public bool VerifySignatureOrigin()
		{
			SoapContext reqCtx = RequestSoapContext.Current;
			
			//Iterate through all Security elements 
			foreach(ISecurityElement secElement in reqCtx.Security.Elements)
			{
				//Check if message is digitally signed
				if ( secElement is MessageSignature)
				{
					MessageSignature signature = (MessageSignature)secElement;
					X509SecurityToken signingToken = signature.SigningToken as X509SecurityToken;
					//Authenticate the Sender using any one of the attributes of Certificate
					//More secure way is to verify using Vendor B public key
					if ( signingToken != null && signingToken.Certificate.FriendlyDisplayName == "Vendor B" )
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
