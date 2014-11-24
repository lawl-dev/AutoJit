using System.Collections.Generic;
using System.IO;
using AutoJIT.Contrib;
using AutoJIT.Parser;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Parser.Strategy;
using AutoJIT.Parser.Lex;
using NUnit.Framework;

namespace IntegrationTests
{
    class TestDeobfuscator
    {
        [Test]
        public void Foo() {
            var script = File.ReadAllText(@"C:\Users\Brunnmeier\Documents\PrivateGIT\OPENSOURCE\Autojit\src\IntegrationTests\testdata\userfunctions\DES.au3");
            var optimizer = new AutoitOptimizer();
            var optimize = optimizer.Optimize( script );
        }
    }

    public class AutoitOptimizer
    {
        private readonly AutoitOptimizerRewriter _autoitOptimizerRewriter = new AutoitOptimizerRewriter();
        private readonly IScriptParser _scriptParser;

        public AutoitOptimizer() {
            var bootStrapper = new ParserBootStrapper();
            _scriptParser = bootStrapper.GetInstance<IScriptParser>();
        }

        public string Optimize( string autoitCode ) {
            var root = _scriptParser.ParseScript( autoitCode, new PragmaOptions() );
            var newTree = _autoitOptimizerRewriter.Visit( root );
            return newTree.ToSource();
        }
    }
}