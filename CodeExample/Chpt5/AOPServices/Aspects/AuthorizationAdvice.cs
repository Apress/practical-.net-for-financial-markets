using System;
using AopAlliance.Intercept;
using System.Threading;
using AOPServices.Services;

namespace AOPServices.Aspects
{
	public class AuthorizationAdvice : IMethodInterceptor
	{
		public AuthorizationAdvice()
		{
		}

		public object Invoke(IMethodInvocation invocation)
		{
			//Perform Authorization Check
			/*if ( !Thread.CurrentPrincipal.IsInRole("Manager"))
				throw new ApplicationException("Access Denied");*/
			
			Console.WriteLine("Pre-Authorization Code");
			invocation.Proceed();
			Console.WriteLine("Post-Authorization Code");
			return null;
		}

	}
}
