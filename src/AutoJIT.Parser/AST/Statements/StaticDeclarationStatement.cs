using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class StaticDeclarationStatement : LocalDeclarationStatement
    {
        public StaticDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression ) : base( variableExpression, initExpression, false ) {}

        public override object Clone() {
            var statement = new StaticDeclarationStatement( (VariableExpression) VariableExpression.Clone(), CloneAs<IExpressionNode>( InitExpression ) );
            statement.Initialize();
            return statement;
        }

        public override string ToSource() {
            string toReturn = string.Format( "Static {0}", VariableExpression.ToSource() );
            if ( InitExpression != null ) {
                toReturn += string.Format( " = {0}", InitExpression.ToSource() );
            }
            return toReturn;
        }

        public new StaticDeclarationStatement Update( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            if ( VariableExpression == variableExpression &&
                 InitExpression == initExpression ) {
                return this;
            }

            var statement = new StaticDeclarationStatement( (VariableExpression) variableExpression.Clone(), (IExpressionNode) initExpression.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
