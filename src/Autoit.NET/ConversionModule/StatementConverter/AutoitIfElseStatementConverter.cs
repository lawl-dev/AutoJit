using System.Collections.Generic;
using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Visitor;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitIfElseStatementConverter : AutoitStatementConverterBase<IfElseStatement>
    {
        private readonly ICSharpStatementFactory _cSharpStatementFactory;

        public AutoitIfElseStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService)
            : base( cSharpStatementFactory, injectionService) {
            _cSharpStatementFactory = cSharpStatementFactory;
        }

        public override IEnumerable<StatementSyntax> Convert(IfElseStatement statement, IContextService context)
        {
            var toReturn = new List<StatementSyntax>();

            var ifStatement =
                _cSharpStatementFactory.CreateIfStatement(
                    Convert(statement.Condition, context),
                    statement.IfBlock.SelectMany( x => ConvertGeneric(x, context) ) );

            var elseIfs = new List<IfStatementSyntax>();

            if ( statement.ElseIfConditions != null ) {
                for ( var i = 0; i < statement.ElseIfConditions.Count(); i++ ) {
                    var innerIfStatement =
                        _cSharpStatementFactory.CreateIfStatement(
                            Convert(statement.ElseIfConditions.ElementAt( i ), context),
                            statement.ElseIfBlocks.ElementAt( i ).SelectMany( x => ConvertGeneric(x, context) ) );
                    elseIfs.Add( innerIfStatement );
                }
            }

            if ( elseIfs.Count > 1 ) {
                for ( var index = elseIfs.Count-1; index > 0; index-- ) {
                    var innerIf = elseIfs[index];
                    elseIfs[index-1] = elseIfs[index-1].WithElse( innerIf.ToElseClause() );
                }
            }

            if ( elseIfs.Any() ) {
                if ( statement.ElseBlock != null ) {
                    elseIfs[0] =
                        elseIfs[0].WithElse(
                            statement.ElseBlock.SelectMany( x => ConvertGeneric(x, context) )
                                .ToBlock()
                                .ToElseClause() );
                }
                ifStatement = ifStatement.WithElse( elseIfs[0].ToElseClause() );
            }
            else {
                if ( statement.ElseBlock != null ) {
                    ifStatement =
                        ifStatement.WithElse(
                            statement.ElseBlock.SelectMany( x => ConvertGeneric(x, context) )
                                .ToBlock()
                                .ToElseClause() );
                }
            }
            toReturn.Add( ifStatement );

            return toReturn;
        }
    }
}
