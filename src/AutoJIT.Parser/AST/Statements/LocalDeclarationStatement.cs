using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public class LocalDeclarationStatement : StatementBase
    {
        public LocalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            IsConst = isConst;
            VariableExpression = variableExpression;
            InitExpression = initExpression;
        }

        public bool IsConst { get; private set; }
        public VariableExpression VariableExpression { get; private set; }
        public IExpressionNode InitExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode> {
                    VariableExpression
                };

                if ( InitExpression != null ) {
                    nodes.Add( InitExpression );
                }

                return nodes;
            }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitLocalDeclarationStatement(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitLocalDeclarationStatement( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "Local {0}", VariableExpression.ToSource() );
            if ( InitExpression != null ) {
                toReturn += string.Format( " = {0}", InitExpression.ToSource() );
            }
            return toReturn;
        }

        public override object Clone() {
            var statement = new LocalDeclarationStatement( (VariableExpression) VariableExpression.Clone(), CloneAs<IExpressionNode>( InitExpression ), IsConst );
            statement.Initialize();
            return statement;
        }

        public virtual LocalDeclarationStatement Update( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            if ( VariableExpression == variableExpression &&
                 InitExpression == initExpression &&
                 IsConst == isConst ) {
                return this;
            }
            var statement = new LocalDeclarationStatement( (VariableExpression) variableExpression.Clone(), CloneAs<IExpressionNode>( initExpression ), isConst );
            statement.Initialize();
            return statement;
        }
    }
}
