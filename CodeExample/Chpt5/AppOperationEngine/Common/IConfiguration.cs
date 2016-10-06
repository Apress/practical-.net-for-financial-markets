using System;
using System.Xml;

namespace Common
{
	public interface IConfiguration 
	{
		XmlElement GetConfig();
	}
}
