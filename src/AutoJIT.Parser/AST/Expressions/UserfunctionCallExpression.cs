using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class UserfunctionCallExpression : CallExpression
    {
        public UserfunctionCallExpression( TokenNode identifierName, List<IExpressionNode> parameter ) : base( identifierName, parameter ) {}

        public override object Clone() {
            var expression = new UserfunctionCallExpression( (TokenNode) IdentifierName.Clone(), CloneEnumerableAs<IExpressionNode>( Parameter ).ToList() );
            expression.Initialize();
            return expression;
        }

        public UserfunctionCallExpression Update( IEnumerable<IExpressionNode> parameter, TokenNode identifierName ) {
            if ( IdentifierName == identifierName &&
                 EnumerableEquals(Parameter, parameter) ) {
                return this;
            }
            var expression = new UserfunctionCallExpression( (TokenNode) identifierName.Clone(), parameter.Select( x=>(IExpressionNode)x.Clone() ).ToList() );
            expression.Initialize();
            return expression;
        }
    }
}
