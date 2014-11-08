using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class FunctionCallStatement : StatementBase
    {
        public FunctionCallStatement( CallExpression functionCallExpression ) {
            FunctionCallExpression = functionCallExpression;
            Initialize();
        }

        public CallExpression FunctionCallExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode> {
                    FunctionCallExpression
                };
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitFunctionCallStatement( this );
        }

        public override string ToSource() {
            return FunctionCallExpression.ToSource();
        }

        public override object Clone() {
            return new FunctionCallStatement( (CallExpression) FunctionCallExpression.Clone() );
        }

        public FunctionCallStatement Update( CallExpression functionCallExpression ) {
            if ( FunctionCallExpression == functionCallExpression ) {
                return this;
            }

            return new FunctionCallStatement( (CallExpression) functionCallExpression.Clone() );
        }
    }
}
