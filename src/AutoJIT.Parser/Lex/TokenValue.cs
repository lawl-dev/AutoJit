using System;

namespace AutoJIT.Parser.Lex
{
    public sealed class TokenValue
    {
        private double _doubleValue;
        private int _int32Value;
        private long _int64Value;
        private Keywords _keyword;
        private string _stringValue;
        public object CurrentValue {
            get;
            private set;
        }

        public Keywords Keyword {
            get {
                return _keyword;
            }
            set {
                _keyword = value;
                CurrentValue = value;
            }
        }

        public int Int32Value {
            get {
                return _int32Value;
            }
            set {
                _int32Value = value;
                CurrentValue = value;
            }
        }

        public Int64 Int64Value {
            get {
                return _int64Value;
            }
            set {
                _int64Value = value;
                CurrentValue = value;
            }
        }

        public double DoubleValue {
            get {
                return _doubleValue;
            }
            set {
                _doubleValue = value;
                CurrentValue = value;
            }
        }

        public string StringValue {
            get {
                return _stringValue;
            }
            set {
                _stringValue = value;
                CurrentValue = value;
            }
        }
    }
}
