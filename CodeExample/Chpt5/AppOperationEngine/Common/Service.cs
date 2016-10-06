using System;

namespace Common
{

	public abstract class Service : MarshalByRefObject
	{
		protected IController serviceController;
		protected DomainApp domainApp;

		//The overloaded constructor accepts two argument, 
		//the first argument is an instance of IController and 
		//second argument is an instance of DomainApp. It is important 
		//to educate the service about the underlying controller(primary controller or agent) 
		//and domain application inside which it is hosted. 
		//Effectively speaking by providing this hosting context information 
		//we allow service to directly interact with controller or 
		//domain application and allow them to leverage other services 
		//provided by the domain application, so to sum-up we are laying a strong foundation 
		//to achieve inter-service communication.
		public Service(IController controller,DomainApp app)
		{
			serviceController = controller;
			domainApp=app;
		}

		public virtual void Start()
		{
		}

		public virtual void Stop()
		{
		}

		public virtual void Suspend()
		{
		}

		public virtual void Resume()
		{
		}
	}
}
