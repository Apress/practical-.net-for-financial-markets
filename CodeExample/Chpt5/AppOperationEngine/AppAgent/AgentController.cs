using System;
using System.Collections;
using Common;
using AppAgent.Services;

namespace AppAgent
{
	public class AgentController : MarshalByRefObject,IController
	{
		Hashtable appCollections = new Hashtable();
		Hashtable dataBag;

		public AgentController ()
		{
		}

		public void InitializeDataBag(Hashtable data)
		{
			dataBag = data;
		}

		public DomainApp CreateApplication(AppInfo appInfo, DomainApp serverApp)
		{
			Console.WriteLine("Creating Application : " +appInfo.Name);
			DomainApp newApp = new DomainApp(appInfo);
			AppManagement appMgmt = new AppManagement(this,newApp);
			newApp.Logger = serverApp.Logger;
			appCollections[appInfo.Name] = newApp;
			return newApp;
		}

		public bool IsAgent
		{
			get{return true;}
		}
	}
}
