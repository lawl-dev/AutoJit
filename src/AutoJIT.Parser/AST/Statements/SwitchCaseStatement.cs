using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class SwitchCaseStatement : StatementBase
    {
        public SwitchCaseStatement(
            IExpressionNode condition,
            Dictionary<IEnumerable<KeyValuePair<IExpressionNode, IExpressionNode>>, IEnumerable<IStatementNode>> cases,
            IEnumerable<IStatementNode> @else ) {
            Condition = condition;
            Cases = cases;
            Else = @else;
            Initialize();
        }

        public IExpressionNode Condition { get; private set; }
        public Dictionary<IEnumerable<KeyValuePair<IExpressionNode, IExpressionNode>>, IEnumerable<IStatementNode>> Cases { get; private set; }
        public IEnumerable<IStatementNode> Else { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode> { Condition };
                syntaxNodes.AddRange( Cases.Keys.SelectMany( x => x.Select( y=>y.Key ) ) );
                syntaxNodes.AddRange( Cases.Keys.SelectMany( x => x.Select( y => y.Value ).Where( e => e != null ) ) );
                syntaxNodes.AddRange( Cases.Values.SelectMany( x => x ) );

                if ( Else != null ) {
                    syntaxNodes.AddRange( Else );
                }

                return syntaxNodes;
            }
        }

        public override string ToSource() {
            string toReturn = string.Format( "Switch {0}{1}", Condition.ToSource(), Environment.NewLine );
            foreach (var @case in Cases) {
                foreach (var pair in  @case.Key) {
                    if ( pair.Value != null ) {
                        toReturn += string.Format( ", {0} To {1}", pair.Key.ToSource(), pair.Value.ToSource() );
                    }
                    else {
                        toReturn += string.Format( ", {0}", pair.Key.ToSource() );
                    }
                }
                foreach (IStatementNode statmnts in @case.Value) {
                    toReturn += string.Format( "{0}{1}", statmnts.ToSource(), Environment.NewLine );
                }
            }
            if ( Else != null ) {
                toReturn += string.Format( "Case Else{0}", Environment.NewLine );
                foreach (IStatementNode node in Else) {
                    toReturn += string.Format( "{0}{1}", node.ToSource(), Environment.NewLine );
                }
            }
            return toReturn;
        }

        public override object Clone() {
            var cases = Cases.ToDictionary(
                @case => @case.Key.Select(
                    x => new KeyValuePair<IExpressionNode, IExpressionNode>(
                        (IExpressionNode) x.Key.Clone(), x.Value != null
                            ? (IExpressionNode) x.Value.Clone()
                            : null ) ), @case => @case.Value.Select( x => (IStatementNode) x.Clone() ) );
            return new SwitchCaseStatement( (IExpressionNode) Condition.Clone(), cases, CloneEnumerableAs<IStatementNode>( Else ) );
        }
    }
}
