using System;
using System.Xml.Serialization;

namespace DCE.Repository
{
	public enum LoopType
	{
		[XmlEnum("repeatable")]
		Repeatable,
		[XmlEnum("single")]
		Single
	}

	public class Band : CellsAttribute
	{
		private Row[] rows = {};
		private LoopType loopMode;

		public Band()
		{
		}

		[XmlAttribute("loop")]
		public LoopType LoopMode
		{
			get{return loopMode;}
			set{loopMode=value;}
		}

		[XmlArray("rows")]
		[XmlArrayItem("row",typeof(Row))]
		public Row[] Rows
		{
			get{return rows;}
			set{rows = value;}
		}
	}
}
