using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class SwitchCase : StatementBase
    {
        public SwitchCase( List<CaseCondition> conditions, BlockStatement block ) {
            Conditions = conditions;
            Block = block;
        }

        public List<CaseCondition> Conditions { get; private set; }
        public BlockStatement Block { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();

                nodes.AddRange( Conditions );
                nodes.Add( Block );

                return nodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitSwitchCase( this );
        }

        public override string ToSource() {
            string toReturn = "	Case ";
            toReturn += string.Format( "{0}{1}", string.Join( ", ", Conditions.Select( x => x.ToSource() ) ), Environment.NewLine );
            toReturn += "	" + Block.ToSource();
            return toReturn;
        }

        public override object Clone() {
            var switchCase = new SwitchCase( Conditions.Select( x => (CaseCondition) x.Clone() ).ToList(), (BlockStatement) Block.Clone() );
            switchCase.Initialize();
            return switchCase;
        }

        public SwitchCase Update( IEnumerable<CaseCondition> conditions, BlockStatement block ) {
            if ( EnumerableEquals( Conditions, conditions) &&
                 Block == block ) {
                return this;
            }
            var switchCase = new SwitchCase( conditions.Select( x=>(CaseCondition)x.Clone() ).ToList(), (BlockStatement) block.Clone() );
            switchCase.Initialize();
            return switchCase;
        }
    }
}
