using System;
using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    public class MarshalInfo
    {
        public MarshalInfo( object parameter, Type type, UnmanagedType? marshalAttribute, bool isRef ) {
            Parameter = parameter;
            IsRef = isRef;
            Type = IsRef
            ? type.MakeByRefType()
            : type;
            MarshalAttribute = marshalAttribute;
        }

        public object Parameter {
            get;
            private set;
        }
        public Type Type {
            get;
            private set;
        }
        public UnmanagedType? MarshalAttribute {
            get;
            private set;
        }
        public bool IsRef {
            get;
            private set;
        }
    }
}
