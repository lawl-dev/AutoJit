using System;
using System.Collections.Generic;
using AutoJIT.CSharpConverter.ConversionModule.StatementConverter.Interface;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.Visitor
{
	public class StatementConverterVisitor : IStatementVisitor<IEnumerable<StatementSyntax>>
	{
		private readonly IInjectionService _injectionService;
		protected IContextService ContextService;

		public StatementConverterVisitor( IInjectionService injectionService, IContextService contextService ) {
			_injectionService = injectionService;
			ContextService = contextService;
		}

		public IEnumerable<StatementSyntax> Visit( IStatementNode node ) {
			return Visit( (dynamic)node );
		}

		public void Visit( ISyntaxNode node ) {
			throw new NotImplementedException();
		}

		public void InitializeContext( IContext context ) {
			ContextService.Initialize( context );
		}

		public IEnumerable<StatementSyntax> Visit( AssignStatement node ) {
			return GetConverter<AssignStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( ContinueCaseStatement node ) {
			return GetConverter<ContinueCaseStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( ContinueloopStatement node ) {
			return GetConverter<ContinueloopStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( DimStatement node ) {
			return GetConverter<DimStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( DoUntilStatement node ) {
			return GetConverter<DoUntilStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( EnumDeclarationStatement node ) {
			return GetConverter<EnumDeclarationStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( ExitloopStatement node ) {
			return GetConverter<ExitloopStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( ExitStatement node ) {
			return GetConverter<ExitStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( ForInStatement node ) {
			return GetConverter<ForInStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( ForToNextStatement node ) {
			return GetConverter<ForToNextStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( FunctionCallStatement node ) {
			return GetConverter<FunctionCallStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( GlobalDeclarationStatement node ) {
			return GetConverter<GlobalDeclarationStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( IfElseStatement node ) {
			return GetConverter<IfElseStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( InitDefaultParameterStatement node ) {
			return GetConverter<InitDefaultParameterStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( LocalDeclarationStatement node ) {
			return GetConverter<LocalDeclarationStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( StaticDeclarationStatement node ) {
			return GetConverter<StaticDeclarationStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( GlobalEnumDeclarationStatement node ) {
			return GetConverter<GlobalEnumDeclarationStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( LocalEnumDeclarationStatement node ) {
			return GetConverter<LocalEnumDeclarationStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( ReDimStatement node ) {
			return GetConverter<ReDimStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( ReturnStatement node ) {
			return GetConverter<ReturnStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( SelectCaseStatement node ) {
			return GetConverter<SelectCaseStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( SwitchCaseStatement node ) {
			return GetConverter<SwitchCaseStatement>().Convert( node, ContextService );
		}

		public IEnumerable<StatementSyntax> Visit( WhileStatement node ) {
			return GetConverter<WhileStatement>().Convert( node, ContextService );
		}
        
        public IEnumerable<StatementSyntax> Visit( VariableFunctionCallStatement node ) {
            return GetConverter<VariableFunctionCallStatement>().Convert(node, ContextService);
		}

		private IAutoitStatementConverter<T, StatementSyntax> GetConverter<T>() where T : IStatementNode {
			var converter = _injectionService.Inject<IAutoitStatementConverter<T, StatementSyntax>>();
			return converter;
		}
	}
}
