using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

//Include namespace from PreGenXMLSerializer.XMLSerializers assembly
using Microsoft.Xml.Serialization.GeneratedAssembly;

namespace PreGenXMLSerializer
{
    public class Order
    {
        public string OrderID;
        public int Quantity;
        public double Price;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Order dayOrder = new Order();
            dayOrder.OrderID = "1";
            dayOrder.Quantity = 50;
            dayOrder.Price = 25;

            //Serialize Order using pre-generated serializers
            OrderSerializer orderSzer = new OrderSerializer();
            XmlTextWriter txtWriter = new XmlTextWriter(new StreamWriter(@"C:\Order.xml"));
            orderSzer.Serialize(txtWriter, dayOrder);
            txtWriter.Close();
            
            //DeSerialize Order using pre-generated de-serializers
            XmlTextReader txtReader = new XmlTextReader(new StreamReader(@"C:\Order.xml"));
            Order newOrder = orderSzer.Deserialize(txtReader) as Order;
            Console.WriteLine(newOrder.OrderID);
        }
    }
}
