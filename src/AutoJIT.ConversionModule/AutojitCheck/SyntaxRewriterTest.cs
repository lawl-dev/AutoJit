using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Factory;

namespace AutoJIT.CSharpConverter.AutojitCheck
{
	public class SyntaxRewriterTest : SyntaxRewriterBase
	{
		public SyntaxRewriterTest( IAutoitStatementFactory statementFactory ) : base( statementFactory ) {}

		public override ISyntaxNode Visit( BinaryExpression node ) {
			return new TrueLiteralExpression();
		}
	}
}
