using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.Parser.AST
{
    public sealed class FunctionNode : SyntaxNodeBase
    {
        public string Name { get; private set; }
        public IEnumerable<AutoitParameterInfo> Parameter { get; private set; }
        public IList<IStatementNode> Statements = new List<IStatementNode>();

        public readonly TokenQueue Queue;


        public FunctionNode( string name, IEnumerable<AutoitParameterInfo> autoitParameterInfos ) {
            Name = name;
            Parameter = autoitParameterInfos;
            Queue = new TokenQueue( new Token[0] );
        }

        public MemberDeclarationSyntax Accept( IFunctionVisitor<MemberDeclarationSyntax> visitor ) {
            return visitor.Visit( this );
        }

        public override string ToSource() {
            var toReturn = string.Format( "Func {0}({1})", Name, string.Join( ", ", Parameter ) );
            foreach (var statement in Statements) {
                toReturn += string.Format( "{0}{1}", statement.ToSource(), Environment.NewLine );
            }
            toReturn += "EndFunc";
            return toReturn;
        }

        public override object Clone() {
            return new FunctionNode( (string) Name.Clone(), Parameter );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get { return Statements; }
        }

        
        public override string ToString() {
            string toReturn = string.Format( "Name: {0}{1}", this.Name, Environment.NewLine );
            toReturn += string.Format( "Parameter: {0}{1}", String.Join( ", ", Parameter.Select( x => x.ParameterName ) ), Environment.NewLine );
            return toReturn;
        }
    }
}
