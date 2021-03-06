using System;

namespace AutoJITRuntime.Variants
{
    public sealed class NullVariant : Variant
    {
        public override DataType DataType {
            get { return DataType.Null; }
        }

        public override bool IsNull {
            get { return true; }
        }

        public override object GetValue() {
            throw new NotImplementedException();
        }

        public override string GetString() {
            return string.Empty;
        }

        public override bool GetBool() {
            return true;
        }

        public override double GetDouble() {
            return 0.0d;
        }

        public override long GetInt64() {
            return 0;
        }

        public override int GetInt() {
            return 0;
        }

        public override IntPtr GetIntPtr() {
            return new IntPtr( 0 );
        }

        public override byte[] GetBinary() {
            return new byte[] {
                0x00,
                0x00,
                0x00,
                0x00
            };
        }

        public override Type GetRealType() {
            throw new NotImplementedException();
        }
    }
}
