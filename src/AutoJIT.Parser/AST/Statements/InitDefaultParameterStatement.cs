using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class InitDefaultParameterStatement : StatementBase
    {
        public string ParameterName { get; private set; }
        public IExpressionNode DefaultValue { get; private set; }

        public InitDefaultParameterStatement( string parameterName, IExpressionNode defaultValue ) {
            ParameterName = parameterName;
            DefaultValue = defaultValue;
            Initialize();
        }

        public override string ToSource() {
            return string.Empty;
        }

        public override object Clone() {
            return new InitDefaultParameterStatement( (string) ParameterName.Clone(), (IExpressionNode) DefaultValue.Clone() );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get { return new List<ISyntaxNode>() { DefaultValue }; }
        }
    }
}
