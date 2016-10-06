using System;
using System.Runtime.InteropServices;

namespace Parsing
{
	[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
	public class MarketDataInfo : MessageHeader
	{
		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=20)]
		public string InstrumentName;
		public double BidPrice;
		public double AskPrice;

		public MarketDataInfo()
		{}

		public MarketDataInfo(string instrumentName,double bidPrice,double askPrice)
		{
			this.MessageType = MessageHeaderType.MarketData;
			InstrumentName = instrumentName;
			BidPrice = bidPrice;
			AskPrice = askPrice;
		}
	}
}
