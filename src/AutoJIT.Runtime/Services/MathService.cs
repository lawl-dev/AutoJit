using System;

namespace AutoJITRuntime.Services
{
    public class MathService
    {
        public Variant Abs( Variant expression ) {
            return Math.Abs( expression );
        }

        public Variant ACos( Variant expression ) {
            return Math.Acos( expression );
        }

        public Variant ATan( Variant expression ) {
            return Math.Atan( expression );
        }

        public Variant BitAND( Variant value1, Variant value2, Variant[] valuen ) {
            int res = value1&(int) value2;
            foreach (Variant variant in valuen) {
                res &= variant;
            }
            return res;
        }

        public Variant BitNOT( Variant value ) {
            return ~value;
        }

        public Variant BitOR( Variant value1, Variant value2, Variant[] valuen ) {
            int res = (int) value1|(int) value2;
            foreach (Variant variant in valuen) {
                res |= variant;
            }
            return res;
        }

        public Variant BitShift( Variant value, Variant shift ) {
            if ( shift > 0 ) {
                return value >> shift.GetInt();
            }
            return value << -shift.GetInt();
        }

        public Variant BitXOR( Variant value1, Variant value2, Variant[] valuen ) {
            int res = (int) value1^(int) value2;
            foreach (Variant variant in valuen) {
                res ^= variant;
            }
            return res;
        }

        public Variant Cos( Variant expression ) {
            return Math.Cos( expression );
        }

        public Variant Ceiling( Variant expression ) {
            return Math.Ceiling( (double) expression );
        }

        public Variant Exp( Variant expression ) {
            return Math.Exp( expression );
        }

        public Variant Floor( Variant expression ) {
            return Math.Floor( (double) expression );
        }

        public Variant Log( Variant expression ) {
            return Math.Log( expression );
        }

        public Variant Mod( Variant value1, Variant value2 ) {
            if ( value1.IsInt32 ||
                 value2.IsInt32 ) {
                return value1.GetInt() % value2.GetInt();
            }

            return value1.GetDouble() % value2.GetDouble();
        }

        public Variant Random( Variant min, Variant max, Variant flag ) {
            throw new NotImplementedException();
        }

        public Variant Round( Variant expression, Variant decimalplaces ) {
            return Math.Round( (double) expression, decimalplaces );
        }

        public Variant Sin( Variant expression ) {
            return Math.Sin( expression );
        }

        public Variant Sqrt( Variant expression ) {
            return Math.Sqrt( expression );
        }

        public Variant SRandom( Variant seed ) {
            throw new NotImplementedException();
        }

        public Variant Tan( Variant expression ) {
            return Math.Tan( expression );
        }
    }
}
