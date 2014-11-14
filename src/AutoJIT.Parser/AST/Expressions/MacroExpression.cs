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
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( MacroName );
                return nodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitMacroExpression( this );
        }

        public override string ToSource() {
            return string.Format( "{0}", MacroName.ToSource() );
        }

        public override object Clone() {
            var expression = new MacroExpression( (TokenNode) MacroName.Clone() );
            expression.Initialize();
            return expression;
        }

        public MacroExpression Update( TokenNode macroName ) {
            if ( MacroName == macroName ) {
                return this;
            }
            var expression = new MacroExpression(  (TokenNode) macroName.Clone() );
            expression.Initialize();
            return expression;
        }
    }
}
