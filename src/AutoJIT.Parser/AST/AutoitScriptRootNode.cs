using System;
using System.Collections.Generic;
using System.Linq;

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

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var syntaxNodes = new List<ISyntaxNode> {
					MainFunctionNode
				};
				syntaxNodes.AddRange( Functions );
				return syntaxNodes;
			}
		}

		public override string ToSource() {
			string toReturn = string.Empty;
			toReturn += MainFunctionNode.ToSource();

			foreach(FunctionNode function in Functions) {
				toReturn += string.Format( "{0}{1}", function.ToSource(), Environment.NewLine );
			}
			return toReturn;
		}

		public override object Clone() {
			return new AutoitScriptRootNode( Functions.Select( x => (FunctionNode)x.Clone() ), (FunctionNode)MainFunctionNode.Clone(), PragmaOptions );
		}
	}
}
