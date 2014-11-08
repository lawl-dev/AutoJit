using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class MacroExpression : ExpressionBase
    {
        public MacroExpression( string macroName ) {
            MacroName = macroName;
        }

        public string MacroName { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitMacroExpression( this );
        }

        public override string ToSource() {
            return string.Format( "@{0}", MacroName );
        }

        public override object Clone() {
            return new MacroExpression( (string) MacroName.Clone() );
        }

        public MacroExpression Update( string macroName ) {
            if ( MacroName == macroName ) {
                return this;
            }
            return new MacroExpression( (string) macroName.Clone() );
        }
    }
}
