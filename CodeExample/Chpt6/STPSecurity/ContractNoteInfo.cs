using System;

namespace STP.Security
{
	//A perfect example of applying cryptography infrastructure
	//to contract note data. The important information required
	//are profile name and type of protection we wanted to apply 
	//to this data. In this case we have expressed data needs
	//to be digitally signed by annotating NonRepudiation attribute. 
	[SecurityProfile("BrokerA")]
	[NonRepudiation]
	[Serializable]
	public class ContractNoteInfo
	{
		public string Symbol;
		public int Quantity;
		public double Price;
		
		public ContractNoteInfo(string symbol,int quantity,double price)
		{
			Symbol = symbol;
			Quantity = quantity;
			Price = price;
		}
	}
}
