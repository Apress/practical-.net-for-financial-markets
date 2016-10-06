using System;
using System.Collections.Generic;
using System.Text;

namespace GenericMethod
{
    public class Order 
    {
        public string OrderID;
        public string Instrument;
    }

    public class SortByOrderID<T> : IComparer<T> where T: Order
    {
        int IComparer<T>.Compare(T x,T y)
        { return x.OrderID.CompareTo(y.OrderID);}
    }

    public class SortByInstrument<T> : IComparer<T> where T : Order
    {
        int IComparer<T>.Compare(T x, T y)
        { return x.Instrument.CompareTo(y.Instrument);}
    }

    public class OrderContainer<T> 
    {
        //Customize the sort behavior using generic method
        public void SortOrder<U>(U orderComparer) where U:IComparer<Order>
        {
            //....
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OrderContainer<Order> container = new OrderContainer<Order>();
            
            //Sort By Instrument 
            SortByInstrument<Order> sortInst = new SortByInstrument<Order>();
            container.SortOrder<SortByInstrument<Order>>(sortInst);

            //Sort By Order ID 
            SortByOrderID<Order> sortID = new SortByOrderID<Order>();
            container.SortOrder<SortByOrderID<Order>>(sortID);
        }
    }
}
