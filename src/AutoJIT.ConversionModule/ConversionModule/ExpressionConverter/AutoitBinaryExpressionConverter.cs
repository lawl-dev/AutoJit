﻿using System;
using System.Collections.Generic;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Lex;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitBinaryExpressionConverter : AutoitInvocationExpressionConverterBase<BinaryExpression>
    {
        public AutoitBinaryExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( BinaryExpression node, IContextService contextService ) {
            if ( NeedsCompilerFunctionCall( node.Operator.Token ) ) {
                return CreateCompilerFunctionCall( node, contextService );
            }

            SyntaxKind syntaxKind = GetSyntaxKind( node.Operator.Token );
            ExpressionSyntax leftExpressionSyntax = ConvertGeneric( node.Left, contextService );
            ExpressionSyntax rightExpressionSyntax = ConvertGeneric( node.Right, contextService );

            return SyntaxFactory.BinaryExpression( syntaxKind, leftExpressionSyntax, rightExpressionSyntax );
        }

        private bool NeedsCompilerFunctionCall( Token @operator ) {
            return @operator.Type == TokenType.StringEqual || @operator.Type == TokenType.Pow || @operator.Type == TokenType.And || @operator.Type == TokenType.Or || @operator.Type == TokenType.Concat;
        }

        private ExpressionSyntax CreateCompilerFunctionCall( BinaryExpression node, IContextService context ) {
            string operatorFunctionName = GetCompilerFunctionName( node.Operator.Token.Type );

            ExpressionSyntax leftExpressionSyntax = ConvertGeneric( node.Left, context );
            ExpressionSyntax rightExpressionSyntax = ConvertGeneric( node.Right, context );

            IEnumerable<ArgumentSyntax> arguments = Utils.GetEnumerable( SyntaxFactory.Argument( leftExpressionSyntax ), SyntaxFactory.Argument( rightExpressionSyntax ) );

            return CreateInvocationExpression( context.GetRuntimeInstanceName(), operatorFunctionName, arguments );
        }

        private string GetCompilerFunctionName( TokenType operatorType ) {
            string compilerFunctionName = null;
            switch (operatorType) {
                case TokenType.StringEqual:
                    compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.EqualString( null, null ) );
                    break;
                case TokenType.And:
                    compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.AND( null, null ) );
                    break;
                case TokenType.Or:
                    compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.OR( null, null ) );
                    break;
                case TokenType.Concat:
                    compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Concat( null, null ) );
                    break;
                case TokenType.Pow:
                    compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Pow( null, null ) );
                    break;
            }
            return compilerFunctionName;
        }

        private SyntaxKind GetSyntaxKind( Token @operator ) {
            switch (@operator.Type) {
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
