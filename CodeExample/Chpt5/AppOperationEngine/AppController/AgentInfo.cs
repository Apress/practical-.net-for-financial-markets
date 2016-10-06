using System;
using System.Collections;
using Common;

namespace AppController
{
	public class AgentInfo
	{
		IController agent;
		Hashtable applications = new Hashtable();

		public Hashtable Applications
		{
			get{return applications;}
		}
		
		public IController Agent
		{
			get{return agent;}
		}

		public AgentInfo(IController controller)
		{
			agent=controller;
		}
	}
}
