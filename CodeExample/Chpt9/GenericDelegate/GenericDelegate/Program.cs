using System;
using System.Collections.Generic;
using System.Text;

    public class Order
    {}

    public class DayOrder
    {}
    
    public class OrderContainer<T>
    {
        //Generic Delegate Declaration
        public delegate void InsertOrderDelegate<U>(U orderComparer);
        public event InsertOrderDelegate<T> OrderInsert;

        public void Add(T order)
        {
            //Notify Consumer of this event
            if (OrderInsert != null)
            {
                OrderInsert(order);
            }
        }
    }

    class GenericDelegate
    {
        static void Main(string[] args)
        {
            OrderContainer<Order> orderCont= new OrderContainer<Order>();
            orderCont.OrderInsert += new OrderContainer<Order>.InsertOrderDelegate<Order>(orderCont_OrderInsert);

            OrderContainer<DayOrder> dayorderCont = new OrderContainer<DayOrder>();
            dayorderCont.OrderInsert += new OrderContainer<DayOrder>.InsertOrderDelegate<DayOrder>(dayorderCont_OrderInsert);
        }

        //Event notification for day orders
        static void dayorderCont_OrderInsert(DayOrder orderComparer)
        {
        }

        //Event notification for regular orders
        static void orderCont_OrderInsert(Order orderComparer)
        {
        }


    }
