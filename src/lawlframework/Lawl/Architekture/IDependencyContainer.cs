using System;

namespace Lawl.Architekture
{
    public interface IDependencyContainer {
        TService GetInstance<TService>();
        object GetInstance(Type serviceType);
    }
}