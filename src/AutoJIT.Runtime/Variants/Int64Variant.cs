using System;
using System.Globalization;

namespace AutoJITRuntime.Variants
{
    public class Int64Variant : Variant
    {
        private readonly Int64 _value;

        public Int64Variant( Int64 int64 ) {
            _value = int64;
        }

        protected override DataType DataType {
            get {
                return DataType.Int64;
            }
        }

        public override bool IsInt64 {
            get {
                return true;
            }
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
            return (int)_value;
        }

        public override IntPtr GetIntPtr() {
            return new IntPtr( _value );
        }

        public override byte[] GetBinary() {
            return BitConverter.GetBytes( _value );
        }

        public override Type GetRealType() {
            return typeof(Int64);
        }
    }
}
