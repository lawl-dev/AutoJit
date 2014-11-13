using System.Collections.Generic;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitFunctionCallStatementConverter : AutoitStatementConverterBase<FunctionCallStatement>
    {
        public AutoitFunctionCallStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( FunctionCallStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            ExpressionSyntax callExpression = ConvertGeneric( statement.FunctionCallExpression, context );

            toReturn.Add( callExpression.ToStatementSyntax() );

            return toReturn;
        }
    }
}
