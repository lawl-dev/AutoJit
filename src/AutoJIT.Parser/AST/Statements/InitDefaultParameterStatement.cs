using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class InitDefaultParameterStatement : StatementBase
    {
        public InitDefaultParameterStatement( string parameterName, IExpressionNode defaultValue ) {
            ParameterName = parameterName;
            DefaultValue = defaultValue;
            Initialize();
        }

        public string ParameterName { get; private set; }
        public IExpressionNode DefaultValue { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode> {
                    DefaultValue
                };
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitInitDefaultParameterStatement( this );
        }

        public override string ToSource() {
            return string.Empty;
        }

        public override object Clone() {
            return new InitDefaultParameterStatement( (string) ParameterName.Clone(), (IExpressionNode) DefaultValue.Clone() );
        }

        public InitDefaultParameterStatement Update( string parameterName, IExpressionNode defaultValue ) {
            if ( ParameterName == parameterName &&
                 DefaultValue == defaultValue ) {
                return this;
            }
            return new InitDefaultParameterStatement( (string) parameterName.Clone(), (IExpressionNode) defaultValue.Clone() );
        }
    }
}
