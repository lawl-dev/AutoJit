using System.Collections.Generic;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser
{
    internal class FunctionToken {
        public Token Name { get; private set; }
        public List<AutoitParameter> Parameter { get; private set; }
        public bool IsMain { get; private set; }
        public Queue<Token> Tokens { get; private set; }
        

        public FunctionToken( Token name, List<AutoitParameter> parameter, bool isMain = false ) {
            Name = name;
            Parameter = parameter;
            IsMain = isMain;
            Tokens = new Queue<Token>();
        }
    }
}