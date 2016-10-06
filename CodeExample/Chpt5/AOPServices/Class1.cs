using System;
using AspectSharp;
using AspectSharp.Builder;
using AOPServices.Services;

namespace AOPServices
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			String weavingRules = 
				" import AOPServices.Aspects " + 
				" " +
				" aspect AuthorizationAspect for [ AOPServices.Services ] " + 
				"   " + 
				"   pointcut method(* Start())" + 
				"     advice(AuthorizationAdvice)" + 
				"   end" + 
				"   " + 
				"   pointcut method(* Stop())" + 
				"     advice(AuthorizationAdvice)" + 
				"   end" + 
				"   " + 
				" end ";

			AspectLanguageEngineBuilder builder = new AspectLanguageEngineBuilder( weavingRules  );
			AspectEngine engine = builder.Build();

			NASDAQHeartBeatService nasdaqService=engine.WrapClass(typeof(NASDAQHeartBeatService)) as NASDAQHeartBeatService;
			nasdaqService.Start();
			Console.ReadLine();
			


		}

	}
}
