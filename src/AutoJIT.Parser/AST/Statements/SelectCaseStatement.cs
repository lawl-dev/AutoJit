using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class SelectCaseStatement : StatementBase
    {
        public SelectCaseStatement( IEnumerable<SelectCase> cases, BlockStatement @else ) {
            Cases = cases;
            Else = @else;
            Initialize();
        }

        public IEnumerable<SelectCase> Cases { get; private set; }
        public BlockStatement Else { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode>();
                syntaxNodes.AddRange( Cases );

                if ( Else != null ) {
                    syntaxNodes.Add( Else );
                }

                return syntaxNodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitSelectCaseStatement( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "Select{0}", Environment.NewLine );
            foreach (SelectCase @case in Cases) {
                toReturn += @case.ToSource();
            }

            if ( Else != null ) {
                toReturn += string.Format("	Case Else{0}", Environment.NewLine);
                toReturn += "	" + Else.ToSource();
            }

            toReturn += "EndSelect";
            return toReturn;
        }

        public override object Clone() {
            return new SelectCaseStatement( Cases.Select( x => (SelectCase) x.Clone() ).ToList(), (BlockStatement) Else.Clone() );
        }

        public SelectCaseStatement Update( IEnumerable<SelectCase> selectCases, BlockStatement @else ) {
            if ( EnumerableEquals( Cases, selectCases)&&
                 Else == @else ) {
                return this;
            }
            return new SelectCaseStatement( selectCases.Select( x=>(SelectCase)x.Clone() ), (BlockStatement) @else.Clone() );
        }
    }
}
