using System;
using System.Collections;
using System.Globalization;
using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Variants
{
    public class Int32Variant : Variant
    {
        private readonly Int32 _value;

        public Int32Variant( Int32 value ) {
            _value = value;
        }

        public override DataType DataType {
            get { return DataType.Int32; }
        }

        public override bool IsInt32 {
            get { return true; }
        }

        public override object GetValue() {
            return _value;
        }

        public override string GetString() {
            return _value.ToString( CultureInfo.InvariantCulture );
        }

        public override bool GetBool() {
            return _value > 0;
        }

        public override double GetDouble() {
            return _value;
        }

        public override long GetInt64() {
            return _value;
        }

        public override int GetInt() {
            return _value;
        }

        public override IntPtr GetIntPtr() {
            return new IntPtr( _value );
        }

        public override byte[] GetBinary() {
            return BitConverter.GetBytes( _value );
        }

        public override Type GetRealType() {
            return typeof (Int32);
        }

        public override IEnumerator GetEnumerator() {
            throw new AutoJITRuntimerException( "In32 Enumeration" );
        }
    }
}
