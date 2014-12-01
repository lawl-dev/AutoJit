using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ReDimStatement : StatementBase
    {
        public ReDimStatement( ArrayExpression arrayExpression ) {
            ArrayExpression = arrayExpression;
        }

        public ArrayExpression ArrayExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode> {
                    ArrayExpression
                };
            }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitReDimStatement(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitReDimStatement( this );
        }

        public override string ToSource() {
            return string.Format( "ReDim {0}", ArrayExpression.ToSource() );
        }

        public override object Clone() {
            var statement = new ReDimStatement( (ArrayExpression) ArrayExpression.Clone() );
            statement.Initialize();
            return statement;
        }

        public ReDimStatement Update( ArrayExpression arrayExpression ) {
            if ( ArrayExpression == arrayExpression ) {
                return this;
            }
            var statement = new ReDimStatement( (ArrayExpression) arrayExpression.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
