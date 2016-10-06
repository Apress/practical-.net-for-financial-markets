using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Common;

namespace InfoCacheClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Retrieve Cached object from market information cache server which is locally hosted
            ICacheInfo remoteCache = (ICacheInfo)Activator.GetObject(typeof(ICacheInfo), "ipc://InfoCacheServer/MktInfoCacheImpl.rem");
            DataSet cacheObj = remoteCache.RetrieveCache();
            Console.WriteLine("Information Successfully Retrieved...");
            Console.ReadLine();
        }
    }
}
