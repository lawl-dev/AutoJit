using System.Collections.Generic;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitIfElseStatementConverter : AutoitStatementConverterBase<IfElseStatement>
    {
        private readonly ICSharpStatementFactory _cSharpStatementFactory;

        public AutoitIfElseStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {
            _cSharpStatementFactory = cSharpStatementFactory;
        }

        public override IEnumerable<StatementSyntax> Convert( IfElseStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            IfStatementSyntax ifStatement = _cSharpStatementFactory.CreateIfStatement( ConvertGeneric( statement.Condition, context ), (BlockSyntax) ConvertGeneric( statement.IfBlock, context ).Single() );

            var elseIfs = new List<IfStatementSyntax>();

            if ( statement.ElseIfConditions != null ) {
                for ( int i = 0; i < statement.ElseIfConditions.Count(); i++ ) {
                    IfStatementSyntax innerIfStatement = _cSharpStatementFactory.CreateIfStatement( ConvertGeneric( statement.ElseIfConditions.ElementAt( i ), context ), (BlockSyntax) ConvertGeneric( statement.ElseIfBlocks.ElementAt( i ), context ).Single() );
                    elseIfs.Add( innerIfStatement );
                }
            }

            if ( elseIfs.Count > 1 ) {
                for ( int index = elseIfs.Count-1; index > 0; index-- ) {
                    IfStatementSyntax innerIf = elseIfs[index];
                    elseIfs[index-1] = elseIfs[index-1].WithElse( innerIf.ToElseClause() );
                }
            }

            if ( elseIfs.Any() ) {
                if ( statement.ElseBlock != null ) {
                    elseIfs[0] = elseIfs[0].WithElse( ConvertGeneric( statement.ElseBlock, context ).Single().ToElseClause() );
                }
                ifStatement = ifStatement.WithElse( elseIfs[0].ToElseClause() );
            }
            else {
                if ( statement.ElseBlock != null ) {
                    ifStatement = ifStatement.WithElse( ConvertGeneric( statement.ElseBlock, context ).Single().ToElseClause() );
                }
            }
            toReturn.Add( ifStatement );

            return toReturn;
        }
    }
}
