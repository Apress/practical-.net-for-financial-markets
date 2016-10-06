using System;
using System.Xml;
using System.IO;
using DCE.Repository;
using DCE.Parser;


namespace DCE.Writer
{
	public class XMLDataWriter: IWriter

	{
		private XmlTextWriter xmlWriter;
		private TextWriter baseWriter;

		public XMLDataWriter(TextWriter dataWriter)
		{
			xmlWriter = new XmlTextWriter(dataWriter);
			xmlWriter.Formatting=  Formatting.Indented;
			xmlWriter.Indentation = 4;
			baseWriter = dataWriter;
		}

		public void WriteStartColumn(CellsAttribute metaDataInfo, string data)
		{
			if ( metaDataInfo.IsSuppressed == true ) return;
			xmlWriter.WriteStartAttribute(metaDataInfo.Name,"");
			xmlWriter.WriteString(data);				
		}

		public void WriteEndColumn(CellsAttribute metaDataInfo)
		{
			if ( metaDataInfo.IsSuppressed == true ) return;
			Row rowCell = metaDataInfo.ParentCell as Row;
			xmlWriter.WriteEndAttribute();
		}

		public void WriteStartRow(CellsAttribute metaDataInfo, string data)
		{
			if ( metaDataInfo.IsSuppressed == true ) return;
			xmlWriter.WriteStartElement(metaDataInfo.Name);
		}

		public void WriteEndRow(CellsAttribute metaDataInfo)
		{
			if ( metaDataInfo.IsSuppressed == true ) return;
			xmlWriter.WriteEndElement();
		}

		public void WriteStartBand(CellsAttribute metaDataInfo, string data)
		{
			if ( metaDataInfo.IsSuppressed == true ) return;
			xmlWriter.WriteStartElement(metaDataInfo.Name);
		}

		public void WriteEndBand(CellsAttribute metaDataInfo)
		{
			if ( metaDataInfo.IsSuppressed == true ) return;
			xmlWriter.WriteEndElement();
		}

		public TextWriter BaseWriter
		{
			get{return baseWriter;}
		}
	}
}
