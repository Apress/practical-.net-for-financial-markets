using System;
using DCE;
using DCE.Repository;

namespace DCE.Parser
{
	public class RowParser : Parser
	{
		public RowParser(BooleanCursor dataReader, string data, CellsAttribute cellInfo) 
			:base(dataReader)
		{
			this.Data = data;
			this.CellsAttribute = cellInfo;
		}
 
		public override bool Parse()
		{
			//The parser checks for the presence of a identifier 
			//defined in row section of rule file. If the parser is 
			//not able to locate the identifier in the data then it 
			//re-sets the read pointer of the data source to its previous 
			//location by invoking the Previous member of BooleanCursor class. 
			if ( CellsAttribute.Identifier.Length > 0 && Data.Substring(CellsAttribute.Start,CellsAttribute.Length) != CellsAttribute.Identifier )
			{
				Reader.Previous();
				return false;
			}
			return true;
		}
	}
}
