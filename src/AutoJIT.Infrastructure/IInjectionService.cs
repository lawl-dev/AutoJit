using System;

namespace AutoJIT.Contrib
{
    public interface IInjectionService
    {
        T Inject<T>();
        T Inject<T>( Type t );
    }
}
