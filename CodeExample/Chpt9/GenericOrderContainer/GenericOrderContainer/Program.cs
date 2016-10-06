using System;
using System.Text;


    class GenericOrderContainer
    {

        //reference type order
        public class OrderObj
        {
            public string Instrument;
            public double Quantity;
        }
        //value type order
        public struct OrderStruct
        {
            public string Instrument;
            public double Quantity;
        }

        static void Main(string[] args)
        {
            //Generic type instantiation using reference type
            OrderContainer<OrderObj> orderObjContainer = new OrderContainer<OrderObj>(10);
            //Add and retrieve reference type order
            orderObjContainer.AddOrder(new OrderObj());
            OrderObj orderObj = orderObjContainer.GetOrder(0);

            //Generic type instantiation using value type
            OrderContainer<OrderStruct> orderStructContainer = new OrderContainer<OrderStruct>(10);
            //Add and retrieve value type order
            orderStructContainer.AddOrder(new OrderStruct());
            OrderStruct orderStruct = orderStructContainer.GetOrder(0);
        }
    }

    public class OrderContainer<T> 
    {
        T[] dataContainers;
        int ctr = 0;

        //allocate array elements with specified capacity
        public OrderContainer(int orderCapacity)
        {
            dataContainers = new T[orderCapacity];

        }

        //Add a new Order
        public void AddOrder(T order)
        {
            dataContainers[ctr] = order;
            ctr++;
        }

        //Retrieve a specific order
        public T GetOrder(int index)
        {
            return dataContainers[index];
        }
    }

