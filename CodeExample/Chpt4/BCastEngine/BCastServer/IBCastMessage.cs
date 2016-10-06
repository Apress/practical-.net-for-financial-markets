using System;

namespace BCastServer
{
	public interface IBCastMessage
	{
		//Identifies broadcast message type
		//for example market data broadcast, exchange bulletin broadcast
		int MessageType{get;}
		//Length of Message
		int MessageLength{get;set;}
	}
}
