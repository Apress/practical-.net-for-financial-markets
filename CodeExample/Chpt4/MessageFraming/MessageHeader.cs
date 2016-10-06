using System;
using System.Runtime.InteropServices;

namespace Parsing
{
	public enum MessageHeaderType
	{
		MarketData,
		OrderData,
		TradeData
	}

	[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
	public class MessageHeader
	{
		public int MessageLength;

		[MarshalAs(UnmanagedType.I4)]
		public MessageHeaderType MessageType;

		public MessageHeader()
		{
		}
	}
}

