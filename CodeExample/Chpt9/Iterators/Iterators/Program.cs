using System;
using System.Collections.Generic;
using System.Text;

namespace Iterators
{
    public class Order {}

    public class OrderContainer<T> 
    {
        List<T> orderList = new List<T>();
        
        //Default foreach Implementation
        public IEnumerator<T> GetEnumerator()
        {
            for (int ctr = 0; ctr < orderList.Count; ctr++)
            {
                yield return orderList[ctr];
            }
        }

        //Best Five Orders
        public IEnumerable<T> BestFive()
        {
            for (int ctr= 0; ctr < orderList.Count; ctr++)
            {
                if (ctr > 4)
                    //Stop Iteration Phase
                    yield break;
                yield return orderList[ctr];
            }
        }

        //Iteration of only limit Orders
        public IEnumerable<T> LimitOrders()
        {
            for (int ctr = 0; ctr < orderList.Count; ctr++)
            {
                //Check for Limit Order and return 
                yield return orderList[ctr];
            }
        }

    }

    class Iterators
    {
        static void Main(string[] args)
        {
            OrderContainer<Order> orderContainer = new OrderContainer<Order>();

            //Iterate all orders
            foreach (Order curOrder in orderContainer)
            {}

            //Iterate Best Five
            foreach (Order curOrder in orderContainer.BestFive())
            {}

            //Iterate Limit Order
            foreach (Order curOrder in orderContainer.LimitOrders())
            {}
        }
    }
}
 