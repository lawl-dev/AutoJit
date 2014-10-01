using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.Parser.Helper
{
    public sealed class CSharpParameterInfo
    {
        public readonly ExpressionSyntax Parameter;

        public readonly bool IsByRef;

        public CSharpParameterInfo( ExpressionSyntax expressionNode, bool isByRef ) {
            IsByRef = isByRef;
            Parameter = expressionNode;
        }
    }
}
