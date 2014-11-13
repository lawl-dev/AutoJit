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
    internal sealed class AutoitWhileStatementConverter : AutoitStatementConverterBase<WhileStatement>
    {
        public AutoitWhileStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( WhileStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            context.RegisterLoop();

            string exitLoopLabelName = context.GetExitLoopLabelName();
            string coninueLoopLabelName = context.GetConinueLoopLabelName();

            ExpressionSyntax condition = ConvertGeneric( statement.Condition, context );
            List<StatementSyntax> block = statement.Block.Block.SelectMany( x => ConvertGeneric( x, context ) ).ToList();
            block.Add( SyntaxFactory.LabeledStatement( coninueLoopLabelName, SyntaxFactory.EmptyStatement() ) );

            WhileStatementSyntax whileStatement = CSharpStatementFactory.CreateWhileStatement( condition, block.ToBlock() );
            toReturn.Add( whileStatement );

            toReturn.Add( SyntaxFactory.LabeledStatement( exitLoopLabelName, SyntaxFactory.EmptyStatement() ) );
            context.UnregisterLoop();

            return toReturn;
        }
    }
}
