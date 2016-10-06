using System;
using System.Runtime.InteropServices;

namespace Parsing
{
	class ParsingExample
	{
		[STAThread]
		static void Main(string[] args)
		{
			//Instantiate new instance of Message parser
			//and also subscribe to its message parsing event
			MessageParser msgParser = new MessageParser();
			msgParser.MessageParsed +=new MessageParsedHandler(msgParser_MessageParsed);

			MarketDataInfo msftData = new MarketDataInfo("MSFT",21.5,22.5);
			MarketDataInfo ibmData = new MarketDataInfo("IBM",23.5,24.5);
			MarketDataInfo geData = new MarketDataInfo("GE",25.5,26.5);

			//Single Message Scenario
			Console.WriteLine("Single Message Scenario");
			byte[] buffer = msgParser.Serialize(msftData);
			msgParser.DeSerialize(buffer);

			//Large Buffer Scenario
			Console.WriteLine("Large Buffer Scenario");
			int typeSize = Marshal.SizeOf(typeof(MarketDataInfo));
			byte[] largeBuffer = new byte[typeSize*3];
			byte[] ibmBuffer = msgParser.Serialize(ibmData);
			byte[] geBuffer = msgParser.Serialize(geData);
			Array.Copy(buffer,0,largeBuffer,0,typeSize);
			Array.Copy(ibmBuffer,0,largeBuffer,buffer.Length,typeSize);
			Array.Copy(geBuffer,0,largeBuffer,buffer.Length + ibmBuffer.Length,typeSize);
			msgParser.DeSerialize(largeBuffer);

			//Small Buffer Scenario
			Console.WriteLine("Small Buffer Scenario");
			byte[] smallBuffer1= new byte[22];
			byte[] smallBuffer2= new byte[22];
			Array.Copy(buffer,0,smallBuffer1,0,22);
			Array.Copy(buffer,22,smallBuffer2,0,22);
			msgParser.DeSerialize(smallBuffer1);
			msgParser.DeSerialize(smallBuffer2);

		}

		private static void msgParser_MessageParsed(MessageHeader header)
		{
			MarketDataInfo dataInfo = header as MarketDataInfo;
			Console.WriteLine("{0} {1} {2}",dataInfo.InstrumentName,dataInfo.BidPrice,dataInfo.AskPrice);
		}
	}
}
