using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class GlobalEnumDeclarationStatement : EnumDeclarationStatement
    {
        public GlobalEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode userInitExpression, IExpressionNode autoInitExpression ) : base( variableExpression, userInitExpression, autoInitExpression ) {}
    }
}