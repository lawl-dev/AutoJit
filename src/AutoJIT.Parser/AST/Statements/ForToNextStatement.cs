using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ForToNextStatement : StatementBase
    {
        public ForToNextStatement( VariableExpression variableExpression, IExpressionNode startExpression, IExpressionNode endExpression, IExpressionNode stepExpression, BlockStatement block ) {
            VariableExpression = variableExpression;
            StartExpression = startExpression;
            EndExpression = endExpression;
            StepExpression = stepExpression;
            Block = block;
        }

        public IExpressionNode StartExpression { get; private set; }
        public IExpressionNode EndExpression { get; private set; }
        public IExpressionNode StepExpression { get; private set; }
        public BlockStatement Block { get; private set; }
        public VariableExpression VariableExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode> {
                    StartExpression,
                    EndExpression,
                    VariableExpression
                };

                if ( StepExpression != null ) {
                    syntaxNodes.Add(StepExpression);
                }

                if ( Block != null ) {
                    syntaxNodes.Add( Block );
                }

                return syntaxNodes;
            }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitForToNextStatement(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitForToNextStatement( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "For {0} = {1} To {2}", VariableExpression.ToSource(), StartExpression.ToSource(), EndExpression.ToSource() );
            if ( StepExpression != null ) {
                toReturn += string.Format( " Step {0}", StepExpression.ToSource() );
            }
            toReturn += Environment.NewLine;
            toReturn += Block.ToSource();
            toReturn += "Next";
            return toReturn;
        }

        public override object Clone() {
            var statement = new ForToNextStatement( (VariableExpression) VariableExpression.Clone(), (IExpressionNode) StartExpression.Clone(), (IExpressionNode) EndExpression.Clone(), CloneAs<IExpressionNode>( StepExpression ), (BlockStatement) Block.Clone() );

            statement.Initialize();
            return statement;
        }

        public ForToNextStatement Update( VariableExpression variableExpression, IExpressionNode startExpression, IExpressionNode endExpression, IExpressionNode stepExpression, BlockStatement block ) {
            if ( VariableExpression == variableExpression &&
                 StartExpression == startExpression &&
                 EndExpression == endExpression &&
                 StepExpression == stepExpression &&
                 Block == block ) {
                return this;
            }
            var statement = new ForToNextStatement( (VariableExpression) variableExpression.Clone(), (IExpressionNode) startExpression.Clone(), (IExpressionNode) endExpression.Clone(), CloneAs<IExpressionNode>( stepExpression ), (BlockStatement) block.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
