namespace AutoJIT.Parser.Lex.Interface
{
	public interface ITokenFactory
	{
		Token CreateLeftparen( int pos, int line );
		Token CreateRightparen( int pos, int line );
		Token CreateEndline( int pos, int line );
		Token CreatePlusAssign( int pos, int line );
		Token CreatePlus( int pos, int line );
		Token CreateMinusAssign( int pos, int line );
		Token CreateMinus( int pos, int line );
		Token CreateDivAssign( int pos, int line );
		Token CreateDiv( int pos, int line );
		Token CreatePowAssign( int pos, int line );
		Token CreatePow( int pos, int line );
		Token CreateMultAssign( int pos, int line );
		Token CreateMult( int pos, int line );
		Token CreateStringEqual( int pos, int line );
		Token CreateEqual( int pos, int line );
		Token CreateComma( int pos, int line );
		Token CreateConcat( int pos, int line );
		Token CreateLeftsubscript( int pos, int line );
		Token CreateRightsubscript( int pos, int line );
		Token CreateNotEqual( int pos, int line );
		Token CreateLessEqual( int pos, int line );
		Token CreateLess( int pos, int line );
		Token CreateGreaterEqual( int pos, int line );
		Token CreateGreater( int pos, int line );
		Token CreateOr( int pos, int line );
		Token CreateAnd( int pos, int line );
		Token CreateNot( int pos, int line );
		Token CreateInt( int value, int pos, int line );
		Token CreateDouble( double value, int pos, int line );
		Token CreateInt64( long value, int pos, int line );
		Token CreateKeyword( Keywords keywords, int pos, int line );
		Token CreateFunction( string functionName, int pos, int line );
		Token CreaeteUserfunction( string functionName, int pos, int line );
		Token CreateVariable( string variableName, int pos, int line );
		Token CreateMacro( string macroName, int pos, int line );
		Token CreateString( string value, int pos, int line );
		Token CreateConcatJoin( int pos, int line );
		Token CreateUndefined( int pos, int line );
		Token CreateQuestionMark( int pos, int line );
		Token CreateDoubleDot( int pos, int line );
		Token CreateNull( int pos, int line );
		Token CreateContinueLine( int pos, int lineNum );
	}
}
