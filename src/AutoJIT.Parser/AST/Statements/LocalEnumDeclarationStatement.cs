using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public class LocalEnumDeclarationStatement : EnumDeclarationStatement
    {
        public LocalEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode userInitExpression, IExpressionNode autoInitExpression ) : base( variableExpression, userInitExpression, autoInitExpression ) {}
    }
}