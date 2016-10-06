using System;
using System.Threading;
using Common;

namespace AppAgent.Services
{
	public class AppManagement  : Service
	{
		AppDomain newDomain;
		Thread newThread;
		
		public AppManagement(IController controller, DomainApp app)
		:base(controller,app)
		{
			app.AppManagement = this;
			newThread = new Thread(new ThreadStart(LaunchApp));
		}

		public void LaunchApp()
		{
			newDomain = AppDomain.CreateDomain(domainApp.Info.Name);
			string appFullPath= domainApp.Info.AssemblyPath +"\\" +domainApp.Info.AssemblyName;
			newDomain.SetData("SERVICE_DOMAINAPP",domainApp);
			newDomain.ExecuteAssembly(appFullPath);
		}
		
		public override void Start()
		{
			newThread.Start();
		}

		public override void Stop()
		{
			
		}

		public override void Resume()
		{

		}

		public override void Suspend()
		{

		}

	}
}
