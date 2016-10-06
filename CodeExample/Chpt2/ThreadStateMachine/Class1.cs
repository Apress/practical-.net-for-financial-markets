using System;
using System.Diagnostics;
using System.Threading;

class ThreadStateMachine
{
	static void Main(string[] args)
	{
		Thread newThread = new Thread(new ThreadStart(Test));
		newThread.Priority = ThreadPriority.Highest;
		newThread.Name = "Yogesh";
		Console.WriteLine("Hash code : " +newThread.GetHashCode());
		newThread.Start();
		
		Process p = Process.GetCurrentProcess();
		foreach(ProcessThread pthread in  p.Threads)
		{
			Console.WriteLine(pthread.Id);
		}
		
	}
	public static void Test()
	{
		Console.WriteLine("Thread ID " +AppDomain.GetCurrentThreadId());
		Console.WriteLine("Thread");
		Console.ReadLine();
	}
}
