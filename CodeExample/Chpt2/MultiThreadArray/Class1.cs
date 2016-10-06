using System;
using System.Collections;

class MultiThreadArray
{
	[STAThread]
	static void Main(string[] args)
	{
		//thread-safe list
		ArrayList orderList = ArrayList.Synchronized(new ArrayList());
	}
}
