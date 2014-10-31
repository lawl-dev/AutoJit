using System;

namespace AutoJIT.Infrastructure
{
	public class InjectionService : IInjectionService
	{
		private readonly IDependencyContainer _container;

		public InjectionService( IDependencyContainer container ) {
			_container = container;
		}

		public T Inject<T>() {
			return _container.GetInstance<T>();
		}

		public T Inject<T>( Type t ) {
			return (T)_container.GetInstance( t );
		}
	}
}
