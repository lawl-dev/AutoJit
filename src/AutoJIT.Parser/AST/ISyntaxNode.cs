using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST
{
    public interface ISyntaxNode : ICloneable
    {
        IEnumerable<ISyntaxNode> Children { get; }
        ISyntaxNode Parent { get; set; }
        string ToSource();
        void AcceptSingle( ISyntaxVisitor visitor );
        void Accept( ISyntaxVisitor visitor );
        TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor );
    }
}
