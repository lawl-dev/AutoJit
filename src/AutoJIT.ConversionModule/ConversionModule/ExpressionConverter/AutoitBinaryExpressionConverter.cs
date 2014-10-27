using System;
using System.Collections.Generic;
using AutoJIT.Parser;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitBinaryExpressionConverter : AutoitInvocationExpressionConverterBase<BinaryExpressionBase>
    {
        public AutoitBinaryExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( BinaryExpressionBase node, IContextService context ) {
            if( node.NeedsCompilerFunctionCall ) {
                return CreateCompilerFunctionCall( node, context );
            }

            SyntaxKind syntaxKind = GetSyntaxKind( node.Operator );
            ExpressionSyntax leftExpressionSyntax = ConverGeneric( node.Left, context );
            ExpressionSyntax rightExpressionSyntax = ConverGeneric( node.Right, context );

            return SyntaxFactory.BinaryExpression( syntaxKind, leftExpressionSyntax, rightExpressionSyntax );
        }

        private ExpressionSyntax CreateCompilerFunctionCall( BinaryExpressionBase node, IContextService context ) {
            string operatorFunctionName = node.GetCompilerFunctionName( node.Operator.Type );

            ExpressionSyntax leftExpressionSyntax = ConverGeneric( node.Left, context );
            ExpressionSyntax rightExpressionSyntax = ConverGeneric( node.Right, context );

            IEnumerable<ArgumentSyntax> arguments = Utils.GetEnumerable( SyntaxFactory.Argument( leftExpressionSyntax ), SyntaxFactory.Argument( rightExpressionSyntax ) );

            return CreateInvocationExpression( context.GetRuntimeInstanceName(), operatorFunctionName, arguments );
        }

        private SyntaxKind GetSyntaxKind( Token @operator ) {
            switch(@operator.Type) {
                case TokenType.Greater:
                    return SyntaxKind.GreaterThanExpression;
                case TokenType.GreaterEqual:
                    return SyntaxKind.GreaterThanOrEqualExpression;
                case TokenType.Equal:
                    return SyntaxKind.EqualsExpression;
                case TokenType.Less:
                    return SyntaxKind.LessThanExpression;
                case TokenType.LessEqual:
                    return SyntaxKind.LessThanOrEqualExpression;
                case TokenType.Notequal:
                    return SyntaxKind.NotEqualsExpression;
                case TokenType.Plus:
                    return SyntaxKind.AddExpression;
                case TokenType.Minus:
                    return SyntaxKind.SubtractExpression;
                case TokenType.Mult:
                    return SyntaxKind.MultiplyExpression;
                case TokenType.Div:
                    return SyntaxKind.DivideExpression;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
