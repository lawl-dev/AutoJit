using System;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.Parser.Lex
{
	public sealed class TokenFactory : ITokenFactory
	{
		public Token CreateLeftparen( int pos, int line ) {
			return CreateToken( TokenType.Leftparen, pos, line );
		}

		public Token CreateRightparen( int pos, int line ) {
			return CreateToken( TokenType.Rightparen, pos, line );
		}

		public Token CreateEndline( int pos, int line ) {
			return CreateToken( TokenType.NewLine, pos, line );
		}

		public Token CreatePlusAssign( int pos, int line ) {
			return CreateToken( TokenType.PlusAssign, pos, line );
		}

		public Token CreatePlus( int pos, int line ) {
			return CreateToken( TokenType.Plus, pos, line );
		}

		public Token CreateMinusAssign( int pos, int line ) {
			return CreateToken( TokenType.MinusAssign, pos, line );
		}

		public Token CreateMinus( int pos, int line ) {
			return CreateToken( TokenType.Minus, pos, line );
		}

		public Token CreateDivAssign( int pos, int line ) {
			return CreateToken( TokenType.DivAssign, pos, line );
		}

		public Token CreateDiv( int pos, int line ) {
			return CreateToken( TokenType.Div, pos, line );
		}

		public Token CreatePowAssign( int pos, int line ) {
			return CreateToken( TokenType.PowAssign, pos, line );
		}

		public Token CreatePow( int pos, int line ) {
			return CreateToken( TokenType.Pow, pos, line );
		}

		public Token CreateMultAssign( int pos, int line ) {
			return CreateToken( TokenType.MultAssign, pos, line );
		}

		public Token CreateMult( int pos, int line ) {
			return CreateToken( TokenType.Mult, pos, line );
		}

		public Token CreateStringEqual( int pos, int line ) {
			return CreateToken( TokenType.StringEqual, pos, line );
		}

		public Token CreateEqual( int pos, int line ) {
			return CreateToken( TokenType.Equal, pos, line );
		}

		public Token CreateComma( int pos, int line ) {
			return CreateToken( TokenType.Comma, pos, line );
		}

		public Token CreateConcat( int pos, int line ) {
			return CreateToken( TokenType.Concat, pos, line );
		}

		public Token CreateLeftsubscript( int pos, int line ) {
			return CreateToken( TokenType.Leftsubscript, pos, line );
		}

		public Token CreateRightsubscript( int pos, int line ) {
			return CreateToken( TokenType.Rightsubscript, pos, line );
		}

		public Token CreateNotEqual( int pos, int line ) {
			return CreateToken( TokenType.Notequal, pos, line );
		}

		public Token CreateLessEqual( int pos, int line ) {
			return CreateToken( TokenType.LessEqual, pos, line );
		}

		public Token CreateLess( int pos, int line ) {
			return CreateToken( TokenType.Less, pos, line );
		}

		public Token CreateGreaterEqual( int pos, int line ) {
			return CreateToken( TokenType.GreaterEqual, pos, line );
		}

		public Token CreateGreater( int pos, int line ) {
			return CreateToken( TokenType.Greater, pos, line );
		}

		public Token CreateOr( int pos, int line ) {
			return CreateToken( TokenType.OR, pos, line );
		}

		public Token CreateAnd( int pos, int line ) {
			return CreateToken( TokenType.AND, pos, line );
		}

		public Token CreateNot( int pos, int line ) {
			return CreateToken( TokenType.NOT, pos, line );
		}

		public Token CreateInt( int value, int pos, int line ) {
			Token res = CreateToken( TokenType.Int32, pos, line );
			res.Value.Int32Value = value;
			return res;
		}

		public Token CreateDouble( double value, int pos, int line ) {
			Token res = CreateToken( TokenType.Double, pos, line );
			res.Value.DoubleValue = value;
			return res;
		}

		public Token CreateInt64( Int64 value, int pos, int line ) {
			Token res = CreateToken( TokenType.Int64, pos, line );
			res.Value.Int64Value = value;
			return res;
		}

		public Token CreateKeyword( Keywords keywords, int pos, int line ) {
			Token res = CreateToken( TokenType.Keyword, pos, line );
			res.Value.Keyword = keywords;
			return res;
		}

		public Token CreateFunction( string functionName, int pos, int line ) {
			Token res = CreateToken( TokenType.Function, pos, line );
			res.Value.StringValue = functionName;
			return res;
		}

		public Token CreaeteUserfunction( string functionName, int pos, int line ) {
			Token res = CreateToken( TokenType.Userfunction, pos, line );
			res.Value.StringValue = string.Format( "f_{0}", functionName );
			return res;
		}

		public Token CreateVariable( string variableName, int pos, int line ) {
			Token res = CreateToken( TokenType.Variable, pos, line );
			res.Value.StringValue = string.Format( "v_{0}", variableName );
			return res;
		}

		public Token CreateMacro( string macroName, int pos, int line ) {
			Token res = CreateToken( TokenType.Macro, pos, line );
			res.Value.StringValue = macroName;
			return res;
		}

		public Token CreateString( string value, int pos, int line ) {
			Token res = CreateToken( TokenType.String, pos, line );
			res.Value.StringValue = value;
			return res;
		}

		public Token CreateConcatJoin( int pos, int line ) {
			return CreateToken( TokenType.ConcatAssign, pos, line );
		}

		public Token CreateUndefined( int pos, int line ) {
			return CreateToken( TokenType.None, pos, line );
		}

		public Token CreateQuestionMark( int pos, int line ) {
			return CreateToken( TokenType.QuestionMark, pos, line );
		}

		public Token CreateDoubleDot( int pos, int line ) {
			return CreateToken( TokenType.DoubleDot, pos, line );
		}

		public Token CreateNull( int pos, int line ) {
			return CreateToken( TokenType.Null, pos, line );
		}

		public Token CreateContinueLine( int pos, int lineNum ) {
			return CreateToken( TokenType.ContinueLine, pos, lineNum );
		}

		private Token CreateToken( TokenType type, int pos, int line ) {
			return new Token {
				Type = type,
				Col = pos,
				Line = line
			};
		}
	}
}
