using System;
using System.Collections.Specialized;

namespace BCastServer
{
	public class PipeContext
	{
		object ctxData;
		IMessageStore msgStore;
		IBCastMessage message;
		
		public PipeContext(IMessageStore store)
		{
			msgStore=store;
		}

		//Returns the current store
		public IMessageStore Store
		{
			get{return msgStore;}
		}

		//Returns the current message
		public IBCastMessage Message
		{
			get{return message;}
			set{message=value;}
		}

		//Returns the contextual data
		public object Data
		{
			get{return ctxData;}
			set{ctxData=value;}
		}

	}
}
