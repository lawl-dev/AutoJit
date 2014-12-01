using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST
{
    public sealed class Function : SyntaxNodeBase
    {
        public Function( TokenNode name, List<AutoitParameter> autoitParameterInfos, BlockStatement statements ) {
            Name = name;
            Parameter = autoitParameterInfos;
            Statements = statements;
        }

        public TokenNode Name { get; private set; }
        public List<AutoitParameter> Parameter { get; private set; }
        public BlockStatement Statements { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( Name );
                nodes.AddRange( Parameter );
                nodes.Add( Statements );
                return nodes;
            }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitFunction(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitFunction( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "Func {0}({1}) {2}", Name.Token.Value.StringValue, string.Join( ", ", Parameter.Select( x=>x.ToSource() ) ), Environment.NewLine );

            toReturn += Statements.ToSource();

            toReturn += string.Format( "EndFunc{0}", Environment.NewLine );
            return toReturn;
        }

        public override object Clone() {
            var function = new Function( (TokenNode) Name.Clone(), Parameter.Select( x => (AutoitParameter) x.Clone() ).ToList(), (BlockStatement) Statements.Clone() );
            function.Initialize();
            return function;
        }

        public override string ToString() {
            string toReturn = string.Format( "Name: {0}{1}", Name, Environment.NewLine );
            toReturn += string.Format( "Parameter: {0}{1}", String.Join( ", ", Parameter.Select( x => x.ParameterName ) ), Environment.NewLine );
            return toReturn;
        }

        public Function Update( TokenNode name, List<AutoitParameter> parameter, BlockStatement statements ) {
            if ( Name == name &&
                 EnumerableEquals( Parameter ,parameter) &&
                 Statements == statements ) {
                return this;
            }
            var function = new Function( (TokenNode) name.Clone(), parameter.Select( x=>(AutoitParameter)x.Clone() ).ToList(), (BlockStatement) statements.Clone());
            function.Initialize();
            return function;
        }
    }
}
