using System;
using System.Globalization;
using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Variants
{
    public class DoubleVariant : Variant
    {
        private readonly double _value;

        public DoubleVariant( double value ) {
            if ( Math.Abs( value-Math.Floor( value ) ) <= double.Epsilon ) {
                if ( value > int.MaxValue
                     ||
                     value < int.MinValue ) {
                    _value = (Int64) value;
                    return;
                }
                _value = (int) value;
                return;
            }
            _value = value;
        }

        protected override DataType DataType {
            get { return DataType.Double; }
        }

        public override bool IsDouble {
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
            return (long) _value;
        }

        public override int GetInt() {
            return (int) _value;
        }

        public override IntPtr GetIntPtr() {
            throw new AutoJITRuntimerException( "" );
        }

        public override byte[] GetBinary() {
            return BitConverter.GetBytes( GetDouble() );
        }

        public override Type GetRealType() {
            return typeof (Double);
        }
    }
}
