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
		public ForToNextStatement( VariableExpression variableExpression, IExpressionNode startExpression, IExpressionNode endExpression, IExpressionNode stepExpression, IEnumerable<IStatementNode> block ) {
			VariableExpression = variableExpression;
			StartExpression = startExpression;
			EndExpression = endExpression;
			StepExpression = stepExpression;
			Block = block;
			Initialize();
		}

		public IExpressionNode StartExpression { get; private set; }
		public IExpressionNode EndExpression { get; private set; }
		public IExpressionNode StepExpression { get; private set; }
		public IEnumerable<IStatementNode> Block { get; private set; }
		public VariableExpression VariableExpression { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var syntaxNodes = new List<ISyntaxNode> {
					StartExpression,
					EndExpression,
					StepExpression,
					VariableExpression
				};

				if( Block != null ) {
					syntaxNodes.AddRange( Block );
				}

				return syntaxNodes;
			}
		}

		public override string ToSource() {
			string toReturn = string.Format( "For {0} = {1} To {2}", VariableExpression.ToSource(), StartExpression.ToSource(), EndExpression.ToSource() );
			if( StartExpression != null ) {
				toReturn += string.Format( " Step {0}", StartExpression.ToSource() );
			}
			toReturn += Environment.NewLine;
			foreach(IStatementNode statement in Block) {
				toReturn += string.Format( "{0}{1}", statement.ToSource(), Environment.NewLine );
			}
			toReturn += "NEXT";
			return toReturn;
		}

		public override object Clone() {
			return new ForToNextStatement( (VariableExpression)VariableExpression.Clone(), (IExpressionNode)StartExpression.Clone(), (IExpressionNode)EndExpression.Clone(), CloneAs<IExpressionNode>( StepExpression ), Block.Select( x => (IStatementNode)x.Clone() ) );
		}
	}
}
