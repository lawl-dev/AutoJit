using System.Collections.Generic;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitDoUntilStatementConverter : AutoitStatementConverterBase<DoUntilStatement>
    {
        public AutoitDoUntilStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( DoUntilStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            context.RegisterLoop();

            string coninueLoopLabelName = context.GetConinueLoopLabelName();
            string exitLoopLabelName = context.GetExitLoopLabelName();

            List<StatementSyntax> block = statement.Block.Block.SelectMany( x => ConvertGeneric( x, context ) ).ToList();
            block.Add( SyntaxFactory.LabeledStatement( coninueLoopLabelName, SyntaxFactory.EmptyStatement() ) );

            toReturn.Add( SyntaxFactory.DoStatement( block.ToBlock(), SyntaxFactory.PrefixUnaryExpression( SyntaxKind.LogicalNotExpression, ConvertGeneric( statement.Condition, context ) ) ) );
            toReturn.Add( SyntaxFactory.LabeledStatement( exitLoopLabelName, SyntaxFactory.EmptyStatement() ) );

            context.UnregisterLoop();

            return toReturn;
        }
    }
}
