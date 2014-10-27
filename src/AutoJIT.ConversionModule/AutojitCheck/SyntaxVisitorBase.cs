using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.CSharpConverter.AutojitCheck
{
    public class SyntaxVisitorBase : ISyntaxVisitor
    {
        public void Visit( ISyntaxNode node ) {
            Visit( (dynamic)node );
        }

        public virtual void Visit( ArrayInitExpression node ) {}

        public virtual void Visit( ArrayExpression node ) {}

        public virtual void Visit( BinaryExpression node ) {}

        public virtual void Visit( BooleanNegateExpression node ) {}

        public virtual void Visit( CallExpression node ) {}

        public virtual void Visit( DefaultExpression node ) {}

        public virtual void Visit( FalseLiteralExpression node ) {}

        public virtual void Visit( MacroExpression node ) {}

        public virtual void Visit( NegateExpression node ) {}

        public virtual void Visit( NullExpression node ) {}

        public virtual void Visit( NumericLiteralExpression node ) {}

        public virtual void Visit( StringLiteralExpression node ) {}

        public virtual void Visit( TernaryExpression node ) {}

        public virtual void Visit( TrueLiteralExpression node ) {}

        public virtual void Visit( UserfunctionCallExpression node ) {}

        public virtual void Visit( VariableExpression node ) {}

        public virtual void Visit( AssignStatement node ) {}

        public virtual void Visit( ContinueCaseStatement node ) {}

        public virtual void Visit( ContinueloopStatement node ) {}

        public virtual void Visit( DimStatement node ) {}

        public virtual void Visit( DoUntilStatement node ) {}

        public virtual void Visit( EnumDeclarationStatement node ) {}

        public virtual void Visit( ExitStatement node ) {}

        public virtual void Visit( ForInStatement node ) {}

        public virtual void Visit( ForToNextStatement node ) {}

        public virtual void Visit( FunctionCallStatement node ) {}

        public virtual void Visit( GlobalDeclarationStatement node ) {}

        public virtual void Visit( IfElseStatement node ) {}

        public virtual void Visit( InitDefaultParameterStatement node ) {}

        public virtual void Visit( LocalDeclarationStatement node ) {}

        public virtual void Visit( ReDimStatement node ) {}

        public virtual void Visit( ReturnStatement node ) {}

        public virtual void Visit( SelectCaseStatement node ) {}

        public virtual void Visit( SwitchCaseStatement node ) {}

        public virtual void Visit( WhileStatement node ) {}

        public virtual void Visit( FunctionNode node ) {}

        public virtual void Visit( AutoitScriptRootNode node ) {}
    }
}
