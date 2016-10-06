using System;
using System.Collections.Generic;
using System.Text;
using GenericsShared;

namespace RemoteClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instantiating remote container that allows only regular order
            IRemoteContainer<Order> ordCont = Activator.GetObject(typeof(IRemoteContainer<Order>), "tcp://localhost:17000/OrderContainer.rem") as IRemoteContainer<Order>;
            Order newOrder = new Order();
            ordCont.Add(newOrder);

            //Instantiating remote container that allows only limit order
            IRemoteContainer<LimitOrder> limitOrdCont = Activator.GetObject(typeof(IRemoteContainer<LimitOrder>), "tcp://localhost:17000/LimitOrderContainer.rem") as IRemoteContainer<LimitOrder>;
            LimitOrder newLimit= new LimitOrder();
            limitOrdCont.Add(newLimit);
            
            Console.ReadLine();
        }
    }
}
