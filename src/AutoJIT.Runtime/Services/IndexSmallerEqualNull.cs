using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Services
{
    public class IndexSmallerEqualNull : AutoJITExceptionBase
    {
        public IndexSmallerEqualNull( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}