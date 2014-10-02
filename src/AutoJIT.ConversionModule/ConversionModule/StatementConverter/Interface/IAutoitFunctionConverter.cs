using AutoJIT.Parser.AST;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter.Interface
{
    internal interface IAutoitFunctionConverter
    {
        MemberDeclarationSyntax Convert( FunctionNode function, IContext context );
    }
}
