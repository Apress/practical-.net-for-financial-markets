using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

    class Order
    {}

    class SemaphoreLock
    {
        static Semaphore orderSemaphore;
        static void Main(string[] args)
        {
            ManualResetEvent waitEvent = new ManualResetEvent(false);
            int initialTokens = 3;
            int maxTokens = 3;

            //Assume some sort of order container that stores order
            List<Order> orderContainer = new List<Order>();

            //Create a new semaphore, which at any time allows 
            //only three concurrrent threads to acess the order container and process individual order
            //The first parameter represent initial tokens available in pool and last parameter represent maximum available tokens
            orderSemaphore = new Semaphore(initialTokens, maxTokens);
            for (int ctr = 0; ctr <= 10; ctr++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessOrders),ctr);
            }

            //Prevent program from exiting
            waitEvent.WaitOne();
        }

        public static void ProcessOrders(object state)
        {
            //Acquire the Semaphore lock
            //If lock is successfully acquired then semaphore count is decremented
            orderSemaphore.WaitOne();
            
            //insert order into Order-Book
            Console.WriteLine("Order Processed : " +state);
            Console.WriteLine("Press any key to Continue");
            Console.ReadLine();
            
            //Release the lock, which will increment the semaphore count
            orderSemaphore.Release();
        }
    }
