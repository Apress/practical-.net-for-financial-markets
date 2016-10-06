using System;
using Common;

namespace AppController.Services
{
	public class LogManagement : Service,ILogger
	{
		public LogManagement(IController controller,DomainApp app)
		:base(controller,app)
		{
			app.Logger = this;
		}
        //Logging of Messages
		public void Log(string logMsg)
		{
			Console.WriteLine(logMsg);
		}
	}
}
