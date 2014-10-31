using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class IfElseStatement : StatementBase
	{
		public IfElseStatement( IExpressionNode condition, IEnumerable<IStatementNode> ifBlock, IEnumerable<IExpressionNode> elseIfConditions, IEnumerable<IEnumerable<IStatementNode>> elseIfBlocks, IEnumerable<IStatementNode> elseBlock ) {
			Condition = condition;
			IfBlock = ifBlock;
			ElseIfConditions = elseIfConditions;
			ElseIfBlocks = elseIfBlocks;
			ElseBlock = elseBlock;
			Initialize();
		}

		public IExpressionNode Condition { get; private set; }
		public IEnumerable<IStatementNode> IfBlock { get; private set; }
		public IEnumerable<IExpressionNode> ElseIfConditions { get; private set; }
		public IEnumerable<IEnumerable<IStatementNode>> ElseIfBlocks { get; private set; }
		public IEnumerable<IStatementNode> ElseBlock { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var syntaxNodes = new List<ISyntaxNode>();
				syntaxNodes.Add( Condition );

				if( IfBlock != null ) {
					syntaxNodes.AddRange( IfBlock );
				}

				if( ElseIfConditions != null ) {
					syntaxNodes.AddRange( ElseIfConditions );
				}

				if( ElseIfBlocks != null ) {
					syntaxNodes.AddRange( ElseIfBlocks.SelectMany( x => x ) );
				}

				if( ElseBlock != null ) {
					syntaxNodes.AddRange( ElseBlock );
				}

				return syntaxNodes;
			}
		}

		public override string ToSource() {
			string toReturn = string.Format( "If {0} Then {1}", Condition.ToSource(), Environment.NewLine );
			foreach(IStatementNode statement in IfBlock) {
				toReturn += string.Format( "{0}{1}", statement.ToSource(), Environment.NewLine );
			}
			if( ElseIfConditions != null ) {
				for( int i = 0; i < ElseIfConditions.Count(); i++ ) {
					IExpressionNode conditionExpression = ElseIfConditions.Skip( i ).First();
					IEnumerable<IStatementNode> statements = ElseIfBlocks.Skip( i ).First();
					toReturn += string.Format( "ElseIf {0}{1}", conditionExpression.ToSource(), Environment.NewLine );
					foreach(IStatementNode statement in statements) {
						toReturn += string.Format( "{0}{1}", statement.ToSource(), Environment.NewLine );
					}
				}
			}

			if( ElseBlock != null ) {
				toReturn += "Else"+Environment.NewLine;
				foreach(IStatementNode statement in ElseBlock) {
					toReturn += string.Format( "{0}{1}", statement.ToSource(), Environment.NewLine );
				}
			}
			toReturn += "WEnd";
			return toReturn;
		}

		public override object Clone() {
			return new IfElseStatement(
			(IExpressionNode)Condition.Clone(),
			IfBlock.Select( x => (IStatementNode)x.Clone() ),
			CloneEnumerableAs<IExpressionNode>( ElseIfConditions ),
			ElseIfBlocks != null
			? ElseIfBlocks.Select( CloneEnumerableAs<IStatementNode> )
			: null,
			CloneEnumerableAs<IStatementNode>( ElseBlock ) );
		}
	}
}
