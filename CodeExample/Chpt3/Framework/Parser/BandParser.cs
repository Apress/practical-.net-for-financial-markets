using System;
using System.IO;
using DCE.Repository;
using DCE;

namespace DCE.Parser
{
	public class BandParser : Parser
	{
		private int _iterationCount = 0;

		public BandParser(BooleanCursor dataReader, string data, CellsAttribute cellInfo) :base(dataReader)
		{
			this.Data = data;
			this.CellsAttribute = cellInfo;
		}

		public override bool Parse()
		{
			//Retrieve the band information
			Band curBand = (Band)CellsAttribute;

			//If data to be processed is null then terminate the parsing
			if ( Data == null ) 
				return false;

			//Referring back to the band section, specifically the loop attribute, 
			//if the current loop mode is single then it needs to process 
			//only once for the current section. 
			if ( curBand.LoopMode == LoopType.Single)
			{
				if ( _iterationCount >= 1 ) 
				{
					_iterationCount  = 0 ;
					Reader.Previous();
					return false;
				}
				else
				{
					_iterationCount++;
					return true;
				}
			}

			//If the loop attribute is of repeatable type then it 
			//evaluates data for the presence of identifier defined band 
			//section of conversion rule file. If parser is not able to 
			//locate the identifier in the data then it re-sets the 
			//read pointer of the data source to its previous location by 
			//invoking the Previous member of BooleanCursor class. 
			if ( curBand.LoopMode == LoopType.Repeatable)
			{
				if ( ( curBand.Identifier.Length <= Data.Length - curBand.Start )&&  
					  Data.Substring(curBand.Start,curBand.Identifier.Length) == curBand.Identifier )
				{
					return true;
				}
				Reader.Previous();
			}
			_iterationCount = 0 ;
			return false;
		}
	}
}
