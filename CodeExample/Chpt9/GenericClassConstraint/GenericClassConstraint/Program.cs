using System;
using System.Collections.Generic;
using System.Text;

public class OrderObj : IComparable<OrderObj>
{
    public string Instrument;
    public double Quantity;

    int IComparable<OrderObj>.CompareTo(OrderObj x)
    {
        return x.Quantity.CompareTo(this.Quantity);
    }
}

//Compiles successfully
public class OrderContainer<T> where T : OrderObj
{
    public void AddOrder(T order)
    {
      //Quantity cannot be negative
      if (order.Quantity < 0)
          throw new ApplicationException("Quantity cannot be negative"); 
    }
}

class GenericClassConstraint
{
    static void Main(string[] args)
    {
    }
}
