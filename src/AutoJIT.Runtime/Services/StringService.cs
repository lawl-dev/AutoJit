using System;
using System.Linq;
using System.Text;

namespace AutoJITRuntime
{
    public class StringService {
        public Variant StringAddCR( Variant variant ) {
            return variant.GetString().Replace("\n", "\r\n");
        }

        public Variant StringCompare( Variant string1, Variant string2, Variant casesense ) {
            throw new System.NotImplementedException();
        }

        public Variant StingInString( Variant variant, Variant substring, Variant casesense, Variant occurrence, Variant start, Variant count ) {
            throw new System.NotImplementedException();
        }

        public Variant StringIsAINum( Variant variant ) {
            throw new System.NotImplementedException();
        }

        public Variant StingIsAlpha( Variant variant ) {
            return variant.GetString().All(char.IsLetter);
        }

        public Variant StingIsASCII( Variant variant ) {
            return variant.GetString().All(c => (c > -1 && c < 128));
        }

        public Variant StingIsDigit( Variant variant ) {
            return variant.GetString().All(char.IsDigit);
        }

        public Variant StingIsFloat( Variant variant ) {
            throw new System.NotImplementedException();
        }

        public Variant StingFormat( Variant formatcontrol, Variant var1, Variant[] varN ) {
            throw new System.NotImplementedException();
        }

        public Variant StingFromASCIIArray( Variant array, Variant start, Variant end, Variant encoding ) {
            throw new System.NotImplementedException();
        }

        public Variant StingIsInt( Variant variant ) {
            throw new System.NotImplementedException();
        }

        public Variant StringIsLower( Variant variant ) {
            return variant.GetString().All(char.IsLower);
        }

        public Variant StringIsSpace( Variant variant ) {
            return variant.GetString().All(char.IsWhiteSpace);
        }

        public Variant StringIsUpper( Variant variant ) {
            return variant.GetString().All(char.IsUpper);
        }

        public Variant StringIsXDigit( Variant variant ) {
            return variant.GetString().All(c => ((c >= 0 && c <= 9) || ((c >= 'a' || c >= 'A') && (c <= 'F' || c <= 'f'))));
        
        }

        public Variant StringLeft( Variant variant, Variant count ) {
            var fullString = variant.GetString();
            if (fullString.Length <= count)
            {
                return fullString;
            }
            return fullString.Substring(0, count);
        }

        public Variant StringLen( Variant variant ) {
            return variant.ToString().Length;
        }

        public Variant StringLower( Variant variant ) {
            return variant.GetString().ToLower();
        }

        public Variant StringMid( string toMid, Variant start, Variant count ) {

            if (start < 1 ||
                 start - 1 > toMid.Length)
            {
                return string.Empty;
            }

            if (start - 1 + count > toMid.Length)
            {
                return toMid.Substring(start - 1, toMid.Length - (start - 1));
            }

            return toMid.Substring(start - 1, count);
        }

        public Variant StringRegExp( Variant test, Variant pattern, Variant flag, Variant offset ) {
            throw new System.NotImplementedException();
        }

        public Variant StringRegExpReplace( Variant test, Variant pattern, Variant replace, Variant count ) {
            throw new System.NotImplementedException();
        }

        public Variant StringReplace( Variant variant, Variant searchstringstart, Variant replacestring, Variant occurrence, Variant casesense ) {
            throw new System.NotImplementedException();
        }

        public Variant StringReverse( Variant variant, Variant flag ) {
            return new String( variant.GetString().Reverse().ToArray() );
        }

        public Variant StringRight( Variant variant, Variant count ) {
            var fullString = variant.GetString();
            if (fullString.Length <= count)
            {
                return fullString;
            }
            return fullString.Substring(fullString.Length - count, count);
        }

        public Variant StringSplit( Variant variant, Variant delimiters, Variant flag ) {
            throw new NotImplementedException();
        }

        public Variant StringStripCR( Variant variant ) {
            return variant.GetString().Replace("\r", string.Empty);
        }

        public Variant StringStripWS( Variant variant, Variant flag ) {
            return variant.GetString().Replace(" ", string.Empty);
        }

        public Variant StringToASCIIArray( string toConvert, Variant start, Variant end, Variant encoding ) {
            if (start + end >= toConvert.Length)
            {
                return string.Empty;
            }

            byte[] bytes;

            switch (encoding.GetInt())
            {
                case 0:
                    bytes = Encoding.Unicode.GetBytes(toConvert.Substring(start, end));
                    break;
                case 1:
                    bytes = Encoding.Default.GetBytes(toConvert.Substring(start, end));
                    break;
                case 2:
                    bytes = Encoding.UTF8.GetBytes(toConvert.Substring(start, end));
                    break;
                default:
                    return string.Empty;
            }

            return bytes.Select(x => Variant.Create((int)x)).ToArray();

        }

        public Variant StringTrimLeft( Variant variant, Variant count ) {
            var toTrim = variant.GetString();
            if (toTrim.Length - count <= 0)
            {
                return string.Empty;
            }
            return toTrim.Substring(count, toTrim.Length - count);
        }

        public Variant StringTrimRight( Variant variant, Variant count ) {
            var toTrim = variant.GetString();
            if (toTrim.Length - count <= 0)
            {
                return string.Empty;
            }
            return toTrim.Substring(0, toTrim.Length - count);
        }

        public Variant StringUpper( Variant variant ) {
            return variant.GetString().ToUpper();
        }
    }
}