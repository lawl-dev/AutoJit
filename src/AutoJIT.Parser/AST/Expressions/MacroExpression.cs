using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class MacroExpression : ExpressionBase
    {
        public readonly string MacroName;

        public MacroExpression( string macroName ) {
            MacroName = macroName;
        }

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
