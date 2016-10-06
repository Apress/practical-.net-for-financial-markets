using System;
using System.Collections;

namespace BCastServer
{
	public class BCastPipe
	{
		ArrayList moduleChain = new ArrayList();
		private static BCastPipe pipeInstance = new BCastPipe();

		public static BCastPipe Instance
		{
			get{return pipeInstance;}
		}

		public BCastPipe()
		{
			//This is the chain formation code, in an ideal world 
			//the chain will be dynamically populated from an XML configuration file. 
			//Currently data serialization and data transport module are associated
			//with this chain
			moduleChain.Add(new DataSerializerModule());
			moduleChain.Add(new TransportModule());
		}

		public void ProcessModules(object objState)
		{
			//A loop is carried out where messages are de-queued one by one 
			//and is submitted first to serializer component. 
			//Serializer component converts the message into raw bytes and 
			//it is made available as part of return argument of Process. 
			//The returned information then becomes part of contextual information 
			//and is assigned to Data property which is then passed to Transport 
			//component.Also after processing all messages inside the store the state 
			//of store is reset to idle state. 
			IMessageStore store = objState as IMessageStore;
			if ( store.Count > 0 ) 
				Console.WriteLine("Dispatching Store : " +store.Name);
			while(store.Count > 0 ) 
			{
				PipeContext pipeCtx = new PipeContext(store);
				pipeCtx.Message = store.DeQueue();
				for(int ctr=0;ctr<moduleChain.Count;ctr++)
				{
					IModule module = moduleChain[ctr] as IModule;
					object ctxData = module.Process(pipeCtx);	
					pipeCtx.Data = ctxData;
				}
			}
			store.State = StoreState.Idle;
		}
	
	}
}
