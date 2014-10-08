using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitRedimStatementConverter : AutoitStatementConverterBase<ReDimStatement>
    {
        public AutoitRedimStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService)
            : base( cSharpStatementFactory, injectionService) {}

        public override IEnumerable<StatementSyntax> Convert(ReDimStatement statement, IContextService context)
        {
            var toReturn = new List<StatementSyntax>();

            toReturn.Add(
                CSharpStatementFactory.CreateInvocationExpression(
                    statement.ArrayExpression.IdentifierName, CompilerHelper.GetVariantMemberName( x => x.ReDim() ),
                    statement.ArrayExpression.AccessParameter.Select(
                        x => new CSharpParameterInfo( Convert(x, context), false ) ) )
                    .ToStatementSyntax() );

            return toReturn;
        }
    }
}
