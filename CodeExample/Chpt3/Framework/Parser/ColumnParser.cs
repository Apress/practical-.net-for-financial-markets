using System;
using DCE.Repository;
using DCE;

namespace DCE.Parser
{
	public class ColumnParser : Parser
	{
		private string[] splittedData;

		public ColumnParser(BooleanCursor dataReader) 
			:base(dataReader)
		{
		}

		public override bool Parse()
		{
			Row curRow = (Row)CellsAttribute.ParentCell;
			//This is the final processing logic in the parsing chain. 
			//A check is performed to see whether a column delimiter has 
			//been specified. If a column delimiter is found then a Split 
			//operation is performed that splits out array of strings based 
			//on character delimiter passed to it. The array of string returned 
			//from the Split operation is assigned to array. This splitting process 
			//is conducted only once - during the parsing of first column - and 
			//subsequent access to data is retrieved from a cached array.
			if (curRow.ColDelimeter.Length > 0 )
			{
				if ( this.CellsAttribute.Index == 1 ) 
					splittedData = Data.Split(curRow.ColDelimeter.ToCharArray());
				this.Data = splittedData[this.CellsAttribute.Index - 1];
			}
			else
			{
				//If there is no delimiter specified then it is assumed that it is a 
				//fixed length file format, and data is retrieved using offset position 
				//and length of data.
				this.Data = this.Data.Substring(CellsAttribute.Start,CellsAttribute.Length);
			}
			return true;
		}
	}
}
