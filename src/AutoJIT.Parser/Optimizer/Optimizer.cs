using System;
using System.Globalization;
using System.Linq;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJITRuntime;
using Lawl.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.Parser.Optimizer
{
    public sealed class Optimizer : CSharpSyntaxRewriter, IOptimizer
    {
        private bool _optimized;

        public SyntaxNode Optimize( SyntaxNode root ) {
            do {
                _optimized = false;
                root = Visit( root );
            } while ( _optimized );
            return root;
        }

        public override SyntaxNode VisitBinaryExpression( BinaryExpressionSyntax node ) {
            var leftCreationExpression = node.Left as ObjectCreationExpressionSyntax;
            var rightCreationExpression = node.Right as ObjectCreationExpressionSyntax;

            if ( leftCreationExpression == null ||
                 rightCreationExpression == null ) {
                return base.VisitBinaryExpression( node );
            }

            var isLeftVariantCreation = ( (IdentifierNameSyntax) leftCreationExpression.Type ).Identifier.Text == typeof (Variant).Name;
            var isRightVariantCreation = ( (IdentifierNameSyntax) rightCreationExpression.Type ).Identifier.Text == typeof (Variant).Name;

            if ( !isLeftVariantCreation ||
                 !isRightVariantCreation ) {
                return base.VisitBinaryExpression( node );
            }

            var leftLiteralExpressionSyntax = (LiteralExpressionSyntax) leftCreationExpression.ArgumentList.Arguments.Single().Expression;
            var rightLiteralExpressionSyntax = (LiteralExpressionSyntax) rightCreationExpression.ArgumentList.Arguments.Single().Expression;

            var leftOperant = Variant.Create( leftLiteralExpressionSyntax.Token.Value );
            var rightOperant = Variant.Create( rightLiteralExpressionSyntax.Token.Value );

            var result = GetBinaryExpressionResult( leftOperant, rightOperant, node.OperatorToken.Text );

            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ),
                    SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.Create( (object) null ) ) ) ) )
                .WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( VariantToLiteralExpression( result ) ).ToSeparatedSyntaxList() ) );
        }

        private Variant GetBinaryExpressionResult( Variant a, Variant b, string @operator ) {
            switch (@operator) {
                case "+":
                    return a+b;
                case "-":
                    return a-b;
                case "*":
                    return a * b;
                case "/":
                    return a / b;
                case "%":
                    throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }

        public override SyntaxNode VisitInvocationExpression( InvocationExpressionSyntax node ) {
            var memberAccessExpressionSyntax = node.Expression as MemberAccessExpressionSyntax;
            if ( memberAccessExpressionSyntax == null ) {
                return base.VisitInvocationExpression( node );
            }

            var functionName = memberAccessExpressionSyntax.Name.Identifier.Text;

            var runtimeClassType = typeof (AutoitRuntime<>).MakeGenericType( typeof (object) );

            var supportedFunctions =
                runtimeClassType.GetMethods().Where( x => x.CustomAttributes.Any( c => c.AttributeType == typeof (PreExecutableAttribute) ) ).ToList();
            var isSupportedFunctionCall = supportedFunctions.Any( x => x.Name == functionName );
            var args = node.ArgumentList.Arguments;

            var allArgumentsAreFixedValues = args.All( x => x.Expression is ObjectCreationExpressionSyntax );

            if ( !isSupportedFunctionCall ||
                 !allArgumentsAreFixedValues ) {
                return base.VisitInvocationExpression( node );
            }

            var compilerFunction = supportedFunctions.Single( x => x.Name == functionName && x.GetParameters().Length == args.Count );

            var runtimeInstance = runtimeClassType.CreateInstanceWithDefaultParameters();

            var parameters =
                args.Select( x => x.Expression as ObjectCreationExpressionSyntax )
                    .Select( x => x.ArgumentList.Arguments.Single().Expression as LiteralExpressionSyntax )
                    .Select( x => Variant.Create( x.Token.Value ) )
                    .ToArray();

            var result = (Variant) compilerFunction.Invoke( runtimeInstance, parameters );

            _optimized = true;

            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ),
                    SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.Create( (object) null ) ) ) ) )
                .WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( VariantToLiteralExpression( result ) ).ToSeparatedSyntaxList() ) );
        }

        private static LiteralExpressionSyntax VariantToLiteralExpression( Variant result ) {
            LiteralExpressionSyntax literalExpression = null;
            if ( result.IsString ) {
                {
                    literalExpression = SyntaxFactory.LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        SyntaxFactory.Literal(
                            SyntaxFactory.TriviaList(),
                            result.ToString(),
                            result.ToString(), SyntaxFactory.TriviaList() ) );
                }
            }
            if ( result.IsInt32 ) {
                {
                    literalExpression = SyntaxFactory.LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        SyntaxFactory.Literal(
                            SyntaxFactory.TriviaList(),
                            result.ToString(),
                            (int) result, SyntaxFactory.TriviaList() ) );
                }
            }
            if ( result.IsInt64 ) {
                {
                    literalExpression = SyntaxFactory.LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        SyntaxFactory.Literal(
                            SyntaxFactory.TriviaList(),
                            result.ToString(),
                            (Int64) result, SyntaxFactory.TriviaList() ) );
                }
            }
            if ( result.IsDouble ) {
                {
                    literalExpression = SyntaxFactory.LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        SyntaxFactory.Literal(
                            SyntaxFactory.TriviaList(),
                            ( (double) result ).ToString( CultureInfo.InvariantCulture ),
                            (double) result, SyntaxFactory.TriviaList() ) );
                }
            }

            if ( result.IsBool ) {
                if ( result ) {
                    literalExpression = SyntaxFactory.LiteralExpression(
                        SyntaxKind.TrueLiteralExpression );
                }
                else {
                    literalExpression = SyntaxFactory.LiteralExpression(
                        SyntaxKind.FalseLiteralExpression );
                }
            }
            return literalExpression;
        }
    }
}
