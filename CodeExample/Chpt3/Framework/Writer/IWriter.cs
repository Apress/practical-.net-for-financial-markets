using System;
using System.IO;
using DCE.Repository;
using DCE.Parser;

namespace DCE.Writer
{
	public interface IWriter
	{
		//This paired method is invoked by ColumnParser during parsing phase
		void WriteStartColumn(CellsAttribute metaDataInfo, string data);
		void WriteEndColumn(CellsAttribute metaDataInfo);
		//This paired method is invoked by RowParser during parsing phase
		void WriteStartRow(CellsAttribute metaDataInfo,string data);
		void WriteEndRow(CellsAttribute metaDataInfo);
		//This paired method is invoked by BandParser during parsing phase
		void WriteStartBand(CellsAttribute metaDataInfo,string data);
		void WriteEndBand(CellsAttribute metaDataInfo);
		TextWriter BaseWriter{get;}
	}
}
