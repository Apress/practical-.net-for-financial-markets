using System;
using System.Runtime.InteropServices;

namespace BCastServer
{
	public class DataSerializerContext
	{
		byte[] rawData;

		public DataSerializerContext(byte[] data)
		{
			rawData = data;
		}

		public byte[] Data
		{
			get{return rawData;}
		}

	}

	public class DataSerializerModule : IModule
	{
		public object Process(PipeContext pipeCtx)
		{
			//Receive the strongly typed broadcast message
			IBCastMessage msg = pipeCtx.Message;
			//Calculate the object size
			int objectSize = Marshal.SizeOf(msg);
			//Assign the lenght of message
			msg.MessageLength= objectSize;
			//convert the managed object into array of bytes
			IntPtr memBuffer = Marshal.AllocHGlobal(objectSize);
			Marshal.StructureToPtr(msg,memBuffer,false);
			byte[] byteArray = new byte[objectSize];
			Marshal.Copy(memBuffer,byteArray,0,objectSize);
			Marshal.FreeHGlobal(memBuffer);
			//Return the byte array that will then be 
			//used by transport module to deliver to its destination
			return new DataSerializerContext(byteArray);
		}

	}
}
