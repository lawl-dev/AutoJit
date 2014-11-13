using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class MacroExpression : ExpressionBase
    {
        public MacroExpression( TokenNode macroName ) {
            MacroName = macroName;
        }

        public TokenNode MacroName { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitMacroExpression( this );
        }

        public override string ToSource() {
            return string.Format( "{0}", MacroName.ToSource() );
        }

        public override object Clone() {
            return new MacroExpression( (TokenNode) MacroName.Clone() );
        }

        public MacroExpression Update( TokenNode macroName ) {
            if ( MacroName == macroName ) {
                return this;
            }
            return new MacroExpression(  (TokenNode) macroName.Clone() );
        }
    }
}
