using System;
using System.Collections.Generic;

public class Order
{
    public string OrderID;
    public string Instrument;
}

//Sort By Order ID
public class SortByOrderID<T> : IComparer<T> where T : Order
{
    int IComparer<T>.Compare(T x, T y)
    { return x.OrderID.CompareTo(y.OrderID); }
}

//Sort by Instrument
public class SortByInstrument<T> : IComparer<T> where T : Order
{
    int IComparer<T>.Compare(T x, T y)
    { return x.Instrument.CompareTo(y.Instrument); }
}

public class OrderContainer<T>
{
    //Customize the Sorting behavior using generic method
    public void SortOrder<U>(U orderComparer) where U : IComparer<Order>
    {
        //....
    }
}


class GenericSortMethod
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
