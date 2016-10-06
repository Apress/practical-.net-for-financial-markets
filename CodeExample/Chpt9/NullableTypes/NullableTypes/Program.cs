using System;

    public class Order
    {
        public string Instrument;
        
        //Nullable Value type, null is assigned as default value 
        public int? Quantity = null;
        public double? Price = null;
    }
    class NullableTypes
    {
        static void Main(string[] args)
        {
            Order newOrder = new Order();
            
            //This will return true because quantity value is null
            Console.WriteLine("Is Quantity Null : " + ( newOrder.Quantity == null ) );

            //Null coalescing operator
            //If quantity value is null then by default assign value 10
            newOrder.Quantity = newOrder.Quantity ?? 10;
            Console.WriteLine("Quantity : " +newOrder.Quantity);

            //Addition operator
            newOrder.Quantity = newOrder.Quantity + 5;
            Console.WriteLine("Quantity : " + newOrder.Quantity);

        }
    }
