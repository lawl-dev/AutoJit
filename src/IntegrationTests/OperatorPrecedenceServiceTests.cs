using System;
using System.Globalization;
using AutoJIT;
using AutoJIT.Compiler;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex.Interface;
using NUnit.Framework;

namespace UnitTests
{
    public class OperatorPrecedenceServiceTests : AutoitFunctionTestBase
    {
        private readonly ILexer _lexer;
        private readonly IOperatorPrecedenceService _operatorPrecedenceService;

        public OperatorPrecedenceServiceTests() {
            var standardAutoJITContainer = new CompilerBootStrapper();

            _lexer = standardAutoJITContainer.GetInstance<ILexer>();
            _operatorPrecedenceService = standardAutoJITContainer.GetInstance<IOperatorPrecedenceService>();
        }

        [TestCase( "NOT 1" )]
        [TestCase( "1 OR (0 + 1)" )]
        [TestCase( "IsNumber(1) OR IsNumber('a123a')" )]
        [TestCase( "IsNumber('1') OR IsNumber('123a')" )]
        [TestCase( "IsNumber(1) OR IsNumber('a123a')" )]
        [TestCase( "1 OR (0 + 1) AND 2" )]
        [TestCase( "IsNumber(1) OR IsNumber('a123a') AND IsNumber('a')" )]
        [TestCase( "IsNumber('1') OR IsNumber('123a') AND IsNumber('2')" )]
        [TestCase( "IsNumber(1) OR IsNumber('a123a') OR IsNumber(123)" )]
        [TestCase( "13123 + 312" )]
        [TestCase( "13123 + 312 * 3" )]
        [TestCase( "13123 + 312 * 3 - 4" )]
        [TestCase( "13123 + 312 * 3 - 4 / 6" )]
        [TestCase( "13123 + 312 * 3 - 4 / 6 ^ 2" )]
        [TestCase( "13123 + 312 + 3 ^ 9 = 13123" )]
        [TestCase( "(13123 + 312)" )]
        [TestCase( "(13123 + 312) * 3" )]
        [TestCase( "13123 + 312 * (3 - 4)" )]
        [TestCase( "13123 + (312 * 3 - 4) / 6" )]
        [TestCase( "13123 + (312 * 3 - 4 / 6) ^ 2" )]
        [TestCase( "13123 + 312 + (3 ^ 9) = 13123" )]
        [TestCase( "9 - 13123 + 312 * (3 - 4)" )]
        [TestCase( "13123 + (312 * 3 - 4) / 6" )]
        [TestCase( "13123 - (312 * 3 - 4 / 6) ^ 2" )]
        [TestCase( "13123 + 312 + (3 ^ 9) = 13123" )]
        [TestCase( "(13123 + 312 / 13123) / 3 * 7" )]
        [TestCase( "(13123 + 312 / 13123) / 3 * 7 / 8" )]
        [TestCase( "(13123 + 312 / 13123) / 3 * 7 / 8 * 9" )]
        [TestCase( "(13123 + 312 / 13123) / 3 * 7 / 8 * 9 / 131230" )]
        [TestCase( "Cos(13123) + Sin(312)" )]
        [TestCase( "Cos(13123) + Sin(312) * Cos(3)" )]
        [TestCase( "Cos(13123) + Sin(312) * Cos(3) - Sin(4)" )]
        [TestCase( "Cos(13123) + Sin(312) * Cos(3) - Sin(4) / Cos(6)" )]
        [TestCase( "Cos(13123) + Sin(312) * Cos(3) - Sin(4) / Cos(6) ^ Sin(8)" )]
        [TestCase( "Cos(13123) + Sin(312) + Cos(3) ^ Sin(9) = Cos(13123)" )]
        [TestCase( "(Cos(13123) + Sin(312))" )]
        [TestCase( "(Cos(13123) + Sin(312)) * Cos(3)" )]
        [TestCase( "Cos(13123) + Sin(312) * (Cos(3) - Sin(4))" )]
        [TestCase( "Cos(13123) + (Sin(312) * Cos(3) - Sin(4)) / Cos(6)" )]
        [TestCase( "Cos(13123) + (Sin(312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)" )]
        [TestCase( "Cos(13123) + Sin(312) + (Cos(3) ^ Sin(9)) = Cos(13123)" )]
        [TestCase( "Cos(9) - Sin(13123) + Cos(312) * (Cos(3) - Cos(4))" )]
        [TestCase( "Cos(13123) + (Cos(312) * Sin(3) - Cos(4)) / Sin(6)" )]
        [TestCase( "Cos(13123) - (Sin(312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)" )]
        [TestCase( "Cos(13123) + Sin(312) + (Cos(3) ^ Sin(9)) = Cos(13123)" )]
        [TestCase( "(Cos(13123) + Sin(312) / Cos(13123)) / Sin(3) * Cos(7)" )]
        [TestCase( "(Cos(13123) + Sin(312) / Cos(13123)) / Sin(3) * Cos(7) / Sin(8)" )]
        [TestCase( "(Cos(13123) + Sin(312) / Cos(13123)) / Sin(3) * Cos(7) / Sin(8) * Cos(9)" )]
        [TestCase( "(Cos(13123) + Sin(312) / Cos(13123)) / Cos(3) * Cos(7) / Cos(8) * Cos(9) / Cos(131230)" )]
        [TestCase( "'13123' + '312'" )]
        [TestCase( "'13123' + '312' * '3'" )]
        [TestCase( "'13123' + '312' * '3' - '4'" )]
        [TestCase( "'13123' + '312' * '3' - '4' / '6'" )]
        [TestCase( "'13123' + '312' * '3' - '4' / '6' ^ '8'" )]
        [TestCase( "'13123' + '312' + '3' ^ '9' = '13123'" )]
        [TestCase( "('13123' + '312')" )]
        [TestCase( "('13123' + '312') * '3'" )]
        [TestCase( "'13123' + '312' * ('3' - '4')" )]
        [TestCase( "'13123' + ('312' * '3' - '4') / '6'" )]
        [TestCase( "'13123' + ('312' * '3' - '4' / '6') ^ '8'" )]
        [TestCase( "'13123' + '312' + ('3' ^ '9') = '13123'" )]
        [TestCase( "'9' - '13123' + '312' * ('3' - '4')" )]
        [TestCase( "'13123' + ('312' * '3' - '4') / '6'" )]
        [TestCase( "'13123' - ('312' * '3' - '4' / '6') ^ '8'" )]
        [TestCase( "'13123' + '312' + ('3' ^ '9') = '13123'" )]
        [TestCase( "('13123' + '312' / '13123') / '3' * '7'" )]
        [TestCase( "('13123' + '312' / '13123') / '3' * '7' / '8'" )]
        [TestCase( "('13123' + '312' / '13123') / '3' * '7' / '8' * '9'" )]
        [TestCase( "('13123' + '312' / '13123') / '3' * '7' / '8' * '9' / '131230'" )]
        [TestCase( "Cos('13123') + Sin('312')" )]
        [TestCase( "Cos('13123') + Sin('13123') * Cos('3')" )]
        [TestCase( "Cos('13123') + Sin('13123') * Cos('3') - Sin(4)" )]
        [TestCase( "Cos('13123') + Sin('13123') * Cos('3') - Sin(4) / Cos(6)" )]
        [TestCase( "Cos('13123') + Sin('13123') * Cos('3') - Sin(4) / Cos(6) ^ Sin(8)" )]
        [TestCase( "Cos('13123') + Sin('13123') + Cos('3') ^ Sin(9) = Cos('13123')" )]
        [TestCase( "(Cos('13123') + Sin('13123'))" )]
        [TestCase( "(Cos('13123') + Sin('13123')) * Cos('3')" )]
        [TestCase( "Cos('13123') + Sin('13123') * (Cos('3') - Sin(4))" )]
        [TestCase( "Cos('13123') + (Sin('13123') * Cos('3') - Sin(4)) / Cos(6)" )]
        [TestCase( "Cos('13123') + (Sin('13123') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)" )]
        [TestCase( "Cos('13123') + Sin('13123') + (Cos('3') ^ Sin(9)) = Cos('13123')" )]
        [TestCase( "Cos(9) - Sin('13123') + Cos('13123') * (Cos('3') - Cos(4))" )]
        [TestCase( "Cos('13123') + (Cos('13123') * Sin('3') - Cos(4)) / Sin(6)" )]
        [TestCase( "Cos('13123') - (Sin('13123') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)" )]
        [TestCase( "Cos('13123') + Sin('13123') + (Cos('3') ^ Sin(9)) = Cos('13123')" )]
        [TestCase( "(Cos('13123') + Sin('13123') / Cos('13123')) / Sin('3') * Cos(7)" )]
        [TestCase( "(Cos('13123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8)" )]
        [TestCase( "(Cos('123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8) * Cos(9)" )]
        [TestCase( "(Cos('123') + Sin('123') / Cos('123')) / Cos('3') * Cos(7) / Cos(8) * Cos(9) / Cos(10)" )]
        [TestCase( "1312.1278 + 312.0312" )]
        [TestCase( "1312.1278 + 312.0312 * 3" )]
        [TestCase( "1312.1278 + 312.0312 * 3 - 4" )]
        [TestCase( "1312.1278 + 312.0312 * 3 - 4 / 6" )]
        [TestCase( "1312.1278 + 312.0312 * 3 - 4 / 6 ^ 2" )]
        [TestCase( "1312.1278 + 312.0312 + 3 ^ 9 = 1312.1278" )]
        [TestCase( "(1312.1278 + 312.0312)" )]
        [TestCase( "(1312.1278 + 312.0312) * 3" )]
        [TestCase( "1312.1278 + 312.0312 * (3 - 4)" )]
        [TestCase( "1312.1278 + (312.0312 * 3 - 4) / 6" )]
        [TestCase( "1312.1278 + (312.0312 * 3 - 4 / 6) ^ 2" )]
        [TestCase( "1312.1278 + 312.0312 + (3 ^ 9) = 1312.1278" )]
        [TestCase( "9 - 1312.1278 + 312.0312 * (3 - 4)" )]
        [TestCase( "1312.1278 + (312.0312 * 3 - 4) / 6" )]
        [TestCase( "1312.1278 - (312.0312 * 3 - 4 / 6) ^ 2" )]
        [TestCase( "1312.1278 + 312.0312 + (3 ^ 9) = 1312.1278" )]
        [TestCase( "(1312.1278 + 312.0312 / 1312.1278) / 3 * 7" )]
        [TestCase( "(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / 8" )]
        [TestCase( "(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / 8 * 9" )]
        [TestCase( "(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / 8 * 9 / 1312.12780" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) * Cos(3)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) * Cos(3) - Sin(4)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) * Cos(3) - Sin(4) / Cos(6)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) * Cos(3) - Sin(4) / Cos(6) ^ Sin(8)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) + Cos(3) ^ Sin(9) = Cos(1312.1278)" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312))" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312)) * Cos(3)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) * (Cos(3) - Sin(4))" )]
        [TestCase( "Cos(1312.1278) + (Sin(312.0312) * Cos(3) - Sin(4)) / Cos(6)" )]
        [TestCase( "Cos(1312.1278) + (Sin(312.0312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) + (Cos(3) ^ Sin(9)) = Cos(1312.1278)" )]
        [TestCase( "Cos(9) - Sin(1312.1278) + Cos(312.0312) * (Cos(3) - Cos(4))" )]
        [TestCase( "Cos(1312.1278) + (Cos(312.0312) * Sin(3) - Cos(4)) / Sin(6)" )]
        [TestCase( "Cos(1312.1278) - (Sin(312.0312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) + (Cos(3) ^ Sin(9)) = Cos(1312.1278)" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin(3) * Cos(7)" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin(3) * Cos(7) / Sin(8)" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin(3) * Cos(7) / Sin(8) * Cos(9)" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Cos(3) * Cos(7) / Cos(8) * Cos(9) / Cos(1312.12780)" )]
        [TestCase( "'1312.1278' + '312.0312'" )]
        [TestCase( "'1312.1278' + '312.0312' * '3'" )]
        [TestCase( "'1312.1278' + '312.0312' * '3' - '4'" )]
        [TestCase( "'1312.1278' + '312.0312' * '3' - '4' / '6'" )]
        [TestCase( "'1312.1278' + '312.0312' * '3' - '4' / '6' ^ '8'" )]
        [TestCase( "'1312.1278' + '312.0312' + '3' ^ '9' = '1312.1278'" )]
        [TestCase( "('1312.1278' + '312.0312')" )]
        [TestCase( "('1312.1278' + '312.0312') * '3'" )]
        [TestCase( "'1312.1278' + '312.0312' * ('3' - '4')" )]
        [TestCase( "'1312.1278' + ('312.0312' * '3' - '4') / '6'" )]
        [TestCase( "'1312.1278' + ('312.0312' * '3' - '4' / '6') ^ '8'" )]
        [TestCase( "'1312.1278' + '312.0312' + ('3' ^ '9') = '1312.1278'" )]
        [TestCase( "'9' - '1312.1278' + '312.0312' * ('3' - '4')" )]
        [TestCase( "'1312.1278' + ('312.0312' * '3' - '4') / '6'" )]
        [TestCase( "'1312.1278' - ('312.0312' * '3' - '4' / '6') ^ '8'" )]
        [TestCase( "'1312.1278' + '312.0312' + ('3' ^ '9') = '1312.1278'" )]
        [TestCase( "('1312.1278' + '312.0312' / '1312.1278') / '3' * '7'" )]
        [TestCase( "('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8'" )]
        [TestCase( "('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8' * '9'" )]
        [TestCase( "('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8' * '9' / '1312.12780'" )]
        [TestCase( "Cos('1312.1278') + Sin('312.0312')" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') * Cos('3')" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4)" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6) ^ Sin(8)" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') + Cos('3') ^ Sin(9) = Cos('1312.1278')" )]
        [TestCase( "(Cos('1312.1278') + Sin('1312.1278'))" )]
        [TestCase( "(Cos('1312.1278') + Sin('1312.1278')) * Cos('3')" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') * (Cos('3') - Sin(4))" )]
        [TestCase( "Cos('1312.1278') + (Sin('1312.1278') * Cos('3') - Sin(4)) / Cos(6)" )]
        [TestCase( "Cos('1312.1278') + (Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') + (Cos('3') ^ Sin(9)) = Cos('1312.1278')" )]
        [TestCase( "Cos(9) - Sin('1312.1278') + Cos('1312.1278') * (Cos('3') - Cos(4))" )]
        [TestCase( "Cos('1312.1278') + (Cos('1312.1278') * Sin('3') - Cos(4)) / Sin(6)" )]
        [TestCase( "Cos('1312.1278') - (Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') + (Cos('3') ^ Sin(9)) = Cos('1312.1278')" )]
        [TestCase( "(Cos('1312.1278') + Sin('1312.1278') / Cos('1312.1278')) / Sin('3') * Cos(7)" )]
        [TestCase( "(Cos('1312.1278') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8)" )]
        [TestCase( "(Cos('123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8) * Cos(9)" )]
        [TestCase( "(Cos('123') + Sin('123') / Cos('123')) / Cos('3') * Cos(7) / Cos(8) * Cos(9) / Cos(10)" )]
        [TestCase( "123 ^ 2" )]
        [TestCase( "SetError(-1, -2, -1)" )]
        [TestCase( "SetError(-1, 0, -1)" )]
        [TestCase( "SetError(1, -1, 1)" )]
        [TestCase( "'-13123' + '--312' * '-+3'" )]
        [TestCase( "'-13123' + '-312' * '-3' - '--4'" )]
        [TestCase( "'-++-13123' + '-+-312' * '+-+3' - '-4' / '6'" )]
        [TestCase( "'-13123' + '--312' * '-+3'" )]
        [TestCase( "'-13123' + '-312' * '-3' - '--4'" )]
        [TestCase( "-++-13123 + -+-312 * +-+3 - -4 / 6" )]
        public void Test_PrepareOperatorPrecedence( string expression ) {
            var tokenList = new TokenCollection( _lexer.Lex( expression ) );
            var preparedList = _operatorPrecedenceService.PrepareOperatorPrecedence( tokenList );
            var unprepared = GetAu3Result( "f!"+expression, typeof (double) );
            var prepared = GetAu3Result( "f!"+preparedList, typeof (double) );
            Assert.IsTrue( Equals( unprepared, prepared ) );
        }

        [TestCase( "-$a[$a[$a[0]]] + '--312' * '-+3'", "(-$a[$a[$a[0]]] + ('--312' * '-+3'))" )]
        [TestCase( "-++-$a[$a[0]] + -+-312 * +-+3 - -4 / 6", "((-++-$a[$a[0]] + (-+-312 * +-+3)) - (-4 / 6))" )]
        [TestCase( "-$a[$a[$a[0] + 1]] + '--312' * '-+3'", "(-$a[$a[($a[0] + 1)]] + ('--312' * '-+3'))" )]
        [TestCase( "-$a[$a[0] * 3] + '-312' * '-3' - '--4'", "((-$a[($a[0] * 3)] + ('-312' * '-3')) - '--4')" )]
        [TestCase( "-++-$a[$a[0] + 1 * 4] + -+-312 * +-+3 - -4 / 6", "((-++-$a[($a[0] + (1 * 4))] + (-+-312 * +-+3)) - (-4 / 6))" )]
        [TestCase( "-$a[0] + $b[0]", "(-$a[0] + $b[0])" )]
        [TestCase( "-$a[$a[0][0]][$a[0][0]] + '-312' * '-3' - '--4'", "((-$a[$a[0][0]][$a[0][0]] + ('-312' * '-3')) - '--4')" )]
        [TestCase( "-++-$a[$a[0][0]][$a[0][0]] + '-+-312' * '+-+3' - '-4' / '6'", "((-++-$a[$a[0][0]][$a[0][0]] + ('-+-312' * '+-+3')) - ('-4' / '6'))" )]
        [TestCase( "-$a[0][0] + '--312' * '-+3'", "(-$a[0][0] + ('--312' * '-+3'))" )]
        public void Test_PrepareOperatorPrecedence2( string expression, string expected ) {
            expected = expected.Replace( " ", string.Empty );
            var tokenList = new TokenCollection( _lexer.Lex( expression ) );
            var preparedList = _operatorPrecedenceService.PrepareOperatorPrecedence( tokenList );
            var result = preparedList.ToString().Replace( " ", string.Empty ).Replace( "v_", string.Empty );
            Assert.AreEqual( expected, result );
        }

        [TestCase( "$a OR ($b + $c)" )]
        [TestCase( "IsNumber($a) OR IsNumber($b + $c)" )]
        [TestCase( "$a OR ($b + 1) AND $c" )]
        [TestCase( "IsNumber($a) OR IsNumber($b) AND IsNumber($c)" )]
        [TestCase( "$a + $b" )]
        [TestCase( "$a + $b * $c" )]
        [TestCase( "$a + 312 * 3 - 4" )]
        [TestCase( "$a + 312 * 3 - 4 / 6" )]
        [TestCase( "$a + 312 * 3 - 4 / 6 ^ $c" )]
        [TestCase( "$a + 312 + 3 ^ 9 = $a" )]
        [TestCase( "($a + 312)" )]
        [TestCase( "($a + 312) * 3" )]
        [TestCase( "$a + 312 * (3 - 4)" )]
        [TestCase( "$a + (312 * 3 - 4) / 6" )]
        [TestCase( "$a + (312 * 3 - 4 / 6) ^ $c" )]
        [TestCase( "$a + 312 + (3 ^ 9) = $a" )]
        [TestCase( "9 - $a + 312 * (3 - 4)" )]
        [TestCase( "$a + (312 * 3 - 4) / 6" )]
        [TestCase( "$a - (312 * 3 - 4 / 6) ^ $c" )]
        [TestCase( "$a + 312 + (3 ^ 9) = $a" )]
        [TestCase( "($a + 312 / $a) / 3 * 7" )]
        [TestCase( "($a + 312 / $a) / 3 * 7 / $c" )]
        [TestCase( "($a + 312 / $a) / 3 * 7 / $c * 9" )]
        [TestCase( "($a + 312 / $a) / 3 * 7 / $c * 9 / $a" )]
        [TestCase( "Cos($a) + Sin(312)" )]
        [TestCase( "Cos($a) + Sin(312) * Cos($b)" )]
        [TestCase( "Cos($a) + Sin(312) * Cos($b) - Sin(4)" )]
        [TestCase( "Cos($a) + Sin(312) * Cos($b) - Sin(4) / Cos(6)" )]
        [TestCase( "Cos($a) + Sin(312) * Cos($b) - Sin(4) / Cos(6) ^ Sin($c)" )]
        [TestCase( "Cos($a) + Sin(312) + Cos($b) ^ Sin(9) = Cos($a)" )]
        [TestCase( "(Cos($a) + Sin(312))" )]
        [TestCase( "(Cos($a) + Sin(312)) * Cos($b)" )]
        [TestCase( "Cos($a) + Sin(312) * (Cos($b) - Sin(4))" )]
        [TestCase( "Cos($a) + (Sin(312) * Cos($b) - Sin(4)) / Cos(6)" )]
        [TestCase( "Cos($a) + (Sin(312) * Cos($b) - Sin(4) / Cos(6)) ^ Sin($c)" )]
        [TestCase( "Cos($a) + Sin(312) + (Cos($b) ^ Sin(9)) = Cos($a)" )]
        [TestCase( "Cos(9) - Sin($a) + Cos(312) * (Cos($b) - Cos(4))" )]
        [TestCase( "Cos($a) + (Cos(312) * Sin($b) - Cos(4)) / Sin(6)" )]
        [TestCase( "Cos($a) - (Sin(312) * Cos($b) - Sin(4) / Cos(6)) ^ Sin($c)" )]
        [TestCase( "Cos($a) + Sin(312) + (Cos($b) ^ Sin(9)) = Cos($a)" )]
        [TestCase( "(Cos($a) + Sin(312) / Cos($a)) / Sin($b) * Cos(7)" )]
        [TestCase( "(Cos($a) + Sin(312) / Cos($a)) / Sin($b) * Cos(7) / Sin($c)" )]
        [TestCase( "(Cos($a) + Sin(312) / Cos($a)) / Sin($b) * Cos(7) / Sin($c) * Cos(9)" )]
        [TestCase( "(Cos($a) + Sin(312) / Cos($a)) / Cos($b) * Cos(7) / Cos($c) * Cos(9) / Cos($a)" )]
        [TestCase( "'$a' + '312'" )]
        [TestCase( "'$a' + '312' * '3'" )]
        [TestCase( "'$a' + '312' * '3' - '4'" )]
        [TestCase( "'$a' + '312' * '3' - '4' / '6'" )]
        [TestCase( "'$a' + '312' * '3' - '4' / '6' ^ '8'" )]
        [TestCase( "'$a' + '312' + '3' ^ '9' = '$a'" )]
        [TestCase( "('$a' + '312')" )]
        [TestCase( "('$a' + '312') * '3'" )]
        [TestCase( "'$a' + '312' * ('3' - '4')" )]
        [TestCase( "'$a' + ('312' * '3' - '4') / '6'" )]
        [TestCase( "'$a' + ('312' * '3' - '4' / '6') ^ '8'" )]
        [TestCase( "'$a' + '312' + ('3' ^ '9') = '$a'" )]
        [TestCase( "'9' - '$a' + '312' * ('3' - '4')" )]
        [TestCase( "'$a' + ('312' * '3' - '4') / '6'" )]
        [TestCase( "'$a' - ('312' * '3' - '4' / '6') ^ '8'" )]
        [TestCase( "'$a' + '312' + ('3' ^ '9') = '$a'" )]
        [TestCase( "('$a' + '312' / '$a') / '3' * '7'" )]
        [TestCase( "('$a' + '312' / '$a') / '3' * '7' / '8'" )]
        [TestCase( "('$a' + '312' / '$a') / '3' * '7' / '8' * '9'" )]
        [TestCase( "('$a' + '312' / '$a') / '3' * '7' / '8' * '9' / '$a'" )]
        [TestCase( "Cos('$a') + Sin('312')" )]
        [TestCase( "Cos('$a') + Sin('$a') * Cos('3')" )]
        [TestCase( "Cos('$a') + Sin('$a') * Cos('3') - Sin(4)" )]
        [TestCase( "Cos('$a') + Sin('$a') * Cos('3') - Sin(4) / Cos(6)" )]
        [TestCase( "Cos('$a') + Sin('$a') * Cos('3') - Sin(4) / Cos(6) ^ Sin($c)" )]
        [TestCase( "Cos('$a') + Sin('$a') + Cos('3') ^ Sin(9) = Cos('$a')" )]
        [TestCase( "(Cos('$a') + Sin('$a'))" )]
        [TestCase( "(Cos('$a') + Sin('$a')) * Cos('3')" )]
        [TestCase( "Cos('$a') + Sin('$a') * (Cos('3') - Sin(4))" )]
        [TestCase( "Cos('$a') + (Sin('$a') * Cos('3') - Sin(4)) / Cos(6)" )]
        [TestCase( "Cos('$a') + (Sin('$a') * Cos('3') - Sin(4) / Cos(6)) ^ Sin($c)" )]
        [TestCase( "Cos('$a') + Sin('$a') + (Cos('3') ^ Sin(9)) = Cos('$a')" )]
        [TestCase( "Cos(9) - Sin('$a') + Cos('$a') * (Cos('3') - Cos(4))" )]
        [TestCase( "Cos('$a') + (Cos('$a') * Sin('3') - Cos(4)) / Sin(6)" )]
        [TestCase( "Cos('$a') - (Sin('$a') * Cos('3') - Sin(4) / Cos(6)) ^ Sin($c)" )]
        [TestCase( "Cos('$a') + Sin('$a') + (Cos('3') ^ Sin(9)) = Cos('$a')" )]
        [TestCase( "(Cos('$a') + Sin('$a') / Cos('$a')) / Sin('3') * Cos(7)" )]
        [TestCase( "(Cos('$a') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin($c)" )]
        [TestCase( "(Cos('123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin($c) * Cos(9)" )]
        [TestCase( "(Cos('123') + Sin('123') / Cos('123')) / Cos('3') * Cos(7) / Cos($c) * Cos(9) / Cos(10)" )]
        [TestCase( "1312.1278 + 312.0312" )]
        [TestCase( "1312.1278 + 312.0312 * 3" )]
        [TestCase( "1312.1278 + 312.0312 * 3 - 4" )]
        [TestCase( "1312.1278 + 312.0312 * 3 - 4 / 6" )]
        [TestCase( "1312.1278 + 312.0312 * 3 - 4 / 6 ^ $c" )]
        [TestCase( "1312.1278 + 312.0312 + 3 ^ 9 = 1312.1278" )]
        [TestCase( "(1312.1278 + 312.0312)" )]
        [TestCase( "(1312.1278 + 312.0312) * 3" )]
        [TestCase( "1312.1278 + 312.0312 * (3 - 4)" )]
        [TestCase( "1312.1278 + (312.0312 * 3 - 4) / 6" )]
        [TestCase( "1312.1278 + (312.0312 * 3 - 4 / 6) ^ $c" )]
        [TestCase( "1312.1278 + 312.0312 + (3 ^ 9) = 1312.1278" )]
        [TestCase( "9 - 1312.1278 + 312.0312 * (3 - 4)" )]
        [TestCase( "1312.1278 + (312.0312 * 3 - 4) / 6" )]
        [TestCase( "1312.1278 - (312.0312 * 3 - 4 / 6) ^ $c" )]
        [TestCase( "1312.1278 + 312.0312 + (3 ^ 9) = 1312.1278" )]
        [TestCase( "(1312.1278 + 312.0312 / 1312.1278) / 3 * 7" )]
        [TestCase( "(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / $c" )]
        [TestCase( "(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / $c * 9" )]
        [TestCase( "(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / $c * 9 / 1312.12780" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) * Cos($b)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) * Cos($b) - Sin(4)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) * Cos($b) - Sin(4) / Cos(6)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) * Cos($b) - Sin(4) / Cos(6) ^ Sin($c)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) + Cos($b) ^ Sin(9) = Cos(1312.1278)" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312))" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312)) * Cos($b)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) * (Cos($b) - Sin(4))" )]
        [TestCase( "Cos(1312.1278) + (Sin(312.0312) * Cos($b) - Sin(4)) / Cos(6)" )]
        [TestCase( "Cos(1312.1278) + (Sin(312.0312) * Cos($b) - Sin(4) / Cos(6)) ^ Sin($c)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) + (Cos($b) ^ Sin(9)) = Cos(1312.1278)" )]
        [TestCase( "Cos(9) - Sin(1312.1278) + Cos(312.0312) * (Cos($b) - Cos(4))" )]
        [TestCase( "Cos(1312.1278) + (Cos(312.0312) * Sin($b) - Cos(4)) / Sin(6)" )]
        [TestCase( "Cos(1312.1278) - (Sin(312.0312) * Cos($b) - Sin(4) / Cos(6)) ^ Sin($c)" )]
        [TestCase( "Cos(1312.1278) + Sin(312.0312) + (Cos($b) ^ Sin(9)) = Cos(1312.1278)" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin($b) * Cos(7)" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin($b) * Cos(7) / Sin($c)" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin($b) * Cos(7) / Sin($c) * Cos(9)" )]
        [TestCase( "(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Cos($b) * Cos(7) / Cos($c) * Cos(9) / Cos(1312.12780)" )]
        [TestCase( "'1312.1278' + '312.0312'" )]
        [TestCase( "'1312.1278' + '312.0312' * '3'" )]
        [TestCase( "'1312.1278' + '312.0312' * '3' - '4'" )]
        [TestCase( "'1312.1278' + '312.0312' * '3' - '4' / '6'" )]
        [TestCase( "'1312.1278' + '312.0312' * '3' - '4' / '6' ^ '8'" )]
        [TestCase( "'1312.1278' + '312.0312' + '3' ^ '9' = '1312.1278'" )]
        [TestCase( "('1312.1278' + '312.0312')" )]
        [TestCase( "('1312.1278' + '312.0312') * '3'" )]
        [TestCase( "'1312.1278' + '312.0312' * ('3' - '4')" )]
        [TestCase( "'1312.1278' + ('312.0312' * '3' - '4') / '6'" )]
        [TestCase( "'1312.1278' + ('312.0312' * '3' - '4' / '6') ^ '8'" )]
        [TestCase( "'1312.1278' + '312.0312' + ('3' ^ '9') = '1312.1278'" )]
        [TestCase( "'9' - '1312.1278' + '312.0312' * ('3' - '4')" )]
        [TestCase( "'1312.1278' + ('312.0312' * '3' - '4') / '6'" )]
        [TestCase( "'1312.1278' - ('312.0312' * '3' - '4' / '6') ^ '8'" )]
        [TestCase( "'1312.1278' + '312.0312' + ('3' ^ '9') = '1312.1278'" )]
        [TestCase( "('1312.1278' + '312.0312' / '1312.1278') / '3' * '7'" )]
        [TestCase( "('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8'" )]
        [TestCase( "('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8' * '9'" )]
        [TestCase( "('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8' * '9' / '1312.12780'" )]
        [TestCase( "Cos('1312.1278') + Sin('312.0312')" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') * Cos('3')" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4)" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6) ^ Sin($c)" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') + Cos('3') ^ Sin(9) = Cos('1312.1278')" )]
        [TestCase( "(Cos('1312.1278') + Sin('1312.1278'))" )]
        [TestCase( "(Cos('1312.1278') + Sin('1312.1278')) * Cos('3')" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') * (Cos('3') - Sin(4))" )]
        [TestCase( "Cos('1312.1278') + (Sin('1312.1278') * Cos('3') - Sin(4)) / Cos(6)" )]
        [TestCase( "Cos('1312.1278') + (Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)) ^ Sin($c)" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') + (Cos('3') ^ Sin(9)) = Cos('1312.1278')" )]
        [TestCase( "Cos(9) - Sin('1312.1278') + Cos('1312.1278') * (Cos('3') - Cos(4))" )]
        [TestCase( "Cos('1312.1278') + (Cos('1312.1278') * Sin('3') - Cos(4)) / Sin(6)" )]
        [TestCase( "Cos('1312.1278') - (Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)) ^ Sin($c)" )]
        [TestCase( "Cos('1312.1278') + Sin('1312.1278') + (Cos('3') ^ Sin(9)) = Cos('1312.1278')" )]
        [TestCase( "(Cos('1312.1278') + Sin('1312.1278') / Cos('1312.1278')) / Sin('3') * Cos(7)" )]
        [TestCase( "(Cos('1312.1278') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin($c)" )]
        [TestCase( "(Cos('123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin($c) * Cos(9)" )]
        [TestCase( "(Cos('123') + Sin('123') / Cos('123')) / Cos('3') * Cos(7) / Cos($c) * Cos(9) / Cos(10)" )]
        [TestCase( "123 ^ 2" )]
        [TestCase( "SetError(-1, -2, -1)" )]
        [TestCase( "SetError(-1, 0, -1)" )]
        [TestCase( "SetError(1, -1, 1)" )]
        [TestCase( "'-$a' + '--312' * '-+3'" )]
        [TestCase( "'-$a' + '-312' * '-3' - '--4'" )]
        [TestCase( "'-++-$a' + '-+-312' * '+-+3' - '-4' / '6'" )]
        [TestCase( "'-$a' + '--312' * '-+3'" )]
        [TestCase( "'-$a' + '-312' * '-3' - '--4'" )]
        [TestCase( "-++-$a + -+-312 * +-+3 - -4 / 6" )]
        [TestCase( "-1 - -1 - -8 + -1" )]
        [TestCase( "--(-(123 + 321) / 4 * 9) + - 1" )]
        public void Test_PrepareOperatorPrecedenceVariables( string expression ) {
            var random = new Random();
            var a = random.Next( 0, 1337 );
            var b = random.Next( 0, 1337 );
            var c = random.Next( 0, 1337 );

            var tokenList = new TokenCollection( _lexer.Lex( expression ) );

            var preparedList = _operatorPrecedenceService.PrepareOperatorPrecedence( tokenList );
            var expression1 =
                expression.Replace( "$a", a.ToString( CultureInfo.InvariantCulture ) )
                    .Replace( "$b", b.ToString( CultureInfo.InvariantCulture ) )
                    .Replace( "$c", c.ToString( CultureInfo.InvariantCulture ) );
            var preparedList1 =
                preparedList.ToString()
                    .Replace( "$v_a", a.ToString( CultureInfo.InvariantCulture ) )
                    .Replace( "$v_b", b.ToString( CultureInfo.InvariantCulture ) )
                    .Replace( "$v_c", c.ToString( CultureInfo.InvariantCulture ) )
                    .Replace( "$a", a.ToString( CultureInfo.InvariantCulture ) )
                    .Replace( "$b", b.ToString( CultureInfo.InvariantCulture ) )
                    .Replace( "$c", c.ToString( CultureInfo.InvariantCulture ) );

            var unprepared = GetAu3Result( "f!"+expression1, typeof (double) );
            var prepared = GetAu3Result( "f!"+preparedList1, typeof (double) );
            Assert.IsTrue( Equals( unprepared, prepared ) );
        }
    }
}
