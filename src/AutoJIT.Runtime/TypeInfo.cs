using System;

namespace AutoJITRuntime
{
    internal class TypeInfo
    {
        public TypeInfo( Type type, int arrayLength, bool @ref ) {
            Ref = @ref;
            ArrayLength = arrayLength;
            if( arrayLength > 0 ) {
                Type = type.MakeArrayType();
            }
        }

        public int ArrayLength {
            get;
            private set;
        }
        public Type Type {
            get;
            private set;
        }
        public bool Ref {
            get;
            private set;
        }

        public bool IsArray {
            get {
                return Type.IsArray;
            }
        }
    }
}
