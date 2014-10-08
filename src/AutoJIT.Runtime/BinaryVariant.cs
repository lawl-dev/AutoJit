using System;
using System.Linq;

namespace AutoJITRuntime
{
    public sealed class BinaryVariant : Variant
    {
        private readonly byte[] _value;

        public BinaryVariant(byte[] value) {
            _value = value;
        }

        protected override DataType DataType {
            get { return DataType.Binary; }
        }

        public override bool IsBinary {
            get { return true; }
        }

        public override object GetValue() {
            return _value;
        }

        public override Variant this[ params int[] index ] {
            get { return (int) _value[index.Single()]; }
            set { _value[index.Single()] = (byte)(int) value; }
        }

        public override string GetString() {
            var c = new char[_value.Length * 2 + 2];
            c[0] = (char)0x30;
            c[1] = (char)0x78;
            for (int i = 0; i < _value.Length; i++)
            {
                int b = _value[i] >> 4;
                c[i * 2 + 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
                b = _value[i] & 0xF;
                c[i * 2 + 1 + 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
            }
            return new string(c);
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
            return _value;
        }

        public override Type GetRealType() {
            return typeof (byte[]);
        }
    }
}