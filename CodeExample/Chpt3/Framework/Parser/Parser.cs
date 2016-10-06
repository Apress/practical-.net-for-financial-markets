using System;
using DCE.Repository;
using DCE;

namespace DCE.Parser
{
	public abstract class Parser
	{
		private string  data;
		private BooleanCursor  dataReader;
		private CellsAttribute cellInfo;

		public Parser(BooleanCursor reader)
		{
			 dataReader = reader;
		}

		//The value of this property governs the entire parsing process. 
		//It represents conversion Rule and must be a Band or Row or Column class. 
		public CellsAttribute CellsAttribute
		{
			get{return cellInfo;}
			set{cellInfo = value;}
		}

		//This property gives the parser class access to the 
		//underlying information data-source. 
		public BooleanCursor Reader
		{
			get{return  dataReader;}
		}
		
		//Parser needs to have access to the raw data before it 
		//could apply its parsing logic. It is with help of this property 
		//data is retrieved or assigned. 
		public string Data
		{
			get{return  data;}
			set{ data = value;}
		}
		public abstract bool Parse();
	}
}
