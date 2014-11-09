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
    public sealed class ExpressionParser : ExpressionParseBase, IExpressionParser
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

            if ( queue.Any() ) {
                throw new InvalidOperationException( queue.ToString() );
            }

            return res;
        }

        public TExpression ParseSingle<TExpression>( TokenQueue block ) where TExpression : IExpressionNode {
            IExpressionNode expressionNode = ParseExpressionNode( block );
            return (TExpression) expressionNode;
        }

        private IExpressionNode ParseBlock( TokenQueue block ) {
            Token @operator;

            IExpressionNode leftNode = ParseExpressionNode( block );

            if ( !block.Any() ) {
                return leftNode;
            }

            bool isBinaryExpression = block.Peek().IsBinaryExpression;

            bool isTernaryExpression = block.Peek().Type == TokenType.QuestionMark;

            if ( isBinaryExpression ) {
                @operator = block.Dequeue();
            }
            else if ( isTernaryExpression ) {
                return ParseTernaryExpression( block, leftNode );
            }
            else {
                return leftNode;
            }

            IExpressionNode rightNode = ParseExpressionNode( block );

            if ( block.Any() ) {
                throw new SyntaxTreeException( "Unresolved expression", block.First().Col, block.First().Line );
            }

            return new BinaryExpression( leftNode, rightNode, new TokenNode( @operator ) );
        }

        private IExpressionNode ParseTernaryExpression( TokenQueue block, IExpressionNode leftNode ) {
            ConsumeAndEnsure( block, TokenType.QuestionMark );
            IExpressionNode ifTrue = ParseExpressionNode( block );

            ConsumeAndEnsure( block, TokenType.DoubleDot );
            IExpressionNode ifFalse = ParseExpressionNode( block );

            if ( block.Any() ) {
                throw new SyntaxTreeException( "Unresolved expression", block.First().Col, block.First().Line );
            }
            return new TernaryExpression( leftNode, ifTrue, ifFalse );
        }

        private IExpressionNode ParseExpressionNode( TokenQueue block ) {
            IExpressionNode toReturn = null;
            IList<Token> signOperators = null;

            bool hasSignOperators = block.Peek().IsSignOperator;

            if ( hasSignOperators ) {
                signOperators = block.DequeueWhile( x => x.IsSignOperator ).ToList();
            }

            switch (block.Peek().Type) {
                case TokenType.Leftparen:
                    toReturn = ParseBlock( GetInnerExpression( block ) );
                    break;
                case TokenType.String:
                    toReturn = new StringLiteralExpression( new TokenNode( block.Dequeue() ) );
                    break;
                case TokenType.Int32:
                case TokenType.Int64:
                case TokenType.Double:
                    toReturn = new NumericLiteralExpression(
                        new TokenNode( block.Dequeue() ), signOperators == null
                            ? null
                            : signOperators.Select( x => new TokenNode( x ) ) );
                    break;
                case TokenType.Variable:
                    toReturn = ParseVariable( block );
                    break;
                case TokenType.Function:
                    bool isFunctionCall = block.Count > 1
                                          && block.Skip( 1 ).First().Type == TokenType.Leftparen;
                    toReturn = isFunctionCall
                        ? ParseFunctionCallExpression( block )
                        : ParserFunctionExpression( block );
                    break;
                case TokenType.Userfunction:
                    bool isUserFunctionCall = block.Count > 1
                                              && block.Skip( 1 ).First().Type == TokenType.Leftparen;
                    toReturn = isUserFunctionCall
                        ? ParseUserfunctionCallExpression( block )
                        : ParseUserfunctionExpression( block );
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
                    toReturn = new BooleanNegateExpression( ParseExpressionNode( block ), new TokenNode( @operator ) );
                    break;
                case TokenType.Keyword:
                    switch (block.Peek().Value.Keyword) {
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

            if ( !( toReturn is LiteralExpression )
                 &&
                 hasSignOperators
                 &&
                 signOperators.Count( x => x.Type == TokenType.Minus ) % 2 != 0 ) {
                return new NegateExpression( toReturn );
            }
            return toReturn;
        }

        private IExpressionNode ParseVariable( TokenQueue block ) {
            IExpressionNode toReturn = null;
            VariableExpression expression = ParseVariableExpression( block );
            if ( block.Any() &&
                 block.Peek().Type == TokenType.Leftparen ) {
                IEnumerable<IExpressionNode> parameter = GetFunctionParameterExpressionTrees( block );
                toReturn = new VariableFunctionCallExpression( expression, parameter );
            }
            else {
                toReturn = expression;
            }
            return toReturn;
        }

        private IExpressionNode ParseUserfunctionExpression( TokenQueue block ) {
            string userFunctionName = block.Dequeue().Value.StringValue;

            return new UserfunctionExpression( userFunctionName );
        }

        private IExpressionNode ParserFunctionExpression( TokenQueue block ) {
            string buildInFunctionName = block.Dequeue().Value.StringValue;
            return new FunctionExpression( buildInFunctionName );
        }

        private IExpressionNode ParseDefaultKeywordExpression( TokenQueue block ) {
            ConsumeAndEnsure( block, Keywords.Default );
            return new DefaultExpression();
        }

        private IExpressionNode ParseTrueKeywordExpression( TokenQueue block ) {
            ConsumeAndEnsure( block, Keywords.True );
            return new TrueLiteralExpression();
        }

        private IExpressionNode ParseFalseKeywordExpression( TokenQueue block ) {
            ConsumeAndEnsure( block, Keywords.False );
            return new FalseLiteralExpression();
        }

        private IExpressionNode ParseArrayInitializerExpression( TokenQueue block ) {
            ConsumeAndEnsure( block, TokenType.Leftsubscript );
            var toInit = new List<IExpressionNode>();
            do {
                toInit.Add( ParseBlock( block ) );
            } while ( Consume( block, TokenType.Comma ) );
            ConsumeAndEnsure( block, TokenType.Rightsubscript );
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

        private VariableExpression ParseVariableExpression( TokenQueue block ) {
            string identifierName = block.Dequeue().Value.StringValue;
            VariableExpression toReturn;
            if ( block.Any()
                 &&
                 block.Peek().Type == TokenType.Leftsubscript ) {
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

            while ( innerExpressionsBlock.Any() ) {
                if ( innerExpressionsBlock.Peek().Type == TokenType.Comma
                     &&
                     !isInner ) {
                    @params.Add( new TokenCollection() );
                    innerExpressionsBlock.Dequeue();
                }
                else {
                    if ( innerExpressionsBlock.Peek().Type == TokenType.Leftparen ) {
                        iC++;
                    }
                    if ( innerExpressionsBlock.Peek().Type == TokenType.Rightparen ) {
                        iC--;
                    }

                    if ( innerExpressionsBlock.Peek().Type == TokenType.Leftsubscript ) {
                        iC++;
                    }
                    if ( innerExpressionsBlock.Peek().Type == TokenType.Rightsubscript ) {
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
