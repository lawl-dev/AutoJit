using System.Linq;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;

namespace AutoJIT.CodeCleanup
{
    public class FunctionOrderRewriter : SyntaxRewriterBase
    {
        private readonly IAutoitSyntaxFactory _syntaxFactory = new AutoitSyntaxFactory(new TokenFactory());
        
        public override ISyntaxNode VisitAutoitScriptRoot( AutoitScriptRoot node ) {
            var functions = node.Functions.OrderBy( x=>x.Name.Token.Value.StringValue ).Select( x=>(Function)x.Clone() ).ToList();

            return _syntaxFactory.CreateRoot( functions, (BlockStatement) node.MainFunction.Clone(), node.PragmaOptions );
        }
    }
}