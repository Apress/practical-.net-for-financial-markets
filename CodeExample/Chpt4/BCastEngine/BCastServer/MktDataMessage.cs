using System;
using System.Runtime.InteropServices;

namespace BCastServer
{
	[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
	public class MktDataMessage : IBCastMessage
	{
		int msgLength;
		
		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=10)]
		string underlyingName;
		double askPrice;
		int askSize;
		double bidPrice;
		int bidSize;
		
		public string Underlying
		{
			get{return underlyingName;}
			set{underlyingName=value;}
		}

		public double Ask
		{
			get{return askPrice;}
			set{askPrice=value;}
		}
		
		public double Bid
		{
			get{return bidPrice;}
			set{bidPrice=value;}
		}

		public int AskSize
		{
			get{return askSize;}
			set{askSize=value;}
		}

		public int BidSize
		{
			get{return bidSize;}
			set{bidSize=value;}
		}


		public MktDataMessage(string underlying,double ask,int askSz,double bid,int bidSz)
		{
			underlyingName=underlying;
			askPrice=ask;
			askSize=askSz;
			bidPrice=bid;
			bidSize=bidSz;	
		}

		public int MessageType
		{
			get{return 1;}
		}

		public int MessageLength
		{
			get{return msgLength;}
			set{msgLength=value;}
		}
	}
}
