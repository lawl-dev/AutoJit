using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Visitor;
using AutoJIT.Parser.AST;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
	public class AutoitToCSharpConverter : IAutoitToCSharpConverter
	{
	    private readonly ConversionVisitor _conversionVisitor;

	    public AutoitToCSharpConverter(ConversionVisitor conversionVisitor) {
            _conversionVisitor = conversionVisitor;
        }

	    public NamespaceDeclarationSyntax Convert( AutoitScriptRoot root ) {
			_conversionVisitor.InitializeContext( new Context( "_functions", "_context" ) );
	        var cSharpSyntaxNode = _conversionVisitor.Visit( root ).Single();
	        return (NamespaceDeclarationSyntax) cSharpSyntaxNode;
	    }
	}
}
