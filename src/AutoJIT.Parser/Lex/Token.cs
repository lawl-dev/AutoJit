using System;
using System.Globalization;

namespace AutoJIT.Parser.Lex
{
    public sealed class Token
    {
        public Token() {
            Value = new TokenValue();
        }

        public TokenValue Value { get; set; }

        public int Col { get; set; }

        public TokenType Type { get; set; }

        public bool IsLiteral {
            get {
                return Type == TokenType.String
                       || Type == TokenType.Int32
                       || Type == TokenType.Int64
                       || Type == TokenType.Double
                       || Type == TokenType.Macro;
            }
        }

        public bool IsMathExpression {
            get {
                return Type == TokenType.Div
                       || Type == TokenType.Mult
                       || Type == TokenType.Minus
                       || Type == TokenType.Plus
                       || Type == TokenType.Pow;
            }
        }

        public bool IsNumberExpression {
            get {
                return Type == TokenType.Greater
                       || Type == TokenType.GreaterEqual
                       || Type == TokenType.Less
                       || Type == TokenType.LessEqual
                       || Type == TokenType.StringEqual
                       || Type == TokenType.Equal
                       || Type == TokenType.Notequal;
            }
        }

        public bool IsBooleanExpression {
            get {
                return Type == TokenType.OR
                       || Type == TokenType.AND
                       || Type == TokenType.NOT;
            }
        }

        public bool IsAssignExpression {
            get {
                return Type == TokenType.DivAssign
                       || Type == TokenType.MinusAssign
                       || Type == TokenType.MultAssign
                       || Type == TokenType.PlusAssign
                       || Type == TokenType.PowAssign
                       || Type == TokenType.Equal
                       || Type == TokenType.ConcatAssign;
            }
        }

        public bool IsSignOperator {
            get { return Type == TokenType.Plus || Type == TokenType.Minus; }
        }

        public int Line { get; set; }

        public override string ToString() {
            switch (Type) {
                case TokenType.Keyword:
                    return Value.Keyword.ToString();
                case TokenType.Int32:
                    return Value.Int32Value.ToString( CultureInfo.InvariantCulture );
                case TokenType.Int64:
                    return Value.Int64Value.ToString( CultureInfo.InvariantCulture );
                case TokenType.Double:
                    return Value.DoubleValue.ToString( CultureInfo.InvariantCulture );
                case TokenType.Macro:
                    return string.Format( "@{0}", Value.StringValue );
                case TokenType.Variable:
                    return string.Format( "${0}", Value.StringValue );
                case TokenType.String:
                    return string.Format( "'{0}'", Value.StringValue );
                case TokenType.Function:
                case TokenType.Userfunction:
                    return Value.StringValue;
                case TokenType.Comma:
                    return ", ";
                case TokenType.Plus:
                    return "+";
                case TokenType.Minus:
                    return "-";
                case TokenType.Div:
                    return "/";
                case TokenType.Greater:
                    return ">";
                case TokenType.Less:
                    return "<";
                case TokenType.Equal:
                    return "=";
                case TokenType.StringEqual:
                    return "==";
                case TokenType.LessEqual:
                    return "<=";
                case TokenType.GreaterEqual:
                    return ">=";
                case TokenType.Notequal:
                    return "<>";
                case TokenType.Mult:
                    return "*";
                case TokenType.AND:
                    return "AND";
                case TokenType.OR:
                    return "OR";
                case TokenType.NOT:
                    return "NOT";
                case TokenType.Pow:
                    return "^";
                case TokenType.Rightsubscript:
                    return "]";
                case TokenType.Leftsubscript:
                    return "[";
                case TokenType.Rightparen:
                    return ")";
                case TokenType.Leftparen:
                    return "(";
                case TokenType.DivAssign:
                    return "/=";
                case TokenType.MinusAssign:
                    return "-=";
                case TokenType.MultAssign:
                    return "*=";
                case TokenType.PlusAssign:
                    return "+=";
                case TokenType.PowAssign:
                    return "^=";
                case TokenType.QuestionMark:
                    return "?";
                case TokenType.DoubleDot:
                    return ":";
                case TokenType.NewLine:
                    return Environment.NewLine;
                case TokenType.ConcatAssign:
                    return "&=";
                case TokenType.Concat:
                    return "&";
            }
            return base.ToString();
        }
    }
}
