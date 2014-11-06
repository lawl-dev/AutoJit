using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST
{
    public sealed class AutoitScriptRoot : SyntaxNodeBase
    {
        public readonly PragmaOptions PragmaOptions;

        public AutoitScriptRoot( IEnumerable<Function> functions, Function main, PragmaOptions pragmaOptions ) {
            PragmaOptions = pragmaOptions;
            Functions = functions;
            MainFunction = main;
            Initialize();
        }

        public Function MainFunction { get; set; }
        public IEnumerable<Function> Functions { get; set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode> {
                    MainFunction
                };
                syntaxNodes.AddRange( Functions );
                return syntaxNodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitAutoitScriptRoot( this );
        }

        public override string ToSource() {
            string toReturn = string.Empty;
            toReturn += MainFunction.ToSource();

            foreach (Function function in Functions) {
                toReturn += string.Format( "{0}{1}", function.ToSource(), Environment.NewLine );
            }
            return toReturn;
        }

        public override object Clone() {
            return new AutoitScriptRoot( Functions.Select( x => (Function) x.Clone() ), (Function) MainFunction.Clone(), PragmaOptions );
        }

        public AutoitScriptRoot Update( Function mainFunction, IEnumerable<Function> functions, PragmaOptions pragmaOptions ) {
            if ( MainFunction == mainFunction &&
                 Functions == functions ) {
                return this;
            }
            return new AutoitScriptRoot( functions, mainFunction, pragmaOptions );
        }
    }
}
