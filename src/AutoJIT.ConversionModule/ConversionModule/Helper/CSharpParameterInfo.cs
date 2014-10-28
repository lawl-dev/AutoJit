using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.Helper
{
    public sealed class CSharpParameterInfo
    {
        public readonly bool IsByRef;
        public readonly ExpressionSyntax Parameter;

        public CSharpParameterInfo( ExpressionSyntax expressionNode, bool isByRef ) {
            IsByRef = isByRef;
            Parameter = expressionNode;
        }
    }
}
