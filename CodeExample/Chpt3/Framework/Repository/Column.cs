using System;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace DCE.Repository
{
	public class Column : CellsAttribute
	{
		private string dataPrefix;
		
		public Column()
		{
		}

		[XmlAttribute("prefix")]
		public string Prefix
		{
			get{return dataPrefix;}
			set{dataPrefix = value;}
		}
	}
}
