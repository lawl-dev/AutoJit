using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class GlobalDeclarationStatement : StatementBase
    {
        public readonly bool IsConst;
        public readonly VariableExpression VariableExpression;
        public readonly IExpressionNode InitExpression;

        public GlobalDeclarationStatement(VariableExpression variableExpression, IExpressionNode initExpression, bool isConst)
        {
            IsConst = isConst;
            VariableExpression = variableExpression;
            InitExpression = initExpression;
            Initialize();
        }

        public override string ToSource()
        {
            var toReturn = string.Format("Global {0}", VariableExpression.ToSource());
            if (InitExpression != null)
            {
                toReturn += string.Format(" = {0}", InitExpression.ToSource());
            }
            return toReturn;
        }

        public override object Clone()
        {
            return new GlobalDeclarationStatement((VariableExpression)VariableExpression.Clone(), CloneAs<IExpressionNode>(InitExpression), IsConst);
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get { return new List<ISyntaxNode>() { VariableExpression, InitExpression }; }
        }
    }
}
