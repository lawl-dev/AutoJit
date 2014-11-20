using System.Collections.Generic;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.StatementConverter.Interface;
using AutoJIT.Parser;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Extensions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.Visitor
{
    public class ConversionVisitor : SyntaxVisitorBase<IEnumerable<CSharpSyntaxNode>>
    {
        private readonly ICSharpSkeletonFactory _cSharpSkeletonFactory;
        private readonly IInjectionService _injectionService;
        protected IContextService ContextService;

        public ConversionVisitor( IInjectionService injectionService, IContextService contextService, ICSharpSkeletonFactory cSharpSkeletonFactory ) {
            _injectionService = injectionService;
            ContextService = contextService;
            _cSharpSkeletonFactory = cSharpSkeletonFactory;
        }

        public void InitializeContext( IContext context ) {
            ContextService.Initialize( context );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitAssignStatement( AssignStatement node ) {
            return GetConverter<AssignStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitContinueCaseStatement( ContinueCaseStatement node ) {
            return GetConverter<ContinueCaseStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitContinueLoopStatement( ContinueLoopStatement node ) {
            return GetConverter<ContinueLoopStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitDimStatement( DimStatement node ) {
            return GetConverter<DimStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitDoUntilStatement( DoUntilStatement node ) {
            return GetConverter<DoUntilStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitExitloopStatement( ExitloopStatement node ) {
            return GetConverter<ExitloopStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitExitStatement( ExitStatement node ) {
            return GetConverter<ExitStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitForInStatement( ForInStatement node ) {
            return GetConverter<ForInStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitForToNextStatement( ForToNextStatement node ) {
            return GetConverter<ForToNextStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitFunctionCallStatement( FunctionCallStatement node ) {
            return GetConverter<FunctionCallStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitGlobalDeclarationStatement( GlobalDeclarationStatement node ) {
            return GetConverter<GlobalDeclarationStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitIfElseStatement( IfElseStatement node ) {
            return GetConverter<IfElseStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitInitDefaultParameterStatement( InitDefaultParameterStatement node ) {
            return GetConverter<InitDefaultParameterStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitLocalDeclarationStatement( LocalDeclarationStatement node ) {
            return GetConverter<LocalDeclarationStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitStaticDeclarationStatement( StaticDeclarationStatement node ) {
            return GetConverter<StaticDeclarationStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitGlobalEnumDeclarationStatement( GlobalEnumDeclarationStatement node ) {
            return GetConverter<GlobalEnumDeclarationStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitLocalEnumDeclarationStatement( LocalEnumDeclarationStatement node ) {
            return GetConverter<LocalEnumDeclarationStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitReDimStatement( ReDimStatement node ) {
            return GetConverter<ReDimStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitReturnStatement( ReturnStatement node ) {
            return GetConverter<ReturnStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitSelectCaseStatement( SelectCaseStatement node ) {
            return GetConverter<SelectCaseStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitSwitchCaseStatement( SwitchCaseStatement node ) {
            return GetConverter<SwitchCaseStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitWhileStatement( WhileStatement node ) {
            return GetConverter<WhileStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitVariableFunctionCallStatement( VariableFunctionCallStatement node ) {
            return GetConverter<VariableFunctionCallStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitFunction( Function node ) {
            return Convert( node, ContextService ).ToEnumerable();
        }

        public override IEnumerable<CSharpSyntaxNode> VisitBlockStatement( BlockStatement node ) {
            return GetConverter<BlockStatement>().Convert( node, ContextService );
        }

        public override IEnumerable<CSharpSyntaxNode> VisitAutoitScriptRoot( AutoitScriptRoot node ) {
            var memberList = new SyntaxList<MemberDeclarationSyntax>();

            ContextService.SetGlobalContext( true );
            var blockSyntax = (BlockSyntax) node.MainFunction.Accept( this ).Single();
            blockSyntax = blockSyntax.AddStatements(
                SyntaxFactory.ReturnStatement( SyntaxFactory.LiteralExpression( SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal( "0", 0 ) ) ) );

            var main = SyntaxFactory.MethodDeclaration(SyntaxFactory.IdentifierName(typeof(Variant).Name), "Main").AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword)).WithBody(blockSyntax);


            memberList = memberList.Add(main);


            ContextService.SetGlobalContext( false );
            memberList = memberList.AddRange( ContextService.PopGlobalVariables() );
            ContextService.ResetFunctionContext();

            foreach (Function function in node.Functions) {
                memberList = memberList.Add( (MemberDeclarationSyntax) function.Accept( this ).Single() );
                memberList = memberList.AddRange( ContextService.PopGlobalVariables() );
                ContextService.ResetFunctionContext();
            }

            NamespaceDeclarationSyntax finalScript = _cSharpSkeletonFactory.EmbedInClassTemplate( new List<MemberDeclarationSyntax>( memberList ), ContextService.GetRuntimeInstanceName(), "AutoJITScriptClass", ContextService.GetContextInstanceName() );

            finalScript = RemoveEmptyStatements( finalScript );

            finalScript = FixByReferenceCalls( finalScript, memberList );

            return finalScript.ToEnumerable();
        }

        protected MemberDeclarationSyntax Convert( Function function, IContextService context ) {
            IList<IStatementNode> statementNodes = function.Statements;
            statementNodes = DeclareParameter( statementNodes, function.Parameter, context );

            List<StatementSyntax> dotNetStatements = ConvertStatements( statementNodes );

            dotNetStatements = OrderDeclarations( dotNetStatements );

            if ( !( dotNetStatements.Last() is ReturnStatementSyntax ) ) {
                dotNetStatements.Add( SyntaxFactory.ReturnStatement( SyntaxFactory.LiteralExpression( SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal( "0", 0 ) ) ) );    
            }


            BlockSyntax body = dotNetStatements.ToBlock();

            return SyntaxFactory.MethodDeclaration( SyntaxFactory.IdentifierName( typeof (Variant).Name ), function.Name.Token.Value.StringValue ).AddModifiers( SyntaxFactory.Token( SyntaxKind.PublicKeyword ) ).WithParameterList( SyntaxFactory.ParameterList( CreaterParameter( function.Parameter, context ).ToSeparatedSyntaxList() ) ).WithBody( body );
        }

        private IList<IStatementNode> DeclareParameter( IList<IStatementNode> statementNodes, IEnumerable<AutoitParameter> parameter, IContextService context ) {
            foreach (AutoitParameter parameterInfo in parameter) {
                context.RegisterLocal( parameterInfo.ParameterName.Token.Value.StringValue );
                if ( parameterInfo.DefaultValue != null ) {
                    var statement = new InitDefaultParameterStatement( context.GetVariableName( parameterInfo.ParameterName.Token.Value.StringValue ), parameterInfo.DefaultValue );
                    statement.Initialize();
                    statementNodes.Insert( 0, statement );
                }
            }
            return statementNodes;
        }

        private static List<StatementSyntax> OrderDeclarations( List<StatementSyntax> cSharpStatements ) {
            List<LocalDeclarationStatementSyntax> allDeclarations = cSharpStatements.SelectMany( s => s.DescendantNodesAndSelf().OfType<LocalDeclarationStatementSyntax>() ).ToList();
            for ( int index = 0; index < cSharpStatements.Count; index++ ) {
                cSharpStatements[index] = cSharpStatements[index].ReplaceNodes( allDeclarations, ( node, syntaxNode ) => SyntaxFactory.EmptyStatement() );
            }
            cSharpStatements.InsertRange( 0, allDeclarations );
            return cSharpStatements;
        }

        private List<StatementSyntax> ConvertStatements( IEnumerable<IStatementNode> statements ) {
            List<CSharpSyntaxNode> nodes = statements.SelectMany( x => x.Accept( this ) ).ToList();
            return nodes.OfType<StatementSyntax>().ToList();
        }

        private IEnumerable<ParameterSyntax> CreaterParameter( IEnumerable<AutoitParameter> parameters, IContextService context ) {
            return parameters.Select(
                p => {
                    ParameterSyntax parameter = SyntaxFactory.Parameter( SyntaxFactory.Identifier( context.GetVariableName( p.ParameterName.Token.Value.StringValue ) ) ).WithType( SyntaxFactory.IdentifierName( typeof (Variant).Name ) );
                    if ( p.DefaultValue != null ) {
                        parameter = parameter.WithDefault( SyntaxFactory.EqualsValueClause( SyntaxFactory.LiteralExpression( SyntaxKind.NullLiteralExpression ) ) );
                    }

                    if ( p.IsByRef ) {
                        parameter = parameter.WithModifiers( new SyntaxTokenList().Add( SyntaxFactory.Token( SyntaxKind.RefKeyword ) ) );
                    }

                    return parameter;
                } );
        }

        private IAutoitStatementConverter<T, StatementSyntax> GetConverter<T>() where T : IStatementNode {
            var converter = _injectionService.Inject<IAutoitStatementConverter<T, StatementSyntax>>();
            return converter;
        }

        private static NamespaceDeclarationSyntax RemoveEmptyStatements( NamespaceDeclarationSyntax finalScript ) {
            List<EmptyStatementSyntax> emptyStatements = finalScript.DescendantNodes().OfType<EmptyStatementSyntax>().Where( x => x.Parent.GetType() != typeof (LabeledStatementSyntax) ).ToList();
            finalScript = finalScript.RemoveNodes( emptyStatements, SyntaxRemoveOptions.KeepEndOfLine );
            return finalScript;
        }

        private static NamespaceDeclarationSyntax FixByReferenceCalls( NamespaceDeclarationSyntax finalScript, SyntaxList<MemberDeclarationSyntax> memberList ) {
            var toReplace = new Dictionary<ArgumentSyntax, ArgumentSyntax>();
            IEnumerable<ArgumentSyntax> argumentSyntaxs = finalScript.DescendantNodes().OfType<ArgumentSyntax>();
            foreach (ArgumentSyntax argumentSyntax in argumentSyntaxs) {
                var invocationExpressionSyntax = argumentSyntax.Parent.Parent as InvocationExpressionSyntax;
                if ( invocationExpressionSyntax != null
                     &&
                     invocationExpressionSyntax.Expression is IdentifierNameSyntax ) {
                    string functionName = ( (IdentifierNameSyntax) invocationExpressionSyntax.Expression ).Identifier.Text;
                    MethodDeclarationSyntax methodDeclarationSyntax = memberList.OfType<MethodDeclarationSyntax>().SingleOrDefault( x => x.Identifier.Text == functionName );

                    if ( methodDeclarationSyntax != null ) {
                        List<ParameterSyntax> parameterSyntaxs = methodDeclarationSyntax.ParameterList.Parameters.Where( x => x.Modifiers.Any( m => m.ValueText.Equals( "ref" ) ) ).ToList();
                        var argumentListSyntax = argumentSyntax.Parent as ArgumentListSyntax;
                        int indexOfArgument = argumentListSyntax.Arguments.IndexOf( argumentSyntax );

                        foreach (ParameterSyntax parameterSyntax in parameterSyntaxs) {
                            int indexOfRefParameter = methodDeclarationSyntax.ParameterList.Parameters.IndexOf( parameterSyntax );

                            if ( indexOfArgument == indexOfRefParameter ) {
                                ArgumentSyntax withRef = argumentSyntax.WithRefOrOutKeyword( SyntaxFactory.Token( SyntaxKind.RefKeyword ) );
                                toReplace.Add( argumentSyntax, withRef );
                            }
                        }
                    }
                }
            }

            finalScript = finalScript.ReplaceNodes( toReplace.Keys, ( old, @new ) => toReplace[old] );
            return finalScript;
        }
    }
}
