using System;

namespace AutoJIT.Infrastructure
{
	public interface IInjectionService
	{
		T Inject<T>();
		T Inject<T>( Type t );
	}
}
