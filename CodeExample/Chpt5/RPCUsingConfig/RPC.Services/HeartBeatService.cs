using System;
using System.Runtime.Remoting;
using System.Threading;
using System.Runtime.Remoting.Lifetime;
using RPC.Common;

namespace RPC.Services
{
	//By inheriting from MarshalByRefObject we have made it a remoteable class
	//The heartbeart functionality defined over here is more or less similar to 
	//its LPC version
	public class HeartBeatService : MarshalByRefObject,IService
	{
		Thread serviceThread;
		bool serviceStop;

		public HeartBeatService()
		{
 		}


		
		public void Start()
		{
			Console.WriteLine("HeartBeat Service Started...");
			serviceThread = new Thread(new ThreadStart(Run));
			serviceStop=true;
			serviceThread.Start();
		}

		public void Run()
		{
			while(serviceStop)
			{
				Console.WriteLine("Sending HeartBeat Message...");
				Thread.Sleep(2000);
			}
		}

		public void Stop()
		{
			serviceStop=false;			
		}

		public override object InitializeLifetimeService()
		{
			return null;			
		}

	}
}
