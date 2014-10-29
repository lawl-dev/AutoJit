using System.Collections.Generic;
using System.Linq;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal abstract class AutoitInvocationExpressionConverterBase<TExpression> : AutoitExpressionConverterBase<TExpression>
    where TExpression : IExpressionNode
    {
        protected AutoitInvocationExpressionConverterBase( IInjectionService injectionService ) : base( injectionService ) {}

        protected InvocationExpressionSyntax CreateInvocationExpression( string runtimeName, string functionName, IEnumerable<ArgumentSyntax> arguments ) {
            return
            SyntaxFactory.InvocationExpression(
                                               SyntaxFactory.MemberAccessExpression(
                                                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                                                    SyntaxFactory.IdentifierName( runtimeName ),
                                                                                    SyntaxFactory.IdentifierName( functionName ) ) )
                         .WithArgumentList( SyntaxFactory.ArgumentList( arguments.ToSeparatedSyntaxList() ) );
        }

        protected InvocationExpressionSyntax CreateInvocationExpression( string functionName, IEnumerable<ArgumentSyntax> arguments ) {
            return
            SyntaxFactory.InvocationExpression( SyntaxFactory.IdentifierName( functionName ) )
                         .WithArgumentList( SyntaxFactory.ArgumentList( arguments.ToSeparatedSyntaxList() ) );
        }

        protected IEnumerable<ArgumentSyntax> CreateParameter( IEnumerable<IExpressionNode> parameter, IContextService context ) {
            if( parameter == null
                || !parameter.Any() ) {
                return new ArgumentSyntax[0];
            }

            return parameter.Select( x => SyntaxFactory.Argument( ConverGeneric( x, context ) ) );
        }
    }
}
