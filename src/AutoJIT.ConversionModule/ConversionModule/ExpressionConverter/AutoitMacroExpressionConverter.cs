using System;
using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitMacroExpressionConverter : AutoitExpressionConverterBase<MacroExpression>
    {
        private readonly ICSharpStatementFactory _cSharpStatementFactory;

        public AutoitMacroExpressionConverter( IInjectionService injectionService, ICSharpStatementFactory cSharpStatementFactory ) : base( injectionService ) {
            _cSharpStatementFactory = cSharpStatementFactory;
        }

        public override ExpressionSyntax Convert( MacroExpression node, IContextService context ) {
            string contextInstanceName = context.GetContextInstanceName();
            string macroName = CompilerHelper.GetMacros( x => x.Name.Equals( node.MacroName, StringComparison.CurrentCultureIgnoreCase ) ).Single();

            return _cSharpStatementFactory.CreateMemberAccessExpression( contextInstanceName, macroName );
        }
    }
}
