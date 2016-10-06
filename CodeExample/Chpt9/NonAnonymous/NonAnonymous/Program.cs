using System;
using System.Text;
using System.Threading;

namespace NonAnonymous
{
    public class Order
    {
        public string OrderId;
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Create a new Order
            Order newOrder = new Order();
            newOrder.OrderId = "1";

            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessOrders),newOrder);
            Console.ReadLine();
        }

        public static void ProcessOrders(object state)
        {
            Order curOrder = state as Order;
            Console.WriteLine("Processing Order : " + curOrder.OrderId);
        }
    }
}
