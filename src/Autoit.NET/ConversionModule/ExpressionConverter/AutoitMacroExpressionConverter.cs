using System;
using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Visitor;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitMacroExpressionConverter : AutoitExpressionConverterBase<MacroExpression>
    {
        private readonly ICSharpStatementFactory _cSharpStatementFactory;

        public AutoitMacroExpressionConverter(
            IInjectionService injectionService,
            ICSharpStatementFactory cSharpStatementFactory ) : base( injectionService ) {
            _cSharpStatementFactory = cSharpStatementFactory;
        }

        public override ExpressionSyntax Convert(MacroExpression node, IContextService context)
        {
            return _cSharpStatementFactory.CreateMemberAccessExpression(
                context.GetContextInstanceName(),
                CompilerHelper.GetMacros( x => x.Name.Equals( node.MacroName, StringComparison.CurrentCultureIgnoreCase ) ).Single() );
        }
    }
}
