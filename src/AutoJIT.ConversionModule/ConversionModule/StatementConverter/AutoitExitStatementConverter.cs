using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitExitStatementConverter : AutoitStatementConverterBase<ExitStatement>
    {
        public AutoitExitStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService )
            : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ExitStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            var exitFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Exit( 0 ) );

            toReturn.Add(
                CSharpStatementFactory.CreateInvocationExpression(
                    context.GetRuntimeInstanceName(), exitFunctionName,
                    CompilerHelper.GetParameterInfo(
                        exitFunctionName, statement.ExpressionNode == null
                            ? SyntaxFactory.LiteralExpression( SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal( 0 ) )
                            : Convert( statement.ExpressionNode, context ) ) ).ToStatementSyntax() );

            return toReturn;
        }
    }
}
