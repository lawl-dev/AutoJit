using System.Collections.Generic;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitInitDefaultParameterStatementConverter : AutoitStatementConverterBase<InitDefaultParameterStatement>
    {
        public AutoitInitDefaultParameterStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( InitDefaultParameterStatement statement, IContextService context ) {
            return SyntaxFactory.IfStatement( SyntaxFactory.BinaryExpression( SyntaxKind.EqualsExpression, SyntaxFactory.IdentifierName( statement.ParameterName ), SyntaxFactory.LiteralExpression( SyntaxKind.NullLiteralExpression ) ), SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( statement.ParameterName ), ConvertGeneric( statement.DefaultValue, context ) ).ToStatementSyntax() ).ToEnumerable();
        }
    }
}
