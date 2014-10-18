using System;

namespace AutoJIT.Parser.Service
{
    public interface IInjectionService
    {
        T Inject<T>();
        T Inject<T>( Type t );
    }
}
