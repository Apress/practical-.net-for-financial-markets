using System;
using System.Collections.Specialized;
using System.Collections;

class StringCol
{
	static void Main(string[] args)
	{
		//create empty arraylist collection
		ArrayList objectCol = new ArrayList();
		//add msft 
		objectCol.Add("MSFT");
		//access the string
		//cast needed
		string symbol = (string)objectCol[0];

		//create empty string collection
		StringCollection stringCol = new StringCollection();
		stringCol.Add("MSFT");
		symbol = stringCol[0]; //Casting is removed
	}
}

