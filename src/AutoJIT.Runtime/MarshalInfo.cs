using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    public class MarshalInfo
    {
        public object Parameter { get; private set; }
        public Type Type { get; private set; }
        public UnmanagedType? MarshalAttribute { get; private set; }
        public bool IsRef { get; private set; }

        public MarshalInfo( object parameter, Type type, UnmanagedType? marshalAttribute, bool isRef ) {
            Parameter = parameter;
            IsRef = isRef;
            Type = IsRef
                ? type.MakeByRefType()
                : type;
            MarshalAttribute = marshalAttribute;
        }
    }
}
