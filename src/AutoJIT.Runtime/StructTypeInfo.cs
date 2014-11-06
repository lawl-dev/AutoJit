using System;
using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    internal class StructTypeInfo
    {
        public StructTypeInfo( string variableName, Type managedType, UnmanagedType? marshalAs, int arraySize ) {
            VariableName = variableName;
            ManagedType = managedType;
            MarshalAs = marshalAs;
            ArraySize = arraySize;
        }

        public string VariableName { get; private set; }
        public Type ManagedType { get; private set; }
        public UnmanagedType? MarshalAs { get; private set; }
        public int ArraySize { get; private set; }
    }
}
