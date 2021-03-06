using System;

namespace AutoJITRuntime.Variants
{
    public class BoolVariant : Variant
    {
        private readonly bool _value;

        public BoolVariant( Boolean @bool ) {
            _value = @bool;
        }

        public override DataType DataType {
            get { return DataType.Bool; }
        }

        public override bool IsBool {
            get { return true; }
        }

        public override object GetValue() {
            return _value;
        }

        public override string GetString() {
            return _value
                ? "True"
                : "False";
        }

        public override bool GetBool() {
            return _value;
        }

        public override double GetDouble() {
            return _value
                ? 1.0d
                : 0.0d;
        }

        public override long GetInt64() {
            return Convert.ToInt64( _value );
        }

        public override int GetInt() {
            return Convert.ToInt32( _value );
        }

        public override IntPtr GetIntPtr() {
            throw new NotImplementedException();
        }

        public override byte[] GetBinary() {
            throw new NotImplementedException();
        }

        public override Type GetRealType() {
            return typeof (Boolean);
        }
    }
}
