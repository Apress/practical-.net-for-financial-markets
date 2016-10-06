using System;
namespace RPC.Common
{
	public interface ILookUp
	{
		IService LookUp(string serviceName);
	}
}