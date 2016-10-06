using System;

namespace Common
{
	public class DomainApp: MarshalByRefObject
	{
		AppInfo appInfo;
		ILogger logger;
		IConfiguration configuration;
		Service appMgmt;
	
        //The underlying information about the actual application 
		//is available with the help of the AppInfo property.
		public AppInfo Info
		{
			get{return appInfo;}
		}

		public DomainApp(AppInfo info)
		{
			appInfo = info;
		}

        //This property allows accessing the functionality provided 
		//by the Application management service.
		public Service AppManagement
		{
			get{return appMgmt;}
			set{appMgmt=value;}
		}

        //Logger property encapsulates centralized logging features.
		public ILogger Logger
		{
			get{return logger;}
			set{logger =value;}
		}
	}
}
