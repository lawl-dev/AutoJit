using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class SwitchCaseStatement : StatementBase
    {
        public IExpressionNode Condition { get; private set; }
        public Dictionary<IEnumerable<IExpressionNode>, IEnumerable<IStatementNode>> Cases { get; private set; }
        public IEnumerable<IStatementNode> Else { get; private set; }

        public SwitchCaseStatement(
            IExpressionNode condition,
            Dictionary<IEnumerable<IExpressionNode>, IEnumerable<IStatementNode>> cases,
            IEnumerable<IStatementNode> @else ) {
            Condition = condition;
            Cases = cases;
            Else = @else;
            Initialize();
        }

        public override string ToSource() {
            var toReturn = string.Format( "Switch {0}{1}", Condition.ToSource(), Environment.NewLine );
            foreach (KeyValuePair<IEnumerable<IExpressionNode>, IEnumerable<IStatementNode>> @case in Cases) {
                toReturn += "Case "+@case.Key.First().ToSource();
                foreach (var expr in @case.Key.Skip( 1 )) {
                    toReturn += string.Format( " To {0}", expr.ToSource() );
                }
                toReturn += Environment.NewLine;
                foreach (var statmnts in @case.Value) {
                    toReturn += string.Format( "{0}{1}", statmnts.ToSource(), Environment.NewLine );
                }
            }
            if ( Else != null ) {
                toReturn += string.Format( "Case Else{0}", Environment.NewLine );
                foreach (var node in Else) {
                    toReturn += string.Format( "{0}{1}", node.ToSource(), Environment.NewLine );
                }
            }
            return toReturn;
        }

        public override object Clone() {
            var cases = Cases.ToDictionary( @case => @case.Key.Select( x=>(IExpressionNode) x.Clone() ), @case => @case.Value.Select( x => (IStatementNode) x.Clone() ) );
            return new SwitchCaseStatement( (IExpressionNode) Condition.Clone(), cases, Else.Select( x => (IStatementNode) x.Clone() ) );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get {
                var syntaxNodes = new List<ISyntaxNode> { Condition };
                syntaxNodes.AddRange( Cases.Keys.SelectMany( x => x ) );
                syntaxNodes.AddRange( Cases.Values.SelectMany( x => x ) );

                if ( Else != null ) {
                    syntaxNodes.AddRange( Else );
                }

                return syntaxNodes;
            }
        }
    }
}
