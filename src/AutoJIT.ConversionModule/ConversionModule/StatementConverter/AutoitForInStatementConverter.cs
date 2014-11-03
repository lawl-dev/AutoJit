using System.Collections.Generic;
using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
	internal sealed class AutoitForInStatementConverter : AutoitStatementConverterBase<ForInStatement>
	{
		public AutoitForInStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

		public override IEnumerable<StatementSyntax> Convert( ForInStatement statement, IContextService context ) {
			var toReturn = new List<StatementSyntax>();

			context.RegisterLocal( statement.VariableExpression.IdentifierName );

			string variableName = context.GetVariableName( statement.VariableExpression.IdentifierName );
			IEnumerable<StatementSyntax> block = statement.Block.SelectMany( x => ConvertGeneric( x, context ) );

			toReturn.Add( CSharpStatementFactory.CreateForInStatement( variableName, ConvertGeneric( statement.ToEnumerate, context ), block ) );

			return toReturn;
		}
	}
}
