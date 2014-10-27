using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class SelectCaseStatement : StatementBase
    {
        public SelectCaseStatement( Dictionary<IExpressionNode, IEnumerable<IStatementNode>> cases, IEnumerable<IStatementNode> @else ) {
            Cases = cases;
            Else = @else;
            Initialize();
        }

        public Dictionary<IExpressionNode, IEnumerable<IStatementNode>> Cases {
            get;
            private set;
        }
        public IEnumerable<IStatementNode> Else {
            get;
            private set;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode>();
                syntaxNodes.AddRange( Cases.Keys );
                syntaxNodes.AddRange( Cases.Values.SelectMany( x => x ).ToArray() );

                if( Else != null ) {
                    syntaxNodes.AddRange( Else );
                }

                return syntaxNodes;
            }
        }

        public override string ToSource() {
            string toReturn = string.Format( "Select{0}", Environment.NewLine );
            foreach(var @case in Cases) {
                toReturn += "Case "+@case.Key.ToSource()+Environment.NewLine;
                foreach(IStatementNode caseStatement in @case.Value) {
                    toReturn += string.Format( "{0}{1}", caseStatement.ToSource(), Environment.NewLine );
                }
            }

            if( Else != null ) {
                toReturn += string.Format( "Case Else{0}", Environment.NewLine );
                foreach(IStatementNode elseStatement in Else) {
                    toReturn += string.Format( "{0}{1}", elseStatement.ToSource(), Environment.NewLine );
                }
            }

            toReturn += "EndSelect";
            return toReturn;
        }

        public override object Clone() {
            Dictionary<IExpressionNode, IEnumerable<IStatementNode>> cases = Cases.ToDictionary( @case => (IExpressionNode)@case.Key.Clone(), @case => @case.Value.Select( x => (IStatementNode)x.Clone() ) );
            return new SelectCaseStatement( cases, CloneEnumerableAs<IStatementNode>( Else ) );
        }
    }
}
