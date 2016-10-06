using System;
using System.Collections.Generic;
using System.Text;

    class NonGenericOrderContainer
    {
        //reference-type order
        class OrderObj
        {
            public string Instrument;
            public double Quantity;
        }
        //value-type order
        struct OrderStruct
        {
            public string Instrument;
            public double Quantity;
        }
        static void Main(string[] args)
        {
            //create order container to store reference type orders
            OrderContainer orderObjContainer = new OrderContainer(10);
            //Adding orders of reference type
            orderObjContainer.AddOrder(new OrderObj());
            //Cast Operation 
            OrderObj orderObj = orderObjContainer.GetOrder(0) as OrderObj;

            //create order container to store value type orders
            OrderContainer orderStructContainer = new OrderContainer(10);
            //Adding orders of value type 
            //Boxing Cost
            orderStructContainer.AddOrder(new OrderStruct());
            //Unboxing Cost
            object orderStruct = orderStructContainer.GetOrder(0);
        }
    }

    public class OrderContainer
    {
        object[] dataContainers;
        int ctr = 0;

        //allocate array elements with specified capacity
        public OrderContainer(int orderCapacity)
        {
            dataContainers = new object[orderCapacity];
        }

        //Add a new Order
        public void AddOrder(object order)
        {
            dataContainers[ctr] = order;
            ctr++;
        }

        //Retrieve a specific order
        public object GetOrder(int index)
        {
            return dataContainers[index];
        }
    }
