using System;

namespace BCastServer
{
	public interface IModule
	{
		object Process(PipeContext pipeCtx);
	}
}
