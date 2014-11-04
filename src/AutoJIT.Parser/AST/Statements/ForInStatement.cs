using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class ForInStatement : StatementBase
	{
		public ForInStatement( VariableExpression variableExpression, IExpressionNode toEnumerate, IEnumerable<IStatementNode> block ) {
			Block = block;
			VariableExpression = variableExpression;
			ToEnumerate = toEnumerate;
			Initialize();
		}

		public IEnumerable<IStatementNode> Block { get; private set; }
		public IExpressionNode ToEnumerate { get; private set; }
		public VariableExpression VariableExpression { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var syntaxNodes = new List<ISyntaxNode> {
					ToEnumerate,
					VariableExpression
				};

				if( Block != null ) {
					syntaxNodes.AddRange( Block );
				}

				return syntaxNodes;
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitForInStatement( this );
	    }

	    public override string ToSource() {
			string toReturn = string.Empty;
			toReturn += string.Format( "For {0} In {1}{2}", VariableExpression.ToSource(), ToEnumerate.ToSource(), Environment.NewLine );
			foreach(IStatementNode node in Block) {
				toReturn += string.Format( "{0}{1}", node.ToSource(), Environment.NewLine );
			}
			toReturn += string.Format( "Next{0}", Environment.NewLine );
			return toReturn;
		}

		public override object Clone() {
			return new ForInStatement( (VariableExpression)VariableExpression.Clone(), (IExpressionNode)ToEnumerate.Clone(), Block.Select( x => (IStatementNode)x.Clone() ) );
		}

	    public ForInStatement Update( IExpressionNode toEnumerate, VariableExpression variableExpression, IEnumerable<IStatementNode> block ) {
	        if ( ToEnumerate == toEnumerate &&
	             VariableExpression == variableExpression &&
	             Block == block ) {
	            return this;
	        }
            return new ForInStatement( variableExpression, toEnumerate, block );
	    }
	}
}
