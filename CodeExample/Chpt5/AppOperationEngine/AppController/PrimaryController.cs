	using System;
	using System.Runtime.Remoting;
	using System.Collections;
	using Common;
	using AppController.Services;

	namespace AppController
	{

		public class PrimaryController: MarshalByRefObject,IController
		{
			Hashtable agents = new Hashtable();
			Hashtable dataBag = new Hashtable();

			//The constructor method populates the data bag by 
			//invoking InitializeDataBag method. The body of the method 
			//is empty but ideally the data bag values will be fetched 
			//from XML File or database or some other data source. 
			public PrimaryController()
			{
				InitializeDataBag(dataBag);
			}

			public AgentInfo this[string agentName]
			{
				get{return agents[agentName] as AgentInfo;}
			}

			public void Start()
			{
				ConnectAgents();
			}

			public void InitializeDataBag(Hashtable data)
			{
			}

            //The real hand-shaking among agents is performed inside this code. 
			//The list of agents primarily their location is stored in a application 
			//configuration file as part of remoting section, after reading this 
			//location values with help of remoting helper method we enter 
			//inside a foreach loop. It is inside this loop a remote instance 
			//of agent is created on remote server and its reference is stored for 
			//subsequent access. After successful creation of agent the next step 
			//is to assign it the list of applications that are directly under its supervision. 
			public void ConnectAgents()
			{
				foreach(WellKnownClientTypeEntry clientEntry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
				{
					Console.WriteLine("Connecting to Agent : " +clientEntry.ObjectUrl);
					IController agent = Activator.GetObject(typeof(IController),clientEntry.ObjectUrl) as IController;
					agent.InitializeDataBag(dataBag);
					AgentInfo agentInfo = new AgentInfo(agent);
					agents[clientEntry.ObjectUrl] = agentInfo;
					InitializeApplications(agentInfo);
				}
			}
			
            //In this section of code both primary controller and agent 
			//creates an instance of domain applications and attach the 
			//list of services applicable on their end. Again for sake 
			//of simplicity we have hard-wired the application name, 
			//application path and the assembly name inside the code 
			//but the best approach is to separate out this information 
			//in a configuration file and also assign the agent controlling 
			//these applications. We will also notice a call to 
			//CreateApplication method happening on both primary controller 
			//and agent; this method invocation will ensure that both agent and 
			//primary controller have performed the necessary required set-up. 
			//Another important section of code to look is exchange of remote 
			//references particularly instance of AppManagement class reference. 
			//When we invoke CreateApplication method on an instance of agent we 
			//also pass a reference to server-side domain application and on successful 
			//execution of this method it returns a reference to agent-side domain 
			//application which itself is a remote reference. We know that both 
			//server-side and agent-side services are derived from common base class 
			//Service, so by accessing the AppManagement property of remote instance of 
			//domain applications we will be returned with proxy reference. 
			public void InitializeApplications(AgentInfo agentInfo)
			{
				AppInfo appInfo = new AppInfo("Order Matching");
				appInfo.AssemblyName = "OrderMatching.exe";
				appInfo.AssemblyPath = @"C:\CodeExample\Chpt5\AppOperationEngine\OrderMatching\bin\Debug";
				DomainApp omeServer= this.CreateApplication(appInfo,null);
				DomainApp omeClient= agentInfo.Agent.CreateApplication(appInfo,omeServer);
				omeServer.AppManagement = omeClient.AppManagement;
				agentInfo.Applications.Add(omeServer.Info.Name,omeServer);
			}

			//The required initialization of domain application is peformed 
			//inside this code, what we meant by intiailization is configuring 
			//the services and assigning its reference back to domain application. 
			public DomainApp CreateApplication(AppInfo appInfo, DomainApp serverApp)
			{
				DomainApp newApp = new DomainApp(appInfo);
				LogManagement logMgmt = new LogManagement(this,newApp);
				return newApp;
			}

			public bool IsAgent
			{
				get{return false;}
			}

		}
	}
