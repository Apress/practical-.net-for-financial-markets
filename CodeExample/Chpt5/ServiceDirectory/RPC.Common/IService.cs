using System;
using System.Runtime.Remoting;

namespace RPC.Common
{
	public interface IService
	{
		void Start();
		void Stop();
	}
}
