using System;
using System.Xml.Serialization;

namespace DCE.Repository
{
	public abstract class CellsAttribute
	{
		private string  dataIdentifer;
		private int  offSet;
		private string name;
		private int  index;
		private int  dataLength;
		private CellsAttribute  parentCell;
		private bool isSuppressed;

		[XmlIgnore]
		public CellsAttribute ParentCell
		{
			get{return  parentCell;}
			set{ parentCell= value;}
		}

		[XmlIgnore]
		public int Index
		{
			get{return  index;}
			set{ index=value;}
		}

		[XmlAttribute("name")]
		public string Name
		{
			get{return  name;}
			set{ name= value;}
		}

		[XmlAttribute("identifier")]
		public string Identifier
		{
			get{return  dataIdentifer;}
			set{ dataIdentifer=value;}
		}

		[XmlAttribute("start")]
		public int Start
		{
			get{return  offSet;}
			set{ offSet= value;}
		}

		[XmlAttribute("length")]
		public int Length
		{
			get{return  dataLength;}
			set{ dataLength = value;}
		}

		[XmlAttribute("suppress")]
		public bool IsSuppressed
		{
			get{return  isSuppressed;}
			set{ isSuppressed= value;}
		}

	}
}
