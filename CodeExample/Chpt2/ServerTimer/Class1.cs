using System;
using System.Threading;

class ServerTimer
{
	class ExchangeGateway
	{
		//Server Timer
		public Timer hbTimer;
		
		//Callback method invoked at an interval of 2 seconds
		public void SendHeartBeat(object state)
		{
			//Disable the timer
			//to avoid code-reentrancy problem
			hbTimer.Change(Timeout.Infinite,Timeout.Infinite);
			
			//Send message to exchange

			//Enable the timer
			//schedule start and subsequent invocation of callback
			//after every 2 seconds
			hbTimer.Change(TimeSpan.FromSeconds(2),TimeSpan.FromSeconds(2));
		}

		public ExchangeGateway()
		{
			//Create server timer and pass the callback method which 
			//is periodically notified at an interval of 2 seconds
			//start the timer immediately
			hbTimer = new Timer(new TimerCallback(SendHeartBeat),null,0,2000);
		}
	}
	
	static void Main(string[] args)
	{
		//create exchange gateway responsible 
		//for all communication with exchange
		ExchangeGateway gateWay = new ExchangeGateway();
		Console.ReadLine();
	}
}
