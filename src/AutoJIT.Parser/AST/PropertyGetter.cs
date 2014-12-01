using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST
{
    public sealed class PropertyGetter : SyntaxNodeBase
    {
        public BlockStatement StatementBlock { get; private set; }

        public PropertyGetter(BlockStatement statementBlock) {
            StatementBlock = statementBlock;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode>() { StatementBlock }; }
        }

        public override string ToSource() {
            var toReturn = string.Format("{0}{1}", Keywords.Get, Environment.NewLine);
            toReturn += StatementBlock.ToSource();
            toReturn += string.Format("{0}{1}", Keywords.EndGet, Environment.NewLine);
            return toReturn;
        }

        public override object Clone() {
            var getter = new PropertyGetter( (BlockStatement) StatementBlock.Clone() );
            getter.Initialize();
            return getter;
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitPropertyGetter( this );
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitPropertyGetter(this);
        }

        public PropertyGetter Update( BlockStatement blockStatement ) {
            if ( StatementBlock == blockStatement ) {
                return this;
            }
            var getter = new PropertyGetter( (BlockStatement) blockStatement.Clone() );
            getter.Initialize();
            return getter;
        }
    }
}