using System;

class QuickSort
{
	static void Main(string[] args)
	{
		//elements arranged in unsorted order
		int[] elements = {12,98,95,1,6,4,101};

		//sort element using quick sort
		Array.Sort(elements,0,elements.Length);

		//display output of sorted elements
		for(int ctr=0;ctr<elements.Length;ctr++)
		{
			Console.WriteLine(elements[ctr]);
		}
	}
}
