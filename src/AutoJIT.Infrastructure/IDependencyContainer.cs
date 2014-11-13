using System;

namespace AutoJIT.Contrib
{
    public interface IDependencyContainer
    {
        TService GetInstance<TService>();
        object GetInstance( Type serviceType );
    }
}
