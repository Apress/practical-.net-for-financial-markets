using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Common;

namespace MktInfoCacheServer
{
    public class MktInfoCacheImpl : MarshalByRefObject, ICacheInfo
    {
        
        public DataSet RetrieveCache()
        {
            Console.WriteLine("Request Received...");
            return null;
        }
    }
}
