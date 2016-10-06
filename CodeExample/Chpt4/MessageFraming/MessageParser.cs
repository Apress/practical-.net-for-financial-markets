using System;
using System.Runtime.InteropServices;
using System.IO;

namespace Parsing
{
	public delegate void MessageParsedHandler(MessageHeader header);

	public class MessageParser
	{
		public event MessageParsedHandler MessageParsed;
		bool newMsg=true;
		int remainingByte;
		MemoryStream memStream = new MemoryStream();
		int msgLength;

		//Message parsing notification
		private void OnMessageParsed(MessageHeader msgHeader)
		{
			if ( MessageParsed != null )
				MessageParsed(msgHeader);
		}
		
		//Serializes message into array of bytes
		public byte[] Serialize(MessageHeader obj)
		{
			//Calculate size of object
			int objectSize = Marshal.SizeOf(obj);
			obj.MessageLength = objectSize;
			//Serialize message into array of bytes
			IntPtr memBuffer = Marshal.AllocHGlobal(objectSize);
			Marshal.StructureToPtr(obj,memBuffer,false);
			byte[] byteArray = new byte[objectSize];
			Marshal.Copy(memBuffer,byteArray,0,objectSize);
			Marshal.FreeHGlobal(memBuffer);
			return byteArray;
		}

		//Convert array of bytes into a managed type
		private void ConvertToObject(byte[] msgBytes)
		{
			//Extract the message type by reading from 4th position of byte array
			//i.e MessageType field of MessageHeader. 
			int msgType = BitConverter.ToInt32(msgBytes,4);
			Type objType = null;
			//Based on the message type determine the underlying type
			if ( msgType == (int) MessageHeaderType.MarketData) 
			{
				objType = typeof(MarketDataInfo);
			}
			//Calculate the object size
			int objectSize = Marshal.SizeOf(objType);
			
			//Convert byte array into object 
			IntPtr memBuffer = Marshal.AllocHGlobal(objectSize);
			Marshal.Copy(msgBytes,0,memBuffer,objectSize);
			MessageHeader obj = Marshal.PtrToStructure(memBuffer,objType) as MessageHeader;
			Marshal.FreeHGlobal(memBuffer);
			
			//Invoke the event to notify parsing of new message
			//pass the concrete object instance
			OnMessageParsed(obj);

		}

		public void DeSerialize(byte[] msgBytes)
		{
			AlignMessageBoundary(msgBytes,0);
		}

		//Code inside this method determines the correct message boundary
		public void AlignMessageBoundary(byte[] recvByte,int offSet)
		{
			if ( offSet >= recvByte.Length ) return ;
			
			//The logic has been branched for two types of scenarios
			//first scenario is when framing of message is performed for a new message
			//second scenario applies to messages received on a installment basis
			if ( newMsg == true ) 
			{
				//Get the length of message
				msgLength = BitConverter.ToInt32(recvByte,offSet);
				//Determine the message type
				int msgType = BitConverter.ToInt32(recvByte,offSet + 4);

				//If the length of byte array + offset is less than message length
				//then it indicates a partial message, and there are still
				//remaining bytes pending to be read. 
				if ( msgLength > ( recvByte.Length - offSet ) + 1 ) 
				{
					newMsg=false;
					remainingByte = msgLength - recvByte.Length;
					memStream = new MemoryStream();
					memStream.Write(recvByte,offSet,recvByte.Length);
				}
				else
				{
					//completes reading all pending bytes and converts
					//it into concrete object
					byte[] bytes = new byte[msgLength];
					Array.Copy(recvByte,offSet,bytes,0,msgLength );
					this.ConvertToObject(bytes);
					//Recursive call 
					AlignMessageBoundary(recvByte,offSet + msgLength);
				}
			}
			else
			{
				if ( remainingByte >  recvByte.Length )
				{
					memStream.Write(recvByte,0,recvByte.Length);
					remainingByte = remainingByte -  recvByte.Length;
				}
				else
				{
					memStream.Write(recvByte,offSet,remainingByte);
					byte[] bytes = new byte[msgLength];
					memStream.Seek(0,SeekOrigin.Begin);
					memStream.Read(bytes,0,msgLength);
					memStream.Close();
					this.ConvertToObject(bytes);
					newMsg=true;
					AlignMessageBoundary(recvByte,offSet + remainingByte + 1);
				}
			}
		}

	}
}
