using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class AssignStatement : StatementBase
    {
        public AssignStatement( VariableExpression variable, IExpressionNode expressionToAssign, TokenNode @operator ) {
            Variable = variable;
            ExpressionToAssign = expressionToAssign;
            Operator = @operator;
        }

        public VariableExpression Variable { get; private set; }
        public IExpressionNode ExpressionToAssign { get; private set; }
        public TokenNode Operator { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode> {
                    Variable,
                    Operator,
                    ExpressionToAssign
                };
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitAssignStatement( this );
        }

        public override string ToSource() {
            return string.Format( "{0} {1} {2}", Variable.ToSource(), Operator.ToSource(), ExpressionToAssign.ToSource() );
        }

        public override object Clone() {
            var statement = new AssignStatement( (VariableExpression) Variable.Clone(), (IExpressionNode) ExpressionToAssign.Clone(), (TokenNode) Operator.Clone() );
            statement.Initialize();
            return statement;
        }

        public AssignStatement Update( VariableExpression variable, IExpressionNode expressionToAssign, TokenNode @operator ) {
            if ( variable == Variable &&
                 expressionToAssign == ExpressionToAssign &&
                 Operator == @operator ) {
                return this;
            }
            var statement = new AssignStatement( (VariableExpression) variable.Clone(), (IExpressionNode) expressionToAssign.Clone(), (TokenNode) @operator.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
