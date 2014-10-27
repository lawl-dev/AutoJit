using System;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Services
{
    public class VariablesAndConversionsService
    {
        public Variant Asc( Variant variant ) {
            char character = ( (string)variant ).FirstOrDefault();
            return character;
        }

        public Variant AscW( Variant variant ) {
            char character = ( (string)variant ).FirstOrDefault();
            return character;
        }

        public Variant Chr( Variant chr ) {
            if( chr < 0 ) {
                return string.Empty;
            }
            if( chr > 255 ) {
                throw new ASCIIcodeIsGreaterThan255Exception( 1, null, string.Empty );
            }

            return ( (char)chr ).ToString( CultureInfo.InvariantCulture );
        }

        public Variant ChrW( Variant unicodEcode ) {
            if( unicodEcode > ushort.MaxValue ) {
                throw new UNICODEIsGreaterThan65535Exception( unicodEcode, null, string.Empty );
            }

            if( unicodEcode < 0 ) {
                return string.Empty;
            }

            return (char)unicodEcode;
        }

        public Variant Binary( Variant expression ) {
            return expression.GetBinary();
        }

        public Variant BinaryLen( Variant binary ) {
            return binary.GetBinary().Length;
        }

        public Variant BinaryMid( Variant binary, Variant start, Variant count ) {
            byte[] bytes = binary.GetBinary();
            if( start < 1
                || start >= bytes.Length
                || ( count != null && start >= count ) ) {
                return new byte[0];
            }
            return bytes.Skip( start-1 ).Take( count ?? bytes.Length-start-1 ).ToArray();
        }

        public Variant BinaryToString( Variant expression, Variant flag ) {
            string @string = expression.GetString();

            if( @string.Length % 2 != 0 ) {
                throw new OddNumberOfBytesException( 2, null, string.Empty );
            }
            if( @string.Length == 0 ) {
                throw new StringHadZeroLengthException( 1, null, string.Empty );
            }

            Encoding encoding;
            switch(flag.GetInt()) {
                case 1:
                    encoding = Encoding.Default;
                    break;
                case 2:
                    encoding = Encoding.Unicode;
                    break;
                case 3:
                    encoding = Encoding.BigEndianUnicode;
                    break;
                default:
                    encoding = Encoding.UTF8;
                    break;
            }

            if( @string.StartsWith( "0x", StringComparison.InvariantCultureIgnoreCase )
                && @string.Skip( 2 ).All( c => ( c >= '0' && c <= '9' ) || ( c >= 'a' && c <= 'f' ) || ( c >= 'A' && c <= 'F' ) ) ) {
                string hex = @string.Substring( 2, @string.Length-2 );
                var raw = new byte[hex.Length / 2];
                for( int i = 0; i < raw.Length; i++ ) {
                    raw[i] = Convert.ToByte( hex.Substring( i * 2, 2 ), 16 );
                }
                return encoding.GetString( raw );
            }

            return encoding.GetString( expression.GetBinary() );
        }

        public Variant Dec( Variant hex, Variant flag ) {
            Int64 result;
            if( Int64.TryParse( hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result ) ) {
                if( result <= int.MaxValue
                    && result >= int.MinValue ) {
                    return (int)result;
                }
                return result;
            }
            throw new InvalidHexStringException( 1, null, 0 );
        }

        public Variant Hex( Variant expression, Variant length ) {
            if( expression.IsInt32 ) {
                if( length == null
                    || length.IsDefault ) {
                    return expression.GetInt().ToString( "x8" ).ToUpper();
                }
                if( length > 16 ) {
                    length = 16;
                }
                return expression.GetInt().ToString( "x"+length ).ToUpper();
            }
            if( expression.IsInt64 ) {
                if( length == null
                    || length.IsDefault ) {
                    return expression.GetInt64().ToString( "x16" ).ToUpper();
                }
                if( length > 16 ) {
                    length = 16;
                }
                return expression.GetInt().ToString( "x"+length ).ToUpper();
            }
            if( expression.IsPtr ) {
                IntPtr intPtr = expression.GetIntPtr();
                int size = IntPtr.Size * 2;
                return intPtr.ToString( "x"+size );
            }

            if( expression.IsBinary ) {
                byte[] bytes = expression.GetBinary();
                var c = new char[bytes.Length * 2];
                for( int i = 0; i < bytes.Length; i++ ) {
                    int b = bytes[i] >> 4;
                    c[i * 2] = (char)( 55+b+( ( ( b-10 ) >> 31 )&-7 ) );
                    b = bytes[i]&0xF;
                    c[i * 2+1] = (char)( 55+b+( ( ( b-10 ) >> 31 )&-7 ) );
                }
                return new string( c ).ToUpper();
            }
            throw new NotImplementedException();
        }

        public Variant HWnd( Variant expression ) {
            throw new NotImplementedException();
        }

        public Variant Int( Variant expression, Variant flag ) {
            throw new NotImplementedException();
        }

        public Variant IsAdmin() {
            throw new NotImplementedException();
        }

        public Variant IsArray( Variant variable ) {
            return variable.IsArray;
        }

        public Variant IsBinary( Variant variable ) {
            return variable.IsBinary;
        }

        public Variant IsBool( Variant variable ) {
            return variable.IsBool;
        }

        public Variant IsDllStruct( Variant variable ) {
            return variable.IsStruct;
        }

        public Variant IsFunc( Variant expression ) {
            throw new NotImplementedException();
        }

        public Variant IsFloat( Variant variable ) {
            return variable.IsDouble;
        }

        public Variant IsHWnd( Variant variable ) {
            return variable.IsPtr && MarshalService.IsWindow( variable );
        }

        public Variant IsInt( Variant variable ) {
            return variable.IsInt32 || variable.IsInt64;
        }

        public Variant IsKeyword( Variant variable ) {
            return variable.IsDefault || variable.IsNull;
        }

        public Variant IsNumber( Variant variable ) {
            return variable.IsInt32 || variable.IsInt64 || variable.IsDouble;
        }

        public Variant IsObj( Variant variable ) {
            throw new NotImplementedException();
        }

        public Variant IsPtr( Variant variable ) {
            return variable.IsPtr;
        }

        public Variant IsString( Variant variable ) {
            return variable.IsString;
        }

        public Variant Number( Variant expression, Variant flag ) {
            throw new NotImplementedException();
        }

        public Variant Ptr( Variant expression ) {
            return expression.GetIntPtr();
        }

        public Variant String( Variant expression ) {
            return expression.GetString();
        }

        public Variant StringToBinary( Variant expression, Variant flag ) {
            string @string = expression.GetString();

            Encoding encoding;
            switch(flag.GetInt()) {
                case 1:
                    encoding = Encoding.Default;
                    break;
                case 2:
                    encoding = Encoding.Unicode;
                    break;
                case 3:
                    encoding = Encoding.BigEndianUnicode;
                    break;
                default:
                    encoding = Encoding.UTF8;
                    break;
            }

            return encoding.GetBytes( expression.GetString() );
        }

        public Variant UBound( Variant Array, Variant dimension ) {
            var array = Array.GetValue() as Array;

            if( array == null ) {
                throw new ArrayGivenIsNotAnArrayException( 1, null, 0 );
            }

            switch(dimension.GetInt()) {
                case 0:
                    return array.Rank;
                case 1:
                    return array.GetLength( 0 );
                case 2:
                    return array.GetLength( 1 );
                default:
                    throw new ArrayDimensionIsInvalidException( 2, null, 0 );
            }
        }
    }
}
