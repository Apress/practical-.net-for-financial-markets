using System;
using System.Threading;

class BackForeGround
{
	static void Main(string[] args)
	{
		//create new thread
		Thread newThread = new Thread(new ThreadStart(UpdateOrder));
		//Assign user friendly name to this thread
		newThread.Name = "OrderUpdate";
		//make it foreground thread
		newThread.IsBackground = false;
		//start the execution of thread
		newThread.Start();
		//since the newly created thread is a foreground thread
		//the application will never terminate until the foreground
		//threads completes its processing
	}
	public static void UpdateOrder()
	{
		Console.WriteLine("Updating Order...Press any key to continue");
		Console.ReadLine();
	}

}
