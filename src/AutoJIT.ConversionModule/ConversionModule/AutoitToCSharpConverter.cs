using AutoJIT.CSharpConverter.ConversionModule.Visitor;
using AutoJIT.Parser.AST;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    public class AutoitToCSharpConverter : IAutoitToCSharpConverter
    {
        private readonly AutoJITScriptConversionVisitor _autoJITScriptConversionVisitor;

        public AutoitToCSharpConverter( AutoJITScriptConversionVisitor autoJITScriptConversionVisitor ) {
            _autoJITScriptConversionVisitor = autoJITScriptConversionVisitor;
        }

        public NamespaceDeclarationSyntax Convert( AutoitScriptRootNode root ) {
            _autoJITScriptConversionVisitor.InitializeContext( new Context( "_functions", "_context" ) );
            NamespaceDeclarationSyntax syntax = _autoJITScriptConversionVisitor.Visit( root );

            return syntax;
        }
    }
}
