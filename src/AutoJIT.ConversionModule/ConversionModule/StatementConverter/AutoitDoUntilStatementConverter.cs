using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitDoUntilStatementConverter : AutoitStatementConverterBase<DoUntilStatement>
    {
        public AutoitDoUntilStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService )
            : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( DoUntilStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            context.RegisterLoop();

            string coninueLoopLabelName = context.GetConinueLoopLabelName();
            string exitLoopLabelName = context.GetExitLoopLabelName();

            List<StatementSyntax> block = statement.Block.SelectMany( x => ConvertGeneric( x, context ) ).ToList();
            block.Add( SyntaxFactory.LabeledStatement( coninueLoopLabelName, SyntaxFactory.EmptyStatement() ) );

            toReturn.Add(SyntaxFactory.DoStatement( block.ToBlock(), SyntaxFactory.PrefixUnaryExpression( SyntaxKind.LogicalNotExpression, Convert( statement.Condition, context ) ) ) );
            toReturn.Add( SyntaxFactory.LabeledStatement( exitLoopLabelName, SyntaxFactory.EmptyStatement() ) );


            context.UnregisterLoop();

            return toReturn;
        }
    }
}
