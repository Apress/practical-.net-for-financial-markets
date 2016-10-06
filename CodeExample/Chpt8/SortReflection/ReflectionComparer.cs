using System;
using System.Reflection;
using SharedAssembly;
using System.Collections;

	class ReflectionComparer  : IComparer
	{
		string fldName;
		public ReflectionComparer(string fld)
		{
			fldName = fld;
		}
    
		public int Compare(object x, object y)
		{
			StockData leftObj = x as StockData;
			StockData rightObj = y as StockData;
      
			//Retrieve field meta data
			FieldInfo leftField= leftObj.GetType().GetField(fldName);
			FieldInfo rightField= rightObj.GetType().GetField(fldName);

			//Retrieve field value
			object leftValue = leftField.GetValue(leftObj);
			object rightValue = rightField.GetValue(rightObj);

			//Retrieve method meta-data
			MethodInfo leftMethod = leftField.FieldType.GetMethod("CompareTo",new  
				Type[]{leftValue.GetType()});
			//invoke the method 
			object retValue = leftMethod.Invoke(leftValue,new object[]{rightValue});
			return (int)retValue;
		}
	}

	public class SortByReflection
	{
		string fldName;
     
		public SortByReflection(string fld)
		{
			fldName=fld;
		}

		public IComparer GetComparer()
		{
			return new ReflectionComparer(fldName);
		}
	}
