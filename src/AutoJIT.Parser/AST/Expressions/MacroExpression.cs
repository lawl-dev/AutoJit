using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class MacroExpression : ExpressionBase
    {
        public MacroExpression( string macroName ) {
            MacroName = macroName;
        }

        public string MacroName { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<IExpressionNode>(); }
        }

        public override string ToSource() {
            return string.Format( "@{0}", MacroName );
        }

        public override object Clone() {
            return new MacroExpression( (string) MacroName.Clone() );
        }
    }
}
