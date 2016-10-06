using System;
using System.Xml.Serialization;

namespace DCE.Repository
{
	public class Row  : CellsAttribute
	{
		private Column[] columns = {};
		private Band childBand;
		private string colDelimeter;

		public Row()
		{
		}

		[XmlAttribute("coldelimeter")]
		public string ColDelimeter
		{
			get{return colDelimeter;}
			set{colDelimeter=value;}
		}

		[XmlElement("band")]
		public Band ChildBand
		{
			get{return childBand;}
			set{childBand= value;}
		}

		[XmlArray("cols")]
		[XmlArrayItem("col",typeof(Column))]
		public Column[] Columns
		{
			get{return columns;}
			set{columns= value;}
		}

	}
}
