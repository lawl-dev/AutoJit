using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    public interface ICSharpSkeletonFactory
    {
        NamespaceDeclarationSyntax EmbedInClassTemplate(
        List<MemberDeclarationSyntax> memberDeclarationSyntaxs,
        string runtimeFieldName,
        string className,
        string contextInstanceName );
    }
}
