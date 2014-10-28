using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class LocalEnumDeclarationStatement : EnumDeclarationStatement
    {
        public LocalEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode userInitExpression, IExpressionNode autoInitExpression )
        : base( variableExpression, userInitExpression, autoInitExpression ) {}

        public override object Clone() {
            return new LocalEnumDeclarationStatement(
            (VariableExpression)VariableExpression.Clone(),
            (IExpressionNode)UserInitExpression.Clone(),
            (IExpressionNode)AutoInitExpression.Clone() );
        }
    }
}
