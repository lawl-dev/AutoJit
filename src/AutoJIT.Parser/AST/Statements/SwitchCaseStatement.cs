using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class SwitchCaseStatement : StatementBase
    {
        public SwitchCaseStatement( IExpressionNode condition, IEnumerable<SwitchCase> cases, BlockStatement @else ) {
            Condition = condition;
            Cases = cases;
            Else = @else;
        }

        public IExpressionNode Condition { get; private set; }
        public IEnumerable<SwitchCase> Cases { get; private set; }
        public BlockStatement Else { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode> {
                    Condition
                };
                syntaxNodes.AddRange( Cases );
                if ( Else != null ) {
                    syntaxNodes.Add( Else );
                }

                return syntaxNodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitSwitchCaseStatement( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "Switch {0}{1}", Condition.ToSource(), Environment.NewLine );
            foreach (SwitchCase @case in Cases) {
                toReturn += @case.ToSource();
            }
            if ( Else != null ) {
                toReturn += string.Format("	Case Else{0}", Environment.NewLine);
                toReturn += "	" + Else.ToSource();
            }
            toReturn += "EndSwitch";
            return toReturn;
        }

        public override object Clone() {
            var statement = new SwitchCaseStatement( (IExpressionNode) Condition.Clone(), Cases.Select( x => (SwitchCase) x.Clone() ).ToList(), (BlockStatement) Else.Clone() );
            statement.Initialize();
            return statement;
        }

        public SwitchCaseStatement Update( IExpressionNode condition, IEnumerable<SwitchCase> cases, BlockStatement @else ) {
            if ( Condition == condition &&
                 EnumerableEquals( Cases,cases) &&
                 Else == @else ) {
                return this;
            }
            var statement = new SwitchCaseStatement( (IExpressionNode) condition.Clone(), cases.Select( x=>(SwitchCase)x.Clone() ), (BlockStatement) @else.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
