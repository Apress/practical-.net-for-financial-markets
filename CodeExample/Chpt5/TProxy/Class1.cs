using System;
using System.Runtime.Remoting;

class TProxy
{
	[STAThread]
	static void Main(string[] args)
	{
		object newObj= new object();
		bool isProxy = RemotingServices.IsTransparentProxy(newObj);
		Console.WriteLine(isProxy);
	}
}
