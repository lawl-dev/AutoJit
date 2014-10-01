using System;

namespace AutoJITRuntime
{
    public sealed class PtrVariant : Variant
    {
        private readonly IntPtr _value;

        public PtrVariant( IntPtr ptr ) {
            _value = ptr;
        }

        protected override DataType DataType {
            get { return DataType.IntPtr; }
        }

        public override object GetValue() {
            return _value;
        }

        public override string GetString() {
            return _value.ToString("X");
        }

        public override bool GetBool() {
            return false;
        }

        public override double GetDouble() {
            return _value.ToInt32();
        }

        public override long GetInt64() {
            return _value.ToInt64();
        }

        public override int GetInt() {
            return _value.ToInt32();
        }

        public override IntPtr GetIntPtr() {
            return _value;
        }

        public override byte[] GetBinary() {
            return BitConverter.GetBytes(_value.ToInt32());
        }

        public override Type GetRealType() {
            return typeof (IntPtr);
        }
    }
}