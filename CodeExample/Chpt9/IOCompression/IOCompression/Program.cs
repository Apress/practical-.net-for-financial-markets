using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace IOCompression
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStream memStream = new MemoryStream();
            string orderXml = "<Order><OrderID>1</OrderID><Quantity>50</Quantity><Price>25</Price></Order>";
            byte[] data = Encoding.UTF8.GetBytes(orderXml);
            
            //Data Compression
            DeflateStream compressedStream = new DeflateStream(memStream , CompressionMode.Compress,true);
            compressedStream.Write(data, 0, data.Length);
            compressedStream.Close();

            //Reset seek pointer which is mandatory in order to decompress data
            memStream.Position = 0;

            //Data De-compression
            DeflateStream decompressedStream = new DeflateStream(memStream, CompressionMode.Decompress);
            byte[] decompBuffer = new byte[memStream.Length];
            decompressedStream.Read(decompBuffer, 0, decompBuffer.Length);
            string orgData = Encoding.UTF8.GetString(decompBuffer);
            Console.WriteLine("Decompressed Data : " + orgData);

        }
    }
}
