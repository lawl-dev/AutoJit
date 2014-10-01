using System;

namespace AutoJITRuntime
{
    public sealed class NullVariabt : Variant
    {
        protected override DataType DataType {
            get { return DataType.Null; }
        }

        public override object GetValue() {
            throw new NotImplementedException();
        }

        public override string GetString() {
            throw new NotImplementedException();
        }

        public override bool GetBool() {
            throw new NotImplementedException();
        }

        public override double GetDouble() {
            throw new NotImplementedException();
        }

        public override long GetInt64() {
            throw new NotImplementedException();
        }

        public override int GetInt() {
            throw new NotImplementedException();
        }

        public override IntPtr GetIntPtr() {
            throw new NotImplementedException();
        }

        public override byte[] GetBinary() {
            throw new NotImplementedException();
        }

        public override Type GetRealType() {
            throw new NotImplementedException();
        }
    }
}