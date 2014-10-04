using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class LocalDeclarationStatement : StatementBase
    {
        public readonly bool IsConst;
        public readonly bool IsStatic;
        public readonly VariableExpression VariableExpression;
        public readonly IExpressionNode InitExpression;

        public LocalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst, bool isStatic ) {
            IsConst = isConst;
            IsStatic = isStatic;
            VariableExpression = variableExpression;
            InitExpression = initExpression;
            Initialize();
        }

        public override string ToSource() {
            var toReturn = string.Format( "Local {0}", VariableExpression.ToSource() );
            if ( InitExpression != null ) {
                toReturn += string.Format( " = {0}", InitExpression.ToSource() );
            }
            return toReturn;
        }

        public override object Clone() {
            return new LocalDeclarationStatement( (VariableExpression) VariableExpression.Clone(), CloneAs<IExpressionNode>( InitExpression), IsConst, IsStatic );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get { return new List<ISyntaxNode>() { VariableExpression, InitExpression }; }
        }
    }
}
