using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class GlobalDeclarationStatement : StatementBase
    {
        public GlobalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            IsConst = isConst;
            VariableExpression = variableExpression;
            InitExpression = initExpression;
            Initialize();
        }

        public bool IsConst { get; private set; }
        public VariableExpression VariableExpression { get; private set; }
        public IExpressionNode InitExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode> {
                    VariableExpression,
                    InitExpression
                };
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitGlobalDeclarationStatement( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "Global {0}", VariableExpression.ToSource() );
            if ( InitExpression != null ) {
                toReturn += string.Format( " = {0}", InitExpression.ToSource() );
            }
            return toReturn;
        }

        public override object Clone() {
            return new GlobalDeclarationStatement( (VariableExpression) VariableExpression.Clone(), CloneAs<IExpressionNode>( InitExpression ), IsConst );
        }

        public GlobalDeclarationStatement Update( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            if ( VariableExpression == variableExpression &&
                 InitExpression == initExpression &&
                 IsConst == isConst ) {
                return this;
            }
            return new GlobalDeclarationStatement( (VariableExpression) variableExpression.Clone(), (IExpressionNode) initExpression.Clone(), isConst );
        }
    }
}
