using System;

namespace AutoJITRuntime.Variants
{
    public sealed class DefaultVariant : Variant
    {
        protected override DataType DataType {
            get {
                return DataType.Default;
            }
        }

        public override object GetValue() {
            return "Default";
        }

        public override string GetString() {
            return "Default";
        }

        public override bool GetBool() {
            return false;
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
            throw new NotImplementedException();
        }

        public override byte[] GetBinary() {
            return new byte[] {
                0xFF,
                0xFF,
                0xFF,
                0xFF
            };
        }

        public override Type GetRealType() {
            return typeof(Default);
        }
    }
}
