using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class AssignStatement : StatementBase
    {
        public AssignStatement( VariableExpression variable, IExpressionNode expressionToAssign, Token @operator ) {
            Variable = variable;
            ExpressionToAssign = expressionToAssign;
            Operator = @operator;
            Initialize();
        }

        public VariableExpression Variable { get; private set; }
        public IExpressionNode ExpressionToAssign { get; private set; }
        public Token Operator { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode> { Variable, ExpressionToAssign }; }
        }

        public override string ToSource() {
            return string.Format( "{0}{1}{2}", Variable.ToSource(), ExpressionToAssign, Operator );
        }

        public override object Clone() {
            return new AssignStatement( (VariableExpression) Variable.Clone(), (IExpressionNode) ExpressionToAssign.Clone(), Operator );
        }
    }
}
