using System;
using System.Globalization;
using System.Text;

namespace AutoJITRuntime.Variants
{
    public sealed class StringVariant : Variant
    {
        private readonly string _value;

        public StringVariant( String @string ) {
            _value = @string;
        }

        public override DataType DataType {
            get { return DataType.String; }
        }

        public override bool IsString {
            get { return true; }
        }

        public override object GetValue() {
            return _value;
        }

        public override string GetString() {
            return _value;
        }

        public override bool GetBool() {
            return _value != string.Empty || _value == "True";
        }

        public override double GetDouble() {
            if ( _value.Length == 0 ) {
                return 0;
            }
            bool isHex = _value[0] == '0' && ( _value[1] == 'x' || _value[1] == 'X' );
            if ( isHex ) {
                return int.Parse( _value.Substring( 2 ), NumberStyles.AllowHexSpecifier );
            }
            double result;
            return double.TryParse( _value, NumberStyles.AllowLeadingSign|NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result )
                ? result
                : 0;
        }

        public override long GetInt64() {
            Int64 result;
            return Int64.TryParse( _value, out result )
                ? result
                : 0;
        }

        public override int GetInt() {
            int result;
            return int.TryParse( _value, out result )
                ? result
                : 0;
        }

        public override IntPtr GetIntPtr() {
            throw new NotImplementedException();
        }

        public override byte[] GetBinary() {
            return Encoding.UTF8.GetBytes( _value );
        }

        public override Type GetRealType() {
            return typeof (String);
        }
    }
}
