using System.Drawing.Imaging;
using AutoJIT.Parser;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Parser.Interface;

namespace IntegrationTests
{
    public class ShadowDeobfuscator
    {
        private readonly BinaryToStringRewriter _binaryToStringRewriter = new BinaryToStringRewriter();
        private readonly IScriptParser _scriptParser;

        public ShadowDeobfuscator() {
            var parserBootStrapper = new ParserBootStrapper();
            _scriptParser = parserBootStrapper.GetInstance<IScriptParser>();
        }

        public string Deobfuscate( string code ) {
            var autoitScriptRoot = _scriptParser.ParseScript( code, new PragmaOptions() );
            var newTree = _binaryToStringRewriter.Visit( autoitScriptRoot );
            return newTree.ToSource();
        }
    }
}