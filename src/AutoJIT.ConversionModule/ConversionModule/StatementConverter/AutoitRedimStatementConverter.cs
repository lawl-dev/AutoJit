using System.Collections.Generic;
using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitRedimStatementConverter : AutoitStatementConverterBase<ReDimStatement>
    {
        public AutoitRedimStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ReDimStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            string variableName = context.GetVariableName( statement.ArrayExpression.IdentifierName );
            IEnumerable<CSharpParameterInfo> parameter = statement.ArrayExpression.AccessParameter.Select( x => new CSharpParameterInfo( ConvertGeneric( x, context ), false ) );

            toReturn.Add( CSharpStatementFactory.CreateInvocationExpression( variableName, CompilerHelper.GetVariantMemberName( x => x.ReDim() ), parameter ).ToStatementSyntax() );

            return toReturn;
        }
    }
}
