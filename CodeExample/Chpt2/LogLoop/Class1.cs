using System;

class LogLoop
{
	static void Main(string[] args)
	{
		int i=1;
		while ( i < 500)
		{
			Console.WriteLine(i);
			i = i * 2;
		}
	}
}
