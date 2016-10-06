using System;
using System.Text;
using System.Threading;

    public class Order 
    {
        public string OrderId;
    }

    class AnonymousMethods
    {
        static void Main(string[] args)
        {
            //Create a new Order
            Order newOrder = new Order();
            newOrder.OrderId = "1";
            
            //Process this newly created order using ThreadPool
            ThreadPool.QueueUserWorkItem
                (delegate(object state)
                    {
                        Order curOrder = state as Order;
                        Console.WriteLine("Processing Order : " +curOrder.OrderId);
                    },newOrder
                );

            Console.ReadLine();
        }

    }
