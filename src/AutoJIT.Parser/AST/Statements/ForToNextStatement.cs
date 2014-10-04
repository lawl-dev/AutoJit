using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ForToNextStatement : StatementBase
    {
        public readonly IExpressionNode StartExpression;
        public readonly IExpressionNode EndExpression;
        public readonly IExpressionNode StepExpression;
        public readonly IEnumerable<IStatementNode> Block;
        public readonly VariableExpression VariableExpression;

        public ForToNextStatement(
            VariableExpression variableExpression,
            IExpressionNode startExpression,
            IExpressionNode endExpression,
            IExpressionNode stepExpression,
            IEnumerable<IStatementNode> block ) {
            VariableExpression = variableExpression;
            StartExpression = startExpression;
            EndExpression = endExpression;
            StepExpression = stepExpression;
            Block = block;
            Initialize();
        }

        public override string ToSource() {
            var toReturn = string.Format( "For {0} = {1} To {2}", VariableExpression.ToSource(), StartExpression.ToSource(), EndExpression.ToSource() );
            if ( StartExpression != null ) {
                toReturn += string.Format( " Step {0}", StartExpression.ToSource() );
            }
            toReturn += Environment.NewLine;
            foreach (var statement in Block) {
                toReturn += string.Format( "{0}{1}", statement.ToSource(), Environment.NewLine );
            }
            toReturn += "NEXT";
            return toReturn;
        }

        public override object Clone() {
            return new ForToNextStatement(
                (VariableExpression) VariableExpression.Clone(), (IExpressionNode) StartExpression.Clone(), (IExpressionNode) EndExpression.Clone(),
                CloneAs<IExpressionNode>(  StepExpression), Block.Select( x => (IStatementNode) x.Clone() ) );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get {
                var syntaxNodes = new List<ISyntaxNode> { StartExpression, EndExpression, StepExpression, VariableExpression };

                if ( Block != null ) {
                    syntaxNodes.AddRange( Block );
                }

                return syntaxNodes;
            }
        }
    }
}
