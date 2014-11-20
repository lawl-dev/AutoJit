using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST
{
    public sealed class Function : SyntaxNodeBase
    {
        public Function( TokenNode name, List<AutoitParameter> autoitParameterInfos, List<IStatementNode> statements ) {
            Name = name;
            Parameter = autoitParameterInfos;
            Statements = statements;
        }

        public TokenNode Name { get; private set; }
        public List<AutoitParameter> Parameter { get; private set; }
        public List<IStatementNode> Statements { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( Name );
                nodes.AddRange( Parameter );
                nodes.AddRange( Statements );
                return nodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitFunction( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "Func {0}({1}) {2}", Name.Token.Value.StringValue, string.Join( ", ", Parameter.Select( x=>x.ToSource() ) ), Environment.NewLine );

            foreach (IStatementNode statement in Statements) {
                toReturn += string.Format( "{0}{1}", statement.ToSource(), Environment.NewLine );
            }

            toReturn += string.Format( "EndFunc{0}", Environment.NewLine );
            return toReturn;
        }

        public override object Clone() {
            return new Function( (TokenNode) Name.Clone(), Parameter.Select( x => (AutoitParameter) x.Clone() ).ToList(), Statements.Select( x => (IStatementNode) x.Clone() ).ToList() );
        }

        public override string ToString() {
            string toReturn = string.Format( "Name: {0}{1}", Name, Environment.NewLine );
            toReturn += string.Format( "Parameter: {0}{1}", String.Join( ", ", Parameter.Select( x => x.ParameterName ) ), Environment.NewLine );
            return toReturn;
        }

        public Function Update( TokenNode name, List<AutoitParameter> parameter, List<IStatementNode> statements ) {
            if ( Name == name &&
                 EnumerableEquals( Parameter ,parameter) &&
                 EnumerableEquals(Statements, statements) ) {
                return this;
            }
            return new Function( name, Parameter, statements);
        }
    }
}
