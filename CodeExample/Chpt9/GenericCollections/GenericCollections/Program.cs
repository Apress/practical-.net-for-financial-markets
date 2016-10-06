using System;
using System.Collections.Generic;
using System.Text;

    public class Order
    {}
    
    class Program
    {
        static void Main(string[] args)
        {
            //Generic Queue 
            Queue<Order> orderQueue = new Queue<Order>();

            //Generic Stack 
            Stack<Order> orderStack = new Stack<Order>();

            //Generic List 
            List<Order> orderList = new List<Order>();

            //Generic Hashtable 
            Dictionary<string, Order> orderHashTable = new Dictionary<string, Order>();

            //Generic SortedList 
            SortedDictionary<string, Order> orderSortDict = new SortedDictionary<string, Order>();

            //Generic LinkedList
            LinkedList<Order> linkList = new LinkedList<Order>();
        }
    }
