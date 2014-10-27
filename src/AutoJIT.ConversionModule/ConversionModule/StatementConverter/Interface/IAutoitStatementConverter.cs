using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter.Interface
{
    internal interface IAutoitStatementConverter<in TInStatement, out TOutStatement> : IAutoitStatementConverter<TOutStatement>
    where TInStatement : IStatementNode
    {
        IEnumerable<TOutStatement> Convert( TInStatement statement, IContextService context );
    }

    internal interface IAutoitStatementConverter<out TOutStatement>
    {
        IEnumerable<TOutStatement> ConvertGeneric( IStatementNode statement, IContextService context );
        IEnumerable<StatementSyntax> Convert( IStatementNode node, IContextService contextService );
    }
}
