using System;
using System.Collections;

class MultiThreadQueue
{
	static void Main(string[] args)
	{
		//Thread safe queue data structure
		Queue orderQueue = Queue.Synchronized(new Queue());
	}

}
