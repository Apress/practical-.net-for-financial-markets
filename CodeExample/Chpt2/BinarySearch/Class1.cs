using System;

class BinarySearch
{
	static void Main(string[] args)
	{
		//elements arranged in unsorted order
		int[] elements = {12,98,95,1,6,4,101};

		//sort element using quick sort
		Array.Sort(elements,0,elements.Length);

		//Find element using binary search 
		//i.e find 95
		int elementPos = Array.BinarySearch(elements,0,elements.Length,99);
		
		//if exact match found
		if ( elementPos >= 0 )
		{
			Console.WriteLine("Exact Match Found : " +elementPos);
		}
		else
		//nearest match found
		{
			//bitwise complement operator
			elementPos = ~elementPos;
			Console.WriteLine("Nearest Match : " +elementPos);
		}
	}
}
