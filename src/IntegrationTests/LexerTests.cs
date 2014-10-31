using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Compiler;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;
using NUnit.Framework;

namespace UnitTests
{
	public class LexerTests
	{
		private readonly string _script = "Func _StringFind($String,$Find_STR,$i_Case = 2) "+Environment.NewLine+"Local $occu = 1,$location "+Environment.NewLine+"Do "+Environment.NewLine+"$location = StringInStr($String,$Find_STR, $i_Case,$occu)"+Environment.NewLine+"$occu += 1"+Environment.NewLine+"ConsoleWrite($occu)"+Environment.NewLine+"Until $location = 0"+Environment.NewLine+"$occu -=2"+Environment.NewLine+"If $occu < 1 Then"+Environment.NewLine+"Return False"+Environment.NewLine+"Else "+Environment.NewLine+"Return $occu "+Environment.NewLine+"EndIf "+Environment.NewLine+"EndFunc  ";

		[Test]
		public void LexerTests123() {
			var autoJITContainer = new CompilerBootStrapper();
			var lexer = autoJITContainer.GetInstance<ILexer>();
			TokenCollection vectorTokens = lexer.Lex( _script );
			List<Token> tokens = vectorTokens.ToList();
		}
	}
}
