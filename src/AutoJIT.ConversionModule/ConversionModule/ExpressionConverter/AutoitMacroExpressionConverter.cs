using System;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Expressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitMacroExpressionConverter : AutoitExpressionConverterBase<MacroExpression>
    {
        private readonly ICSharpStatementFactory _cSharpStatementFactory;

        public AutoitMacroExpressionConverter( IInjectionService injectionService, ICSharpStatementFactory cSharpStatementFactory ) : base( injectionService ) {
            _cSharpStatementFactory = cSharpStatementFactory;
        }

        public override ExpressionSyntax Convert( MacroExpression node, IContextService contextService ) {
            string contextInstanceName = contextService.GetContextInstanceName();
            string macroName = CompilerHelper.GetMacros(x => x.Name.Equals(node.MacroName.Token.Value.StringValue, StringComparison.CurrentCultureIgnoreCase)).Single();

            return _cSharpStatementFactory.CreateMemberAccessExpression( contextInstanceName, macroName );
        }
    }
}
