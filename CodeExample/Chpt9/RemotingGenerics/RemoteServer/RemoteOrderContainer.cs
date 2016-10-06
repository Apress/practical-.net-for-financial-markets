using System;
using System.Collections.Generic;
using System.Text;
using GenericsShared;

namespace RemoteServer
{
    public class RemoteOrderContainer<T> : MarshalByRefObject,IRemoteContainer<T>
    {
        //Add a new item
        public void Add(T newOrder)
        {
            Console.WriteLine("Order of Type " +newOrder.ToString() +" Added" );   
        }

        //Retrieve a specific item
        public T this[string orderId]
        {
            get { return default(T); }
        }
    }
}
