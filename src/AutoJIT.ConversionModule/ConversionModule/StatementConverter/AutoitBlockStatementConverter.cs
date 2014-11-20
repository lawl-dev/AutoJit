using System.Collections.Generic;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitBlockStatementConverter : AutoitStatementConverterBase<BlockStatement>
    {
        public AutoitBlockStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( BlockStatement statement, IContextService context ) {
            BlockSyntax toReturn = statement.Block.SelectMany( x => ConvertGeneric( x, context ) ).ToBlock();
            return toReturn.ToEnumerable();
        }
    }
}
