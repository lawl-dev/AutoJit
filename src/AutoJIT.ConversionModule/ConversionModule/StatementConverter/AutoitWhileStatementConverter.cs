using System.Collections.Generic;
using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Statements;
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

			ExpressionSyntax condition = Convert( statement.Condition, context );
			List<StatementSyntax> block = statement.Block.SelectMany( x => ConvertGeneric( x, context ) ).ToList();
			block.Add( SyntaxFactory.LabeledStatement( coninueLoopLabelName, SyntaxFactory.EmptyStatement() ) );

			WhileStatementSyntax whileStatement = CSharpStatementFactory.CreateWhileStatement( condition, block );
			toReturn.Add( whileStatement );

			toReturn.Add( SyntaxFactory.LabeledStatement( exitLoopLabelName, SyntaxFactory.EmptyStatement() ) );
			context.UnregisterLoop();

			return toReturn;
		}
	}
}
