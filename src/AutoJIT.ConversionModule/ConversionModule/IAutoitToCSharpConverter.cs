using AutoJIT.Parser.AST;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    public interface IAutoitToCSharpConverter
    {
        NamespaceDeclarationSyntax Convert( AutoitScriptRoot root );
    }
}
