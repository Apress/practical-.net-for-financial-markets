using System;
using System.Xml.Serialization;

namespace DCE.Repository
{
	[XmlRoot("matrix")]	
	public class Matrix
	{
		private Band[] bands = {};
		public Matrix()
		{
		}

		[XmlArray("bands")]
		[XmlArrayItem("band",typeof(Band))]
		public Band[] Bands
		{
			get{return bands;}
			set{bands = value;}
		}
	}
}
