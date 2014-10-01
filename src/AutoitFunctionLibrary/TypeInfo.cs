using System;

namespace AutoJITRuntime
{
    internal class TypeInfo
    {
        public readonly int ArrayLength;
        public readonly Type Type;

        public bool IsArray {
            get { return Type.IsArray; }
        }

        public TypeInfo(Type type, int arrayLength) {
            ArrayLength = arrayLength;
            if ( arrayLength > 0 ) {
                Type = type.MakeArrayType();   
            }
        }
    }
}