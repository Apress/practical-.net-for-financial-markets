using System;
using System.Security.Principal;
using System.Threading;

class CurrentThread
{
	static void Main(string[] args)
	{
		//get Access to current running thread
		Thread curThread = Thread.CurrentThread;
		//get credential associated with current thread;
		IPrincipal principal = Thread.CurrentPrincipal;
		//get Priority of the current Thread
		ThreadPriority priority = Thread.CurrentThread.Priority;
		//set priority of the current thread
		Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
		//get the current state of the thread
		ThreadState state = Thread.CurrentThread.ThreadState;
	}
}
