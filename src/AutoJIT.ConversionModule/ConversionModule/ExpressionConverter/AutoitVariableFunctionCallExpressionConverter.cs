using System.Collections.Generic;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Expressions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitVariableFunctionCallExpressionConverter : AutoitInvocationExpressionConverterBase<VariableFunctionCallExpression>
    {
        public AutoitVariableFunctionCallExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( VariableFunctionCallExpression node, IContextService contextService ) {
            IEnumerable<ArgumentSyntax> parameter = CreateParameter( node.Parameter, contextService );

            string invokeName = CompilerHelper.GetVariantMemberName( x => x.Invoke( null ) );

            return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, ConvertGeneric( node.VariableExpression, contextService ), SyntaxFactory.IdentifierName( invokeName ) ) ).WithArgumentList( SyntaxFactory.ArgumentList().AddArguments( parameter.ToArray() ) );
        }
    }
}
