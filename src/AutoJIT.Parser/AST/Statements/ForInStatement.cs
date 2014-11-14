using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ForInStatement : StatementBase
    {
        public ForInStatement( VariableExpression variableExpression, IExpressionNode toEnumerate, BlockStatement block ) {
            Block = block;
            VariableExpression = variableExpression;
            ToEnumerate = toEnumerate;
        }

        public BlockStatement Block { get; private set; }
        public IExpressionNode ToEnumerate { get; private set; }
        public VariableExpression VariableExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode> {
                    ToEnumerate,
                    VariableExpression
                };

                if ( Block != null ) {
                    syntaxNodes.Add( Block );
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
            toReturn += Block.ToSource();
            toReturn += "Next";
            return toReturn;
        }

        public override object Clone() {
            var statement = new ForInStatement( (VariableExpression) VariableExpression.Clone(), (IExpressionNode) ToEnumerate.Clone(), (BlockStatement) Block.Clone() );
            statement.Initialize();
            return statement;
        }

        public ForInStatement Update( IExpressionNode toEnumerate, VariableExpression variableExpression, BlockStatement block ) {
            if ( ToEnumerate == toEnumerate &&
                 VariableExpression == variableExpression &&
                 Block == block ) {
                return this;
            }
            var statement = new ForInStatement( (VariableExpression) variableExpression.Clone(), (IExpressionNode) toEnumerate.Clone(), (BlockStatement) block.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
