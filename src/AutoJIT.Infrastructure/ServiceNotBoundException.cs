using System;

namespace AutoJIT.Contrib
{
    public class ServiceNotBoundException : Exception
    {
        private readonly Type _serviceType;

        public ServiceNotBoundException( Type serviceType ) {
            _serviceType = serviceType;
        }

        public override string ToString() {
            return string.Format( "No binding found for {0}", _serviceType.Name );
        }
    }
}