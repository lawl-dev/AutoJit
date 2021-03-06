using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class VariableFunctionCallStatement : StatementBase
    {
        public VariableFunctionCallStatement( VariableFunctionCallExpression variableFunctionCallExpression ) {
            VariableFunctionCallExpression = variableFunctionCallExpression;
        }

        public VariableFunctionCallExpression VariableFunctionCallExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( VariableFunctionCallExpression );
                return nodes;
            }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitVariableFunctionCallStatement(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitVariableFunctionCallStatement( this );
        }

        public override string ToSource() {
            return VariableFunctionCallExpression.ToSource();
        }

        public override object Clone() {
            var statement = new VariableFunctionCallStatement( (VariableFunctionCallExpression) VariableFunctionCallExpression.Clone() );
            statement.Initialize();
            return statement;
        }

        public VariableFunctionCallStatement Update( VariableFunctionCallExpression variableFunctionCallExpression ) {
            if ( VariableFunctionCallExpression == variableFunctionCallExpression ) {
                return this;
            }
            var statement = new VariableFunctionCallStatement( (VariableFunctionCallExpression) variableFunctionCallExpression.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
