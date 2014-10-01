using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class UserfunctionCallExpression : CallExpression
    {
        public UserfunctionCallExpression( string identifierName, IEnumerable<IExpressionNode> parameter ) : base( identifierName, parameter ) {}
    }
}
