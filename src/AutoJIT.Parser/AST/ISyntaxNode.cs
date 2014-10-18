using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST
{
    public interface ISyntaxNode : ICloneable
    {
        string ToSource();
        void AcceptSingle( ISyntaxVisitor visitor );
        IEnumerable<ISyntaxNode> Children { get; }
        ISyntaxNode Parent { get; set; }
        void Accept( ISyntaxVisitor visitor );
        void Accept( ISyntaxVisitor<ISyntaxNode, ISyntaxNode> visitor );
    }
}
