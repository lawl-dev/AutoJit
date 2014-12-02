using System;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;
using AutoJITRuntime;
using AutoJITRuntime.Variants;

namespace IntegrationTests
{
    public class AutoitOptimizerRewriter : SyntaxRewriterBase
    {
        private readonly IAutoitSyntaxFactory _syntaxFactory = new AutoitSyntaxFactory( new TokenFactory() );
        private readonly ITokenFactory _tokenFactory = new TokenFactory();

        public override ISyntaxNode VisitBinaryExpression( BinaryExpression node ) {
            var binaryExpression = (BinaryExpression)base.VisitBinaryExpression( node );

            if ( !( binaryExpression.Left is LiteralExpression && binaryExpression.Right is LiteralExpression ) ) {
                return binaryExpression;
            }

            var leftVariant = Variant.Create( ((LiteralExpression)binaryExpression.Left).LiteralToken.Token.Value.CurrentValue );
            var rightVariant = Variant.Create( ((LiteralExpression)binaryExpression.Right).LiteralToken.Token.Value.CurrentValue );

            Variant result = GetBinaryResult( leftVariant, rightVariant, binaryExpression.Operator );

            ISyntaxNode toReturn = null;

            switch (result.DataType) {
                case DataType.Bool:
                    toReturn = result
                        ? (ISyntaxNode) _syntaxFactory.CreateTrueLiteralExpression()
                        : _syntaxFactory.CreateFalseLiteralExpression();
                    break;
                case DataType.Double:
                    toReturn = _syntaxFactory.CreateNumericLiteralExpression( _syntaxFactory.CreateTokenNode( _tokenFactory.CreateDouble( result.GetDouble(), 0, 0 ) ), Constants.Array<TokenNode>.Empty.ToList() );
                    break;
                case DataType.Int32:
                    toReturn = _syntaxFactory.CreateNumericLiteralExpression( _syntaxFactory.CreateTokenNode( _tokenFactory.CreateInt( result.GetInt(), 0, 0 ) ), Constants.Array<TokenNode>.Empty.ToList() );
                    break;
                case DataType.Int64:
                    toReturn = _syntaxFactory.CreateNumericLiteralExpression( _syntaxFactory.CreateTokenNode( _tokenFactory.CreateInt64( result.GetInt64(), 0, 0 ) ), Constants.Array<TokenNode>.Empty.ToList() );
                    break;
                case DataType.String:
                    toReturn = _syntaxFactory.CreateStringLiteralExpression( result.GetString() );
                    break;
                default:
                    throw new NotImplementedException();
            }
            return toReturn;
        }

        private Variant GetBinaryResult( Variant leftVariant, Variant rightVariant, TokenNode @operator ) {
            switch (@operator.Token.Type) {
                case TokenType.Plus:
                    return leftVariant+rightVariant;
                case TokenType.Minus:
                    return leftVariant-rightVariant;
                case TokenType.Mult:
                    return leftVariant * rightVariant;
                case TokenType.Div:
                    return leftVariant / rightVariant;
                case TokenType.Pow:
                    return Math.Pow( leftVariant, rightVariant );
                case TokenType.Equal:
                    return leftVariant == rightVariant;
                case TokenType.Greater:
                    return leftVariant > rightVariant;
                case TokenType.GreaterEqual:
                    return leftVariant >= rightVariant;
                case TokenType.Less:
                    return leftVariant < rightVariant;
                case TokenType.LessEqual:
                    return leftVariant <= rightVariant;
                case TokenType.And:
                    return leftVariant && rightVariant;
                case TokenType.Notequal:
                    return leftVariant != rightVariant;
                case TokenType.Or:
                    return leftVariant || rightVariant;
                case TokenType.StringEqual:
                    return leftVariant.GetString() == rightVariant.GetString();
            }
            throw new NotImplementedException();
        }
    }
}