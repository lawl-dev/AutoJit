using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST
{
    public sealed class PropertySetter : SyntaxNodeBase
    {
        public BlockStatement StatementBlock { get; private set; }

        public PropertySetter(BlockStatement statementBlock)
        {
            StatementBlock = statementBlock;
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get { return new List<ISyntaxNode>() { StatementBlock }; }
        }

        public override string ToSource() {
            var toReturn = string.Format( "{0}{1}", Keywords.Set, Environment.NewLine );
            toReturn += StatementBlock.ToSource();
            toReturn += string.Format( "{0}{1}", Keywords.EndSet, Environment.NewLine );
            return toReturn;
        }

        public override object Clone() {
            var setter = new PropertySetter((BlockStatement)StatementBlock.Clone());
            setter.Initialize();
            return setter;
        }

        public override void Accept(ISyntaxVisitor visitor)
        {
            visitor.VisitPropertySetter(this);
        }

        public override TResult Accept<TResult>(ISyntaxVisitor<TResult> visitor)
        {
            return visitor.VisitPropertySetter(this);
        }

        public PropertySetter Update(BlockStatement blockStatement)
        {
            if (StatementBlock == blockStatement)
            {
                return this;
            }
            var setter = new PropertySetter((BlockStatement)blockStatement.Clone());
            setter.Initialize();
            return setter;
        }
    }
}