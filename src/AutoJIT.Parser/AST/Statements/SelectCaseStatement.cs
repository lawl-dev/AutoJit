using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class SelectCaseStatement : StatementBase
    {
        public readonly Dictionary<IExpressionNode, IEnumerable<IStatementNode>> Cases;
        public readonly IEnumerable<IStatementNode> Else;

        public SelectCaseStatement( Dictionary<IExpressionNode, IEnumerable<IStatementNode>> cases, IEnumerable<IStatementNode> @else ) {
            Cases = cases;
            Else = @else;
            Initialize();
        }

        public override string ToSource() {
            var toReturn = string.Format( "Select{0}", Environment.NewLine );
            foreach (var @case in Cases) {
                toReturn += "Case "+@case.Key.ToSource()+Environment.NewLine;
                foreach (var caseStatement in @case.Value) {
                    toReturn += string.Format( "{0}{1}", caseStatement.ToSource(), Environment.NewLine );
                }
            }

            if ( Else != null ) {
                toReturn += string.Format( "Case Else{0}", Environment.NewLine );
                foreach (var elseStatement in Else) {
                    toReturn += string.Format( "{0}{1}", elseStatement.ToSource(), Environment.NewLine );
                }
            }

            toReturn += "EndSelect";
            return toReturn;
        }

        public override object Clone() {
            var cases = Cases.ToDictionary( @case => (IExpressionNode) @case.Key.Clone(), @case => @case.Value.Select( x => (IStatementNode) x.Clone() ) );
            return new SelectCaseStatement( cases, Else.Select( x => (IStatementNode) x.Clone() ) );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get {
                var syntaxNodes = new List<ISyntaxNode>();
                syntaxNodes.AddRange( Cases.Keys );
                syntaxNodes.AddRange( Cases.Values.SelectMany( x => x ).ToArray() );

                if ( Else != null ) {
                    syntaxNodes.AddRange( Else );
                }

                return syntaxNodes;
            }
        }
    }
}
