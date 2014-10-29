using System;

namespace AutoJIT.Infrastructure
{
    public interface IDependencyContainer {
        TService GetInstance<TService>();
        object GetInstance(Type serviceType);
    }
}