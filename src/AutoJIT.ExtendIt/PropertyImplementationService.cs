using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoJIT.Parser;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Parser.Interface;

namespace AutoJIT.ExtendIt
{
    public class PropertyImplementationService
    {
        private readonly PropertyRewriter _propertyRewriter = new PropertyRewriter();
        private readonly IScriptParser _scriptParser;

        public PropertyImplementationService()
        {
            _scriptParser = new OOPParserBootStrapper().GetInstance<IScriptParser>();
        }
        
        public string ImplementProperties( string autoitCode ) {
            var root = _scriptParser.ParseScript( autoitCode, new PragmaOptions() );
            var newTree = _propertyRewriter.ImplementProperties( root );
            return newTree.ToSource();
        }
    }
}
