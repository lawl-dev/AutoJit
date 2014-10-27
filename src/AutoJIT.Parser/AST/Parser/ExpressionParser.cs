using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser
{
    public class ExpressionParser : ExpressionParseBase, IExpressionParser
    {
        private readonly IOperatorPrecedenceService _operatorPrecedenceService;

        public ExpressionParser( IOperatorPrecedenceService operatorPrecedenceService ) {
            _operatorPrecedenceService = operatorPrecedenceService;
        }

        public IExpressionNode ParseBlock( TokenCollection block, bool prepareExpression ) {
            TokenQueue queue = prepareExpression
            ? new TokenQueue( _operatorPrecedenceService.PrepareOperatorPrecedence( block ) )
            : new TokenQueue( block );

            IExpressionNode res = ParseBlock( queue );

            if( queue.Any() ) {
                throw new InvalidOperationException( queue.ToString() );
            }

            return res;
        }

        public TExpression ParseSingle<TExpression>( TokenQueue block ) where TExpression : IExpressionNode {
            IExpressionNode expressionNode = ParseExpressionNode( block );
            return (TExpression)expressionNode;
        }

        private IExpressionNode ParseBlock( TokenQueue block ) {
            if( block == null
                || !block.Any() ) {
                return null;
            }

            Token @operator;

            IExpressionNode leftNode = ParseExpressionNode( block );

            if( !block.Any() ) {
                return leftNode;
            }

            bool isOperatableExpression = block.Peek().IsMathExpression || block.Peek().IsNumberExpression || block.Peek().IsBooleanExpression || block.Peek().Type == TokenType.StringEqual || block.Peek().Type == TokenType.Concat;

            bool isTernaryExpression = block.Peek().Type == TokenType.QuestionMark;

            if( isOperatableExpression ) {
                @operator = block.Dequeue();
            }
            else if( isTernaryExpression ) {
                return ParseTernaryExpression( block, leftNode );
            }
            else {
                return leftNode;
            }

            IExpressionNode rightNode = ParseExpressionNode( block );

            if( block.Any() ) {
                throw new SyntaxTreeException( "Unresolved expression", block.First().Col, block.First().Line );
            }

            switch(@operator.Type) {
                case TokenType.Plus:
                case TokenType.Minus:
                case TokenType.Div:
                case TokenType.Mult:
                case TokenType.Concat:
                case TokenType.PowAssign:
                case TokenType.Greater:
                case TokenType.GreaterEqual:
                case TokenType.Less:
                case TokenType.LessEqual:
                case TokenType.StringEqual:
                case TokenType.Equal:
                case TokenType.Notequal:
                case TokenType.Pow:
                case TokenType.OR:
                case TokenType.AND:
                    return new BinaryExpression( leftNode, rightNode, @operator );
            }
            throw new NotImplementedException();
        }

        private IExpressionNode ParseTernaryExpression( TokenQueue block, IExpressionNode leftNode ) {
            SkipAndAssert( block, TokenType.QuestionMark );
            IExpressionNode ifTrue = ParseExpressionNode( block );
            SkipAndAssert( block, TokenType.DoubleDot );
            IExpressionNode ifFalse = ParseExpressionNode( block );
            if( block.Any() ) {
                throw new SyntaxTreeException( "Unresolved expression", block.First().Col, block.First().Line );
            }
            return new TernaryExpression( leftNode, ifTrue, ifFalse );
        }

        private IExpressionNode ParseExpressionNode( TokenQueue block ) {
            IExpressionNode toReturn = null;
            IList<Token> signOperators = null;

            bool hasSignOperators = block.Peek().IsSignOperator;

            if( hasSignOperators ) {
                signOperators = block.DequeueWhile( x => x.IsSignOperator ).ToList();
            }

            switch(block.Peek().Type) {
                case TokenType.Leftparen:
                    toReturn = ParseBlock( GetInnerExpression( block ) );
                    break;
                case TokenType.String:
                    toReturn = new StringLiteralExpression( block.Dequeue() );
                    break;
                case TokenType.Int32:
                case TokenType.Int64:
                case TokenType.Double:
                    toReturn = new NumericLiteralExpression( block.Dequeue(), signOperators );
                    break;
                case TokenType.Variable:
                    toReturn = ParseVariableExpression( block );
                    break;
                case TokenType.Function:
                    toReturn = ParseFunctionCallExpression( block );
                    break;
                case TokenType.Userfunction:
                    toReturn = ParseUserfunctionCallExpression( block );
                    break;
                case TokenType.Macro:
                    toReturn = ParseMacro( block );
                    break;
                case TokenType.Null:
                    toReturn = ParseNull( block );
                    break;
                case TokenType.Leftsubscript:
                    toReturn = ParseArrayInitializerExpression( block );
                    break;
                case TokenType.NOT:
                    Token @operator = block.Dequeue();
                    toReturn = new BooleanNegateExpression( ParseExpressionNode( block ), @operator );
                    break;
                case TokenType.Keyword:
                    switch(block.Peek().Value.Keyword) {
                        case Keywords.True:
                            toReturn = ParseTrueKeywordExpression( block );
                            break;
                        case Keywords.False:
                            toReturn = ParseFalseKeywordExpression( block );
                            break;
                        case Keywords.Default:
                            toReturn = ParseDefaultKeywordExpression( block );
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    break;
            }

            if( !( toReturn is LiteralExpression )
                && hasSignOperators
                && signOperators.Count( x => x.Type == TokenType.Minus ) % 2 != 0 ) {
                return new NegateExpression( toReturn );
            }
            return toReturn;
        }

        private IExpressionNode ParseDefaultKeywordExpression( TokenQueue block ) {
            SkipAndAssert( block, Keywords.Default );
            return new DefaultExpression();
        }

        private IExpressionNode ParseTrueKeywordExpression( TokenQueue block ) {
            SkipAndAssert( block, Keywords.True );
            return new TrueLiteralExpression();
        }

        private IExpressionNode ParseFalseKeywordExpression( TokenQueue block ) {
            SkipAndAssert( block, Keywords.False );
            return new FalseLiteralExpression();
        }

        private IExpressionNode ParseArrayInitializerExpression( TokenQueue block ) {
            SkipAndAssert( block, TokenType.Leftsubscript );
            var toInit = new List<IExpressionNode>();
            do {
                toInit.Add( ParseBlock( block ) );
            } while( Skip( block, TokenType.Comma ) );
            SkipAndAssert( block, TokenType.Rightsubscript );
            return new ArrayInitExpression( toInit );
        }

        private IExpressionNode ParseUserfunctionCallExpression( TokenQueue block ) {
            string userfunctionName = block.Dequeue().Value.StringValue;
            List<IExpressionNode> parameter = GetFunctionParameterExpressionTrees( block ).ToList();
            var toReturn = new UserfunctionCallExpression( userfunctionName, parameter );
            return toReturn;
        }

        private IExpressionNode ParseFunctionCallExpression( TokenQueue block ) {
            string buildInFunctionName = block.Dequeue().Value.StringValue;
            List<IExpressionNode> parameter = GetFunctionParameterExpressionTrees( block ).ToList();
            var toReturn = new CallExpression( buildInFunctionName, parameter );
            return toReturn;
        }

        private IExpressionNode ParseMacro( TokenQueue block ) {
            string name = block.Dequeue().Value.StringValue;
            return new MacroExpression( name );
        }

        private IExpressionNode ParseNull( TokenQueue block ) {
            block.Dequeue();
            return new NullExpression();
        }

        private IExpressionNode ParseVariableExpression( TokenQueue block ) {
            string identifierName = block.Dequeue().Value.StringValue;
            IExpressionNode toReturn;
            if( block.Any()
                && block.Peek().Type == TokenType.Leftsubscript ) {
                IEnumerable<TokenCollection> arrayIndexExpressionTrees = GetArrayIndexExpressionTrees( block );
                List<IExpressionNode> leftParameter = arrayIndexExpressionTrees.Select( x => ParseBlock( x, true ) ).ToList();
                toReturn = new ArrayExpression( identifierName, leftParameter );
            }
            else {
                toReturn = new VariableExpression( identifierName );
            }
            return toReturn;
        }

        private IEnumerable<IExpressionNode> GetFunctionParameterExpressionTrees( TokenQueue block ) {
            var innerExpressionsBlock = new TokenQueue( ParseInner( block, TokenType.Leftparen, TokenType.Rightparen ) );

            var @params = new List<TokenCollection>();
            @params.Add( new TokenCollection() );

            bool isInner = false;
            int iC = 0;

            while( innerExpressionsBlock.Any() ) {
                if( innerExpressionsBlock.Peek().Type == TokenType.Comma
                    && !isInner ) {
                    @params.Add( new TokenCollection() );
                    innerExpressionsBlock.Dequeue();
                }
                else {
                    if( innerExpressionsBlock.Peek().Type == TokenType.Leftparen ) {
                        iC++;
                    }
                    if( innerExpressionsBlock.Peek().Type == TokenType.Rightparen ) {
                        iC--;
                    }

                    if( innerExpressionsBlock.Peek().Type == TokenType.Leftsubscript ) {
                        iC++;
                    }
                    if( innerExpressionsBlock.Peek().Type == TokenType.Rightsubscript ) {
                        iC--;
                    }

                    isInner = iC > 0;
                    @params.Last().Add( innerExpressionsBlock.Dequeue() );
                }
            }
            return @params.Where( x => x.Any() ).Select( x => ParseBlock( x, true ) );
        }
    }
}
