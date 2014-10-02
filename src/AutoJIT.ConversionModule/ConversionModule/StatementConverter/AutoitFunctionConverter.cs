using AutoJIT.CSharpConverter.ConversionModule.StatementConverter.Interface;
using AutoJIT.CSharpConverter.ConversionModule.Visitor;
using AutoJIT.Parser.AST;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal class AutoitFunctionConverter : IAutoitFunctionConverter
    {
        private readonly StatementConverterVisitor _statementConverterVisitor;

        public AutoitFunctionConverter( StatementConverterVisitor statementConverterVisitor ) {
            _statementConverterVisitor = statementConverterVisitor;
        }

        public MemberDeclarationSyntax Convert( FunctionNode function, IContext context ) {
            throw new System.NotImplementedException();
        }
    }
}
