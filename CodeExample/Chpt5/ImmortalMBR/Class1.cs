using System;

class ImmortalMBR : MarshalByRefObject
{
	public override object InitializeLifetimeService()
	{
		//By returning null we have granted infinite lifetime to remote object
		return null;
	}

	static void Main(string[] args)
	{
	}
}
