using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class EmptyStatement : StatementBase
    {
        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override string ToSource() {
            return Environment.NewLine;
        }

        public override object Clone() {
            return new EmptyStatement();
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitEmptyStatement( this );
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitEmptyStatement(this);
        }

        public EmptyStatement Update() {
            return new EmptyStatement();
        }
    }
}