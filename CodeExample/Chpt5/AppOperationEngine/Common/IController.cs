using System;
using System.Collections;

namespace Common
{
	public interface IController
	{
		//This method is invoked on the agent by the primary controller. 
		//It is with help of this method, the primary controller empowers 
		//the agent by assigning a list of applications that directly falls 
		//under the agent’s control.
		DomainApp CreateApplication(AppInfo appInfo, DomainApp serverApp);
		//This property determines whether the controller 
		//is an agent or a primary controller.
		bool IsAgent{get;}
        //The concept of databag is not unique. Its existence could be 
		//drawn from Windows OS that provides similar features in the form 
		//of environment variables. With the help of environment variables, 
		//important configuration information are shared among OS processes. 
		//We are following similar path by introducing the databag, but the 
		//information is shared among services. With the help of this method 
		//primary controller passes the information to the agent which is then 
		//shared with agent-side services.
		void InitializeDataBag(Hashtable dataBag);
	}

}
