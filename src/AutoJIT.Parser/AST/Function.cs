using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST
{
    public sealed class Function : SyntaxNodeBase
    {
        public readonly TokenQueue Queue;
        public IList<IStatementNode> Statements = new List<IStatementNode>();

        public Function( string name, IEnumerable<AutoitParameterInfo> autoitParameterInfos ) {
            Name = name;
            Parameter = autoitParameterInfos;
            Queue = new TokenQueue( new Token[0] );
        }

        public string Name { get; private set; }
        public IEnumerable<AutoitParameterInfo> Parameter { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return Statements; }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitFunction( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "Func {0}({1}) {2}", Name, string.Join( ", ", Parameter ), Environment.NewLine );

            foreach (IStatementNode statement in Statements) {
                toReturn += string.Format( "{0}{1}", statement.ToSource(), Environment.NewLine );
            }

            toReturn += string.Format( "EndFunc{0}", Environment.NewLine );
            return toReturn;
        }

        public override object Clone() {
            return new Function( (string) Name.Clone(), Parameter ) {
                Statements = Statements.Select( x => (IStatementNode) x.Clone() ).ToList()
            };
        }

        public override string ToString() {
            string toReturn = string.Format( "Name: {0}{1}", Name, Environment.NewLine );
            toReturn += string.Format( "Parameter: {0}{1}", String.Join( ", ", Parameter.Select( x => x.ParameterName ) ), Environment.NewLine );
            return toReturn;
        }

        public Function Update( string name, IEnumerable<AutoitParameterInfo> parameter, IEnumerable<IStatementNode> statements ) {
            if ( Name == name &&
                 Parameter == parameter &&
                 Statements == statements ) {
                return this;
            }
            return new Function( name, Parameter ) { Statements = statements.ToList() };
        }
    }
}
