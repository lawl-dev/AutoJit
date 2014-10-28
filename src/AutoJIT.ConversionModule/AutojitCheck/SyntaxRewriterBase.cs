using System;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.CSharpConverter.AutojitCheck
{
    public class SyntaxRewriterBase : ISyntaxVisitor<ISyntaxNode, ISyntaxNode>
    {
        private readonly IAutoitStatementFactory _statementFactory;

        public SyntaxRewriterBase( IAutoitStatementFactory statementFactory ) {
            _statementFactory = statementFactory;
        }

        ISyntaxNode ISyntaxVisitor<ISyntaxNode, ISyntaxNode>.Visit( ISyntaxNode node ) {
            return Visit( (dynamic)node );
        }

        public void Visit( ISyntaxNode node ) {
            throw new NotImplementedException();
        }

        public virtual ISyntaxNode Visit( ArrayInitExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( ArrayExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( BinaryExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( BooleanNegateExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( CallExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( DefaultExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( FalseLiteralExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( MacroExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( NegateExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( NullExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( NumericLiteralExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( StringLiteralExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( TernaryExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( TrueLiteralExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( UserfunctionCallExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( VariableExpression node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( AssignStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( ContinueCaseStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( ContinueloopStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( DimStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( DoUntilStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( EnumDeclarationStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( ExitStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( ForInStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( ForToNextStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( FunctionCallStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( GlobalDeclarationStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( IfElseStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( InitDefaultParameterStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( LocalDeclarationStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( ReDimStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( ReturnStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( SelectCaseStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( SwitchCaseStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( WhileStatement node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( FunctionNode node ) {
            return node;
        }

        public virtual ISyntaxNode Visit( AutoitScriptRootNode node ) {
            return node;
        }
    }
}
