using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST
{
    public sealed class AutoitScriptRootNode : SyntaxNodeBase
    {
        public readonly PragmaOptions PragmaOptions;

        public AutoitScriptRootNode( IEnumerable<FunctionNode> functions, FunctionNode main, PragmaOptions pragmaOptions ) {
            PragmaOptions = pragmaOptions;
            Functions = functions;
            MainFunctionNode = main;
            Initialize();
        }

        public FunctionNode MainFunctionNode { get; set; }
        public IEnumerable<FunctionNode> Functions { get; set; }


        public override string ToSource() {
            var toReturn = string.Empty;
            toReturn += MainFunctionNode.ToSource();

            foreach (var function in Functions) {
                toReturn += string.Format( "{0}{1}", function.ToSource(), Environment.NewLine );
            }
            return toReturn;
        }
        
        public override object Clone() {
            return new AutoitScriptRootNode( Functions.Select( x => (FunctionNode)x.Clone() ), (FunctionNode) MainFunctionNode.Clone(), PragmaOptions );
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode> { MainFunctionNode };
                syntaxNodes.AddRange( Functions );
                return syntaxNodes;
            }
        }
    }
}
