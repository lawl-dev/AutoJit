using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.Service;

namespace AutoJIT.Infrastructure
{
    public abstract class ComponentContainerBase : IDependencyContainer
    {
        private readonly Dictionary<Type, Func<object>> _registrations = new Dictionary<Type, Func<object>>();

        protected ComponentContainerBase() {
            Bind();
        }

        protected abstract void Bind();

        protected void RegisterModule( ComponentContainerBase module ) {
            foreach (var registration in module._registrations) {
                _registrations.Add( registration.Key, registration.Value );
            }
        }

        protected void Bind<TService, TImplementation>()
            where TImplementation : TService
        {
            _registrations.Add(typeof(TService),
                () => GetInstance<TImplementation>());
        }

        protected void Bind<TService>(Func<TService> instanceCreator)
        {
            _registrations.Add(typeof(TService), () => instanceCreator());
        }

        protected void BindSingle<TService>(TService instance)
        {
            _registrations.Add(typeof(TService), () => instance);
        }

        protected void BindSingle<TService>(Func<TService> instanceCreator)
        {
            var lazy = new Lazy<TService>(instanceCreator);
            Bind(() => lazy.Value);
        }

        public TService GetInstance<TService>() {
            var instance = GetInstance(typeof(TService));
            return (TService)instance;
        }

        public object GetInstance(Type serviceType)
        {
            Func<object> creator;
            if (!_registrations.TryGetValue(serviceType, out creator))
            {
                if (!serviceType.IsAbstract)
                {
                    return CreateConcreteType(serviceType);
                }
            }

            if (serviceType.IsAssignableFrom(typeof(IInjectionService)))
            {
                return new InjectionService(this);
            }

            return _registrations[serviceType]();
        }

        private object CreateConcreteType(Type implementationType)
        {
            var ctor = implementationType.GetConstructors().Single();

            var parameters = (
                ctor.GetParameters().Select( parameter => GetInstance( parameter.ParameterType ) ) )
                .ToArray();

            return CreateInstance<object>(implementationType, parameters );
        }

        private T CreateInstance<T>(Type src, object[] parameter)
        {
            var parameterTypes = parameter.Select(x => x.GetType()).ToArray();
            var constructorInfo = src.GetConstructor(parameterTypes);
            if (constructorInfo != null)
                return (T)constructorInfo.Invoke(parameter);
            return default(T);
        }
    }
}
