using System.Collections.Generic;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
	internal sealed class AutoitExitStatementConverter : AutoitStatementConverterBase<ExitStatement>
	{
		public AutoitExitStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

		public override IEnumerable<StatementSyntax> Convert( ExitStatement statement, IContextService context ) {
			var toReturn = new List<StatementSyntax>();

			string exitFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Exit( 0 ) );
			string runtimeInstanceName = context.GetRuntimeInstanceName();

			ExpressionSyntax exitExpression = statement.ExitExpression == null
			? SyntaxFactory.LiteralExpression( SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal( 0 ) )
			: ConvertGeneric( statement.ExitExpression, context );

			toReturn.Add( CSharpStatementFactory.CreateInvocationExpression( runtimeInstanceName, exitFunctionName, CompilerHelper.GetParameterInfo( exitFunctionName, exitExpression ) ).ToStatementSyntax() );

			return toReturn;
		}
	}
}
