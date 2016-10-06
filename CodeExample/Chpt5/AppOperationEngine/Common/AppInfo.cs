using System;
using System.Collections;

namespace Common
{
	[Serializable]
	public class AppInfo
	{
		string appName, assemblyName;
		string assemblyPath;


		public string AssemblyPath
		{
			get{return assemblyPath;}
			set{assemblyPath=value;}
		}

		public AppInfo(string name)
		{
			appName = name;
		}

		public string Name
		{
			get{return appName;}
			set{appName=value;}
		}
		
		public string AssemblyName
		{
			get{return assemblyName;}
			set{assemblyName=value;}
		}

	}
}
