using System;
using System.Collections.Generic;
using System.Text;

public class Order<T>
{
public T OrderID;
}
public class DayOrder<T> : Order<T>
{}
public class LimitOrder<T> : Order<string>
{}

class OrderObj : IComparable<OrderObj>
{
    public string Instrument;
    public double Quantity;

    int IComparable<OrderObj>.CompareTo(OrderObj x)
    {
        return x.Quantity.CompareTo(this.Quantity);
    }

}
struct OrderStruct : IComparable<OrderStruct>
{
    public string Instrument;
    public double Quantity;

    int IComparable<OrderStruct>.CompareTo(OrderStruct x)
    {
        return x.Quantity.CompareTo(this.Quantity);
    }
}


class GenericInheritance
{
    static void Main(string[] args)
    {
    }
}
