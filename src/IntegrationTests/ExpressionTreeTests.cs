﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoJIT;
using AutoJIT.Compiler;
using AutoJITRuntime;
using Lawl.Architekture;
using Lawl.Reflection;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace UnitTests
{
    public class ExpressionTreeTests : AutoitFunctionTestBase
    {
        private readonly ICompiler _compiler;
        private readonly string _scriptTemplate = string.Format( "Func ExpressionReturner(){0}Return {{0}}{0}Endfunc{0}", Environment.NewLine );
        private readonly string _scriptTemplateWithParameters = string.Format( "Func ExpressionReturner($a, $b, $c){0}Return {{0}}{0}Endfunc{0}", Environment.NewLine );

        public ExpressionTreeTests() {
            var componentContainer = new CompilerBootStrapper();
            _compiler = componentContainer.GetInstance<ICompiler>();
        }

        [TestCase("NOT 1")]
        [TestCase("1 OR (0 + 1)")]
        [TestCase("IsNumber(1) OR IsNumber('a123a')")]
        [TestCase("IsNumber('1') OR IsNumber('123a')")]
        [TestCase("IsNumber(1) OR IsNumber('a123a')")]
        [TestCase("1 OR (0 + 1) AND 2")]
        [TestCase("IsNumber(1) OR IsNumber('a123a') AND IsNumber('a')")]
        [TestCase("IsNumber('1') OR IsNumber('123a') AND IsNumber('2')")]
        [TestCase("IsNumber(1) OR IsNumber('a123a') OR IsNumber(123)")]
        [TestCase("13123 + 312")]
        [TestCase("13123 + 312 * 3")]
        [TestCase("13123 + 312 * 3 - 4")]
        [TestCase("13123 + 312 * 3 - 4 / 6")]
        [TestCase("13123 + 312 * 3 - 4 / 6 ^ 2")]
        [TestCase("13123 + 312 + 3 ^ 2 = 13123")]
        [TestCase("(13123 + 312)")]
        [TestCase("(13123 + 312) * 3")]
        [TestCase("13123 + 312 * (3 - 4)")]
        [TestCase("13123 + (312 * 3 - 4) / 6")]
        [TestCase("13123 + (312 * 3 - 4 / 6) ^ 2")]
        [TestCase("13123 + 312 + (3 ^ 2) = 13123")]
        [TestCase("9 - 13123 + 312 * (3 - 4)")]
        [TestCase("13123 + (312 * 3 - 4) / 6")]
        [TestCase("13123 - (312 * 3 - 4 / 6) ^ 2")]
        [TestCase("13123 + 312 + (3 ^ 2) = 13123")]
        [TestCase("(13123 + 312 / 13123) / 3 * 7")]
        [TestCase("(13123 + 312 / 13123) / 3 * 7 / 8")]
        [TestCase("(13123 + 312 / 13123) / 3 * 7 / 8 * 9")]
        [TestCase("(13123 + 312 / 13123) / 3 * 7 / 8 * 9 / 131230")]
        [TestCase("Cos(13123) + Sin(312)")]
        [TestCase("Cos(13123) + Sin(312) * Cos(3)")]
        [TestCase("Cos(13123) + Sin(312) * Cos(3) - Sin(4)")]
        [TestCase("Cos(13123) + Sin(312) * Cos(3) - Sin(4) / Cos(6)")]
        [TestCase("Cos(13123) + Sin(312) * Cos(3) - Sin(4) / Cos(6) ^ Sin(8)")]
        [TestCase("Cos(13123) + Sin(312) + Cos(3) ^ Sin(9) = Cos(13123)")]
        [TestCase("(Cos(13123) + Sin(312))")]
        [TestCase("(Cos(13123) + Sin(312)) * Cos(3)")]
        [TestCase("Cos(13123) + Sin(312) * (Cos(3) - Sin(4))")]
        [TestCase("Cos(13123) + (Sin(312) * Cos(3) - Sin(4)) / Cos(6)")]
        [TestCase("Cos(13123) + (Sin(312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos(13123) + Sin(312) + (Cos(3) ^ Sin(9)) = Cos(13123)")]
        [TestCase("Cos(9) - Sin(13123) + Cos(312) * (Cos(3) - Cos(4))")]
        [TestCase("Cos(13123) + (Cos(312) * Sin(3) - Cos(4)) / Sin(6)")]
        [TestCase("Cos(13123) - (Sin(312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos(13123) + Sin(312) + (Cos(3) ^ Sin(9)) = Cos(13123)")]
        [TestCase("(Cos(13123) + Sin(312) / Cos(13123)) / Sin(3) * Cos(7)")]
        [TestCase("(Cos(13123) + Sin(312) / Cos(13123)) / Sin(3) * Cos(7) / Sin(8)")]
        [TestCase("(Cos(13123) + Sin(312) / Cos(13123)) / Sin(3) * Cos(7) / Sin(8) * Cos(9)")]
        [TestCase("(Cos(13123) + Sin(312) / Cos(13123)) / Cos(3) * Cos(7) / Cos(8) * Cos(9) / Cos(131230)")]
        [TestCase("'13123' + '312'")]
        [TestCase("'13123' + '312' * '3'")]
        [TestCase("'13123' + '312' * '3' - '4'")]
        [TestCase("'13123' + '312' * '3' - '4' / '6'")]
        [TestCase("'13123' + '312' * '3' - '4' / '6' ^ '2'")]
        [TestCase("'13123' + '312' + '3' ^ '9' = '13123'")]
        [TestCase("('13123' + '312')")]
        [TestCase("('13123' + '312') * '3'")]
        [TestCase("'13123' + '312' * ('3' - '4')")]
        [TestCase("'13123' + ('312' * '3' - '4') / '6'")]
        [TestCase("'13123' + ('312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("'13123' + '312' + ('3' ^ '9') = '13123'")]
        [TestCase("'9' - '13123' + '312' * ('3' - '4')")]
        [TestCase("'13123' + ('312' * '3' - '4') / '6'")]
        [TestCase("'13123' - ('312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("'13123' + '312' + ('3' ^ '9') = '13123'")]
        [TestCase("('13123' + '312' / '13123') / '3' * '7'")]
        [TestCase("('13123' + '312' / '13123') / '3' * '7' / '8'")]
        [TestCase("('13123' + '312' / '13123') / '3' * '7' / '8' * '9'")]
        [TestCase("('13123' + '312' / '13123') / '3' * '7' / '8' * '9' / '131230'")]
        [TestCase("Cos('13123') + Sin('312')")]
        [TestCase("Cos('13123') + Sin('13123') * Cos('3')")]
        [TestCase("Cos('13123') + Sin('13123') * Cos('3') - Sin(4)")]
        [TestCase("Cos('13123') + Sin('13123') * Cos('3') - Sin(4) / Cos(6)")]
        [TestCase("Cos('13123') + Sin('13123') * Cos('3') - Sin(4) / Cos(6) ^ Sin(8)")]
        [TestCase("Cos('13123') + Sin('13123') + Cos('3') ^ Sin(9) = Cos('13123')")]
        [TestCase("(Cos('13123') + Sin('13123'))")]
        [TestCase("(Cos('13123') + Sin('13123')) * Cos('3')")]
        [TestCase("Cos('13123') + Sin('13123') * (Cos('3') - Sin(4))")]
        [TestCase("Cos('13123') + (Sin('13123') * Cos('3') - Sin(4)) / Cos(6)")]
        [TestCase("Cos('13123') + (Sin('13123') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos('13123') + Sin('13123') + (Cos('3') ^ Sin(9)) = Cos('13123')")]
        [TestCase("Cos(9) - Sin('13123') + Cos('13123') * (Cos('3') - Cos(4))")]
        [TestCase("Cos('13123') + (Cos('13123') * Sin('3') - Cos(4)) / Sin(6)")]
        [TestCase("Cos('13123') - (Sin('13123') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos('13123') + Sin('13123') + (Cos('3') ^ Sin(9)) = Cos('13123')")]
        [TestCase("(Cos('13123') + Sin('13123') / Cos('13123')) / Sin('3') * Cos(7)")]
        [TestCase("(Cos('13123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8) * Cos(9)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Cos('3') * Cos(7) / Cos(8) * Cos(9) / Cos(10)")]
        [TestCase("1312.1278 + 312.0312")]
        [TestCase("1312.1278 + 312.0312 * 3")]
        [TestCase("1312.1278 + 312.0312 * 3 - 4")]
        [TestCase("1312.1278 + 312.0312 * 3 - 4 / 6")]
        [TestCase("1312.1278 + 312.0312 * 3 - 4 / 6 ^ 2")]
        [TestCase("1312.1278 + 312.0312 + 3 ^ 2 = 1312.1278")]
        [TestCase("(1312.1278 + 312.0312)")]
        [TestCase("(1312.1278 + 312.0312) * 3")]
        [TestCase("1312.1278 + 312.0312 * (3 - 4)")]
        [TestCase("1312.1278 + (312.0312 * 3 - 4) / 6")]
        [TestCase("1312.1278 + (312.0312 * 3 - 4 / 6) ^ 2")]
        [TestCase("1312.1278 + 312.0312 + (3 ^ 2) = 1312.1278")]
        [TestCase("9 - 1312.1278 + 312.0312 * (3 - 4)")]
        [TestCase("1312.1278 + (312.0312 * 3 - 4) / 6")]
        [TestCase("1312.1278 - (312.0312 * 3 - 4 / 6) ^ 2")]
        [TestCase("1312.1278 + 312.0312 + (3 ^ 2) = 1312.1278")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / 8")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / 8 * 9")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / 8 * 9 / 1312.12780")]
        [TestCase("Cos(1312.1278) + Sin(312.0312)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos(3)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos(3) - Sin(4)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos(3) - Sin(4) / Cos(6)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos(3) - Sin(4) / Cos(6) ^ Sin(8)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) + Cos(3) ^ Sin(9) = Cos(1312.1278)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312))")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312)) * Cos(3)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * (Cos(3) - Sin(4))")]
        [TestCase("Cos(1312.1278) + (Sin(312.0312) * Cos(3) - Sin(4)) / Cos(6)")]
        [TestCase("Cos(1312.1278) + (Sin(312.0312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) + (Cos(3) ^ Sin(9)) = Cos(1312.1278)")]
        [TestCase("Cos(9) - Sin(1312.1278) + Cos(312.0312) * (Cos(3) - Cos(4))")]
        [TestCase("Cos(1312.1278) + (Cos(312.0312) * Sin(3) - Cos(4)) / Sin(6)")]
        [TestCase("Cos(1312.1278) - (Sin(312.0312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) + (Cos(3) ^ Sin(9)) = Cos(1312.1278)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin(3) * Cos(7)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin(3) * Cos(7) / Sin(8)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin(3) * Cos(7) / Sin(8) * Cos(9)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Cos(3) * Cos(7) / Cos(8) * Cos(9) / Cos(1312.12780)")]
        [TestCase("'1312.1278' + '312.0312'")]
        [TestCase("'1312.1278' + '312.0312' * '3'")]
        [TestCase("'1312.1278' + '312.0312' * '3' - '4'")]
        [TestCase("'1312.1278' + '312.0312' * '3' - '4' / '6'")]
        [TestCase("'1312.1278' + '312.0312' * '3' - '4' / '6' ^ '2'")]
        [TestCase("'1312.1278' + '312.0312' + '3' ^ '9' = '1312.1278'")]
        [TestCase("('1312.1278' + '312.0312')")]
        [TestCase("('1312.1278' + '312.0312') * '3'")]
        [TestCase("'1312.1278' + '312.0312' * ('3' - '4')")]
        [TestCase("'1312.1278' + ('312.0312' * '3' - '4') / '6'")]
        [TestCase("'1312.1278' + ('312.0312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("'1312.1278' + '312.0312' + ('3' ^ '9') = '1312.1278'")]
        [TestCase("'9' - '1312.1278' + '312.0312' * ('3' - '4')")]
        [TestCase("'1312.1278' + ('312.0312' * '3' - '4') / '6'")]
        [TestCase("'1312.1278' - ('312.0312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("'1312.1278' + '312.0312' + ('3' ^ '9') = '1312.1278'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8' * '9'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8' * '9' / '1312.12780'")]
        [TestCase("Cos('1312.1278') + Sin('312.0312')")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3')")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6) ^ Sin(8)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') + Cos('3') ^ Sin(9) = Cos('1312.1278')")]
        [TestCase("(Cos('1312.1278') + Sin('1312.1278'))")]
        [TestCase("(Cos('1312.1278') + Sin('1312.1278')) * Cos('3')")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * (Cos('3') - Sin(4))")]
        [TestCase("Cos('1312.1278') + (Sin('1312.1278') * Cos('3') - Sin(4)) / Cos(6)")]
        [TestCase("Cos('1312.1278') + (Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') + (Cos('3') ^ Sin(9)) = Cos('1312.1278')")]
        [TestCase("Cos(9) - Sin('1312.1278') + Cos('1312.1278') * (Cos('3') - Cos(4))")]
        [TestCase("Cos('1312.1278') + (Cos('1312.1278') * Sin('3') - Cos(4)) / Sin(6)")]
        [TestCase("Cos('1312.1278') - (Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') + (Cos('3') ^ Sin(9)) = Cos('1312.1278')")]
        [TestCase("(Cos('1312.1278') + Sin('1312.1278') / Cos('1312.1278')) / Sin('3') * Cos(7)")]
        [TestCase("(Cos('1312.1278') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8) * Cos(9)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Cos('3') * Cos(7) / Cos(8) * Cos(9) / Cos(10)")]
        [TestCase("123 ^ 2")]
        [TestCase("SetError(-1, -2, -1)")]
        [TestCase("SetError(-1, 0, -1)")]
        [TestCase("SetError(1, -1, 1)")]
        [TestCase("'-13123' + '--312' * '-+3'")]
        [TestCase("'-13123' + '-312' * '-3' - '--4'")]
        [TestCase("'-++-13123' + '-+-312' * '+-+3' - '-4' / '6'")]
        [TestCase("'-13123' + '--312' * '-+3'")]
        [TestCase("'-13123' + '-312' * '-3' - '--4'")]
        [TestCase("-++-13123 + -+-312 * +-+3 - -4 / 6")]
        [TestCase("-1 - -1 - -8 + -1")]
        [TestCase("--(-(123 + 321) / 4 * 9) + - 1")]
        [TestCase("'Hallo' == 'Hallo'")]
        [TestCase("'Hallo' == 'HallO'")]
        [TestCase("'1' == 1")]
        [TestCase("BitXOR(BitAND(1, 2, 3), BitNOT(1), BitXOR(1, 2, 3)) = -2")]
        [TestCase("BitXOR(BitAND('1', 2, 3), BitNOT('1' + 2), BitXOR(1, 2, 3)) = -2")]
        [TestCase("String(BinaryMid(Binary('1ZcstHe5aWa95mCa6ba1'), 4, 5))")]
        [TestCase("IsInt(StringMid('awdawdawdawdawdawdawdawdawdawdawdawdawda', 4, 5))")]
        [TestCase("1 OR ( 2 AND 3 ) OR ( NOT 4 AND NOT 5)")]
        [TestCase("Null = Null")]
        public void Test_Expressions(string expression)
        {
            var script = string.Format( _scriptTemplate, expression );
            var assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
            var assembly = Assembly.Load( assemblyBytes );
            var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
            var method = type.GetMethod( "f_ExpressionReturner" );
            var instance = type.CreateInstanceWithDefaultParameters();
            var result = method.Invoke( instance, null ) as Variant;
            var au3Result = GetAu3Result( string.Format( "f!{0}", expression ), result.GetRealType() );
            CompareResults( result, au3Result );
        }


        [TestCase("(1 * 2) == 0 ? 10 : 100")]
        [TestCase("SetError ( 1 ,  0 ,  - 1 )")]
        [TestCase("@error OR NOT 1")]
        public void Test_ExpressionsTernary(string expression)
        {
            var script = string.Format(_scriptTemplate, expression);
            var assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false);
            File.WriteAllBytes(@"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.dll", assemblyBytes);
            
            var assembly = Assembly.Load(assemblyBytes);
            var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
            var method = type.GetMethod("f_ExpressionReturner");
            var instance = type.CreateInstanceWithDefaultParameters();
            var result = method.Invoke(instance, null) as Variant;
            var au3Result = GetAu3Result(string.Format("f!{0}", expression), result.GetRealType());
            CompareResults(result, au3Result);
        }


        [TestCase("NOT 1")]
        [TestCase("1 OR (0 + 1)")]
        [TestCase("IsNumber(1) OR IsNumber('a123a')")]
        [TestCase("IsNumber('1') OR IsNumber('123a')")]
        [TestCase("IsNumber(1) OR IsNumber('a123a')")]
        [TestCase("1 OR (0 + 1) AND 2")]
        [TestCase("IsNumber(1) OR IsNumber('a123a') AND IsNumber('a')")]
        [TestCase("IsNumber('1') OR IsNumber('123a') AND IsNumber('2')")]
        [TestCase("IsNumber(1) OR IsNumber('a123a') OR IsNumber(123)")]
        [TestCase("13123 + 312")]
        [TestCase("13123 + 312 * 3")]
        [TestCase("13123 + 312 * 3 - 4")]
        [TestCase("13123 + 312 * 3 - 4 / 6")]
        [TestCase("13123 + 312 * 3 - 4 / 6 ^ 2")]
        [TestCase("13123 + 312 + 3 ^ 2 = 13123")]
        [TestCase("(13123 + 312)")]
        [TestCase("(13123 + 312) * 3")]
        [TestCase("13123 + 312 * (3 - 4)")]
        [TestCase("13123 + (312 * 3 - 4) / 6")]
        [TestCase("13123 + (312 * 3 - 4 / 6) ^ 2")]
        [TestCase("13123 + 312 + (3 ^ 2) = 13123")]
        [TestCase("9 - 13123 + 312 * (3 - 4)")]
        [TestCase("13123 + (312 * 3 - 4) / 6")]
        [TestCase("13123 - (312 * 3 - 4 / 6) ^ 2")]
        [TestCase("13123 + 312 + (3 ^ 2) = 13123")]
        [TestCase("(13123 + 312 / 13123) / 3 * 7")]
        [TestCase("(13123 + 312 / 13123) / 3 * 7 / 8")]
        [TestCase("(13123 + 312 / 13123) / 3 * 7 / 8 * 9")]
        [TestCase("(13123 + 312 / 13123) / 3 * 7 / 8 * 9 / 131230")]
        [TestCase("Cos(13123) + Sin(312)")]
        [TestCase("Cos(13123) + Sin(312) * Cos(3)")]
        [TestCase("Cos(13123) + Sin(312) * Cos(3) - Sin(4)")]
        [TestCase("Cos(13123) + Sin(312) * Cos(3) - Sin(4) / Cos(6)")]
        [TestCase("Cos(13123) + Sin(312) * Cos(3) - Sin(4) / Cos(6) ^ Sin(8)")]
        [TestCase("Cos(13123) + Sin(312) + Cos(3) ^ Sin(9) = Cos(13123)")]
        [TestCase("(Cos(13123) + Sin(312))")]
        [TestCase("(Cos(13123) + Sin(312)) * Cos(3)")]
        [TestCase("Cos(13123) + Sin(312) * (Cos(3) - Sin(4))")]
        [TestCase("Cos(13123) + (Sin(312) * Cos(3) - Sin(4)) / Cos(6)")]
        [TestCase("Cos(13123) + (Sin(312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos(13123) + Sin(312) + (Cos(3) ^ Sin(9)) = Cos(13123)")]
        [TestCase("Cos(9) - Sin(13123) + Cos(312) * (Cos(3) - Cos(4))")]
        [TestCase("Cos(13123) + (Cos(312) * Sin(3) - Cos(4)) / Sin(6)")]
        [TestCase("Cos(13123) - (Sin(312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos(13123) + Sin(312) + (Cos(3) ^ Sin(9)) = Cos(13123)")]
        [TestCase("(Cos(13123) + Sin(312) / Cos(13123)) / Sin(3) * Cos(7)")]
        [TestCase("(Cos(13123) + Sin(312) / Cos(13123)) / Sin(3) * Cos(7) / Sin(8)")]
        [TestCase("(Cos(13123) + Sin(312) / Cos(13123)) / Sin(3) * Cos(7) / Sin(8) * Cos(9)")]
        [TestCase("(Cos(13123) + Sin(312) / Cos(13123)) / Cos(3) * Cos(7) / Cos(8) * Cos(9) / Cos(131230)")]
        [TestCase("'13123' + '312'")]
        [TestCase("'13123' + '312' * '3'")]
        [TestCase("'13123' + '312' * '3' - '4'")]
        [TestCase("'13123' + '312' * '3' - '4' / '6'")]
        [TestCase("'13123' + '312' * '3' - '4' / '6' ^ '2'")]
        [TestCase("'13123' + '312' + '3' ^ '9' = '13123'")]
        [TestCase("('13123' + '312')")]
        [TestCase("('13123' + '312') * '3'")]
        [TestCase("'13123' + '312' * ('3' - '4')")]
        [TestCase("'13123' + ('312' * '3' - '4') / '6'")]
        [TestCase("'13123' + ('312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("'13123' + '312' + ('3' ^ '9') = '13123'")]
        [TestCase("'9' - '13123' + '312' * ('3' - '4')")]
        [TestCase("'13123' + ('312' * '3' - '4') / '6'")]
        [TestCase("'13123' - ('312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("'13123' + '312' + ('3' ^ '9') = '13123'")]
        [TestCase("('13123' + '312' / '13123') / '3' * '7'")]
        [TestCase("('13123' + '312' / '13123') / '3' * '7' / '8'")]
        [TestCase("('13123' + '312' / '13123') / '3' * '7' / '8' * '9'")]
        [TestCase("('13123' + '312' / '13123') / '3' * '7' / '8' * '9' / '131230'")]
        [TestCase("Cos('13123') + Sin('312')")]
        [TestCase("Cos('13123') + Sin('13123') * Cos('3')")]
        [TestCase("Cos('13123') + Sin('13123') * Cos('3') - Sin(4)")]
        [TestCase("Cos('13123') + Sin('13123') * Cos('3') - Sin(4) / Cos(6)")]
        [TestCase("Cos('13123') + Sin('13123') * Cos('3') - Sin(4) / Cos(6) ^ Sin(8)")]
        [TestCase("Cos('13123') + Sin('13123') + Cos('3') ^ Sin(9) = Cos('13123')")]
        [TestCase("(Cos('13123') + Sin('13123'))")]
        [TestCase("(Cos('13123') + Sin('13123')) * Cos('3')")]
        [TestCase("Cos('13123') + Sin('13123') * (Cos('3') - Sin(4))")]
        [TestCase("Cos('13123') + (Sin('13123') * Cos('3') - Sin(4)) / Cos(6)")]
        [TestCase("Cos('13123') + (Sin('13123') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos('13123') + Sin('13123') + (Cos('3') ^ Sin(9)) = Cos('13123')")]
        [TestCase("Cos(9) - Sin('13123') + Cos('13123') * (Cos('3') - Cos(4))")]
        [TestCase("Cos('13123') + (Cos('13123') * Sin('3') - Cos(4)) / Sin(6)")]
        [TestCase("Cos('13123') - (Sin('13123') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos('13123') + Sin('13123') + (Cos('3') ^ Sin(9)) = Cos('13123')")]
        [TestCase("(Cos('13123') + Sin('13123') / Cos('13123')) / Sin('3') * Cos(7)")]
        [TestCase("(Cos('13123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8) * Cos(9)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Cos('3') * Cos(7) / Cos(8) * Cos(9) / Cos(10)")]
        [TestCase("1312.1278 + 312.0312")]
        [TestCase("1312.1278 + 312.0312 * 3")]
        [TestCase("1312.1278 + 312.0312 * 3 - 4")]
        [TestCase("1312.1278 + 312.0312 * 3 - 4 / 6")]
        [TestCase("1312.1278 + 312.0312 * 3 - 4 / 6 ^ 2")]
        [TestCase("1312.1278 + 312.0312 + 3 ^ 2 = 1312.1278")]
        [TestCase("(1312.1278 + 312.0312)")]
        [TestCase("(1312.1278 + 312.0312) * 3")]
        [TestCase("1312.1278 + 312.0312 * (3 - 4)")]
        [TestCase("1312.1278 + (312.0312 * 3 - 4) / 6")]
        [TestCase("1312.1278 + (312.0312 * 3 - 4 / 6) ^ 2")]
        [TestCase("1312.1278 + 312.0312 + (3 ^ 2) = 1312.1278")]
        [TestCase("9 - 1312.1278 + 312.0312 * (3 - 4)")]
        [TestCase("1312.1278 + (312.0312 * 3 - 4) / 6")]
        [TestCase("1312.1278 - (312.0312 * 3 - 4 / 6) ^ 2")]
        [TestCase("1312.1278 + 312.0312 + (3 ^ 2) = 1312.1278")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / 8")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / 8 * 9")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / 8 * 9 / 1312.12780")]
        [TestCase("Cos(1312.1278) + Sin(312.0312)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos(3)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos(3) - Sin(4)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos(3) - Sin(4) / Cos(6)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos(3) - Sin(4) / Cos(6) ^ Sin(8)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) + Cos(3) ^ Sin(9) = Cos(1312.1278)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312))")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312)) * Cos(3)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * (Cos(3) - Sin(4))")]
        [TestCase("Cos(1312.1278) + (Sin(312.0312) * Cos(3) - Sin(4)) / Cos(6)")]
        [TestCase("Cos(1312.1278) + (Sin(312.0312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) + (Cos(3) ^ Sin(9)) = Cos(1312.1278)")]
        [TestCase("Cos(9) - Sin(1312.1278) + Cos(312.0312) * (Cos(3) - Cos(4))")]
        [TestCase("Cos(1312.1278) + (Cos(312.0312) * Sin(3) - Cos(4)) / Sin(6)")]
        [TestCase("Cos(1312.1278) - (Sin(312.0312) * Cos(3) - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) + (Cos(3) ^ Sin(9)) = Cos(1312.1278)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin(3) * Cos(7)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin(3) * Cos(7) / Sin(8)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin(3) * Cos(7) / Sin(8) * Cos(9)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Cos(3) * Cos(7) / Cos(8) * Cos(9) / Cos(1312.12780)")]
        [TestCase("'1312.1278' + '312.0312'")]
        [TestCase("'1312.1278' + '312.0312' * '3'")]
        [TestCase("'1312.1278' + '312.0312' * '3' - '4'")]
        [TestCase("'1312.1278' + '312.0312' * '3' - '4' / '6'")]
        [TestCase("'1312.1278' + '312.0312' * '3' - '4' / '6' ^ '2'")]
        [TestCase("'1312.1278' + '312.0312' + '3' ^ '9' = '1312.1278'")]
        [TestCase("('1312.1278' + '312.0312')")]
        [TestCase("('1312.1278' + '312.0312') * '3'")]
        [TestCase("'1312.1278' + '312.0312' * ('3' - '4')")]
        [TestCase("'1312.1278' + ('312.0312' * '3' - '4') / '6'")]
        [TestCase("'1312.1278' + ('312.0312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("'1312.1278' + '312.0312' + ('3' ^ '9') = '1312.1278'")]
        [TestCase("'9' - '1312.1278' + '312.0312' * ('3' - '4')")]
        [TestCase("'1312.1278' + ('312.0312' * '3' - '4') / '6'")]
        [TestCase("'1312.1278' - ('312.0312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("'1312.1278' + '312.0312' + ('3' ^ '9') = '1312.1278'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8' * '9'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8' * '9' / '1312.12780'")]
        [TestCase("Cos('1312.1278') + Sin('312.0312')")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3')")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6) ^ Sin(8)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') + Cos('3') ^ Sin(9) = Cos('1312.1278')")]
        [TestCase("(Cos('1312.1278') + Sin('1312.1278'))")]
        [TestCase("(Cos('1312.1278') + Sin('1312.1278')) * Cos('3')")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * (Cos('3') - Sin(4))")]
        [TestCase("Cos('1312.1278') + (Sin('1312.1278') * Cos('3') - Sin(4)) / Cos(6)")]
        [TestCase("Cos('1312.1278') + (Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') + (Cos('3') ^ Sin(9)) = Cos('1312.1278')")]
        [TestCase("Cos(9) - Sin('1312.1278') + Cos('1312.1278') * (Cos('3') - Cos(4))")]
        [TestCase("Cos('1312.1278') + (Cos('1312.1278') * Sin('3') - Cos(4)) / Sin(6)")]
        [TestCase("Cos('1312.1278') - (Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)) ^ Sin(8)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') + (Cos('3') ^ Sin(9)) = Cos('1312.1278')")]
        [TestCase("(Cos('1312.1278') + Sin('1312.1278') / Cos('1312.1278')) / Sin('3') * Cos(7)")]
        [TestCase("(Cos('1312.1278') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin(8) * Cos(9)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Cos('3') * Cos(7) / Cos(8) * Cos(9) / Cos(10)")]
        [TestCase("123 ^ 2")]
        [TestCase("SetError(-1, -2, -1)")]
        [TestCase("SetError(-1, 0, -1)")]
        [TestCase("SetError(1, -1, 1)")]
        [TestCase("'-13123' + '--312' * '-+3'")]
        [TestCase("'-13123' + '-312' * '-3' - '--4'")]
        [TestCase("'-++-13123' + '-+-312' * '+-+3' - '-4' / '6'")]
        [TestCase("'-13123' + '--312' * '-+3'")]
        [TestCase("'-13123' + '-312' * '-3' - '--4'")]
        [TestCase("-++-13123 + -+-312 * +-+3 - -4 / 6")]
        [TestCase("-1 - -1 - -8 + -1")]
        [TestCase("--(-(123 + 321) / 4 * 9) + - 1")]
        [TestCase("String(BinaryMid(Binary('1ZcstHe5aWa95mCa6ba1'), 4, 5))")]
        public void Test_ExpressionsOptimizing(string expression)
        {
            var script = string.Format(_scriptTemplate, expression);
            var assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false, true);
            var assembly = Assembly.Load(assemblyBytes);
            var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
            var method = type.GetMethod("f_ExpressionReturner");
            var instance = type.CreateInstanceWithDefaultParameters();
            var result = method.Invoke(instance, null) as Variant;
            var au3Result = GetAu3Result(string.Format("f!{0}", expression), result.GetRealType());
            CompareResults(result, au3Result);
        }



        [TestCase("$a OR ($b + $c)")]
        [TestCase("IsNumber($a) OR IsNumber($b + $c)")]
        [TestCase("$a OR ($b + 1) AND $c")]
        [TestCase("IsNumber($a) OR IsNumber($b) AND IsNumber($c)")]
        [TestCase("$a + $b")]
        [TestCase("$a + $b * $c")]
        [TestCase("$a + 312 * 3 - 4")]
        [TestCase("$a + 312 * 3 - 4 / 6")]
        [TestCase("$a + 312 * 3 - 4 / 6 ^ $c")]
        [TestCase("$a + 312 + 3 ^ 2 = $a")]
        [TestCase("($a + 312)")]
        [TestCase("($a + 312) * 3")]
        [TestCase("$a + 312 * (3 - 4)")]
        [TestCase("$a + (312 * 3 - 4) / 6")]
        [TestCase("$a + (312 * 3 - 4 / 6) ^ $c")]
        [TestCase("$a + 312 + (3 ^ 2) = $a")]
        [TestCase("9 - $a + 312 * (3 - 4)")]
        [TestCase("$a + (312 * 3 - 4) / 6")]
        [TestCase("$a - (312 * 3 - 4 / 6) ^ $c")]
        [TestCase("$a + 312 + (3 ^ 2) = $a")]
        [TestCase("($a + 312 / $a) / 3 * 7")]
        [TestCase("($a + 312 / $a) / 3 * 7 / $c")]
        [TestCase("($a + 312 / $a) / 3 * 7 / $c * 9")]
        [TestCase("($a + 312 / $a) / 3 * 7 / $c * 9 / $a")]
        [TestCase("Cos($a) + Sin(312)")]
        [TestCase("Cos($a) + Sin(312) * Cos($b)")]
        [TestCase("Cos($a) + Sin(312) * Cos($b) - Sin(4)")]
        [TestCase("Cos($a) + Sin(312) * Cos($b) - Sin(4) / Cos(6)")]
        [TestCase("Cos($a) + Sin(312) * Cos($b) - Sin(4) / Cos(6) ^ Sin($c)")]
        [TestCase("Cos($a) + Sin(312) + Cos($b) ^ Sin(9) = Cos($a)")]
        [TestCase("(Cos($a) + Sin(312))")]
        [TestCase("(Cos($a) + Sin(312)) * Cos($b)")]
        [TestCase("Cos($a) + Sin(312) * (Cos($b) - Sin(4))")]
        [TestCase("Cos($a) + (Sin(312) * Cos($b) - Sin(4)) / Cos(6)")]
        [TestCase("Cos($a) + (Sin(312) * Cos($b) - Sin(4) / Cos(6)) ^ Sin($c)")]
        [TestCase("Cos($a) + Sin(312) + (Cos($b) ^ Sin(9)) = Cos($a)")]
        [TestCase("Cos(9) - Sin($a) + Cos(312) * (Cos($b) - Cos(4))")]
        [TestCase("Cos($a) + (Cos(312) * Sin($b) - Cos(4)) / Sin(6)")]
        [TestCase("Cos($a) - (Sin(312) * Cos($b) - Sin(4) / Cos(6)) ^ Sin($c)")]
        [TestCase("Cos($a) + Sin(312) + (Cos($b) ^ Sin(9)) = Cos($a)")]
        [TestCase("(Cos($a) + Sin(312) / Cos($a)) / Sin($b) * Cos(7)")]
        [TestCase("(Cos($a) + Sin(312) / Cos($a)) / Sin($b) * Cos(7) / Sin($c)")]
        [TestCase("(Cos($a) + Sin(312) / Cos($a)) / Sin($b) * Cos(7) / Sin($c) * Cos(9)")]
        [TestCase("(Cos($a) + Sin(312) / Cos($a)) / Cos($b) * Cos(7) / Cos($c) * Cos(9) / Cos($a)")]
        [TestCase("$a + '312'")]
        [TestCase("$a + '312' * '3'")]
        [TestCase("$a + '312' * '3' - '4'")]
        [TestCase("$a + '312' * '3' - '4' / '6'")]
        [TestCase("$a + '312' * '3' - '4' / '6' ^ '2'")]
        [TestCase("$a + '312' + '3' ^ '9' = $a")]
        [TestCase("($a + '312')")]
        [TestCase("($a + '312') * '3'")]
        [TestCase("$a + '312' * ('3' - '4')")]
        [TestCase("$a + ('312' * '3' - '4') / '6'")]
        [TestCase("$a + ('312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("$a + '312' + ('3' ^ '9') = $a")]
        [TestCase("'9' - $a + '312' * ('3' - '4')")]
        [TestCase("$a + ('312' * '3' - '4') / '6'")]
        [TestCase("$a - ('312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("$a + '312' + ('3' ^ '9') = $a")]
        [TestCase("($a + '312' / $a) / '3' * '7'")]
        [TestCase("($a + '312' / $a) / '3' * '7' / '8'")]
        [TestCase("($a + '312' / $a) / '3' * '7' / '8' * '9'")]
        [TestCase("($a + '312' / $a) / '3' * '7' / '8' * '9' / $a")]
        [TestCase("Cos($a) + Sin('312')")]
        [TestCase("Cos($a) + Sin($a) * Cos('3')")]
        [TestCase("Cos($a) + Sin($a) * Cos('3') - Sin(4)")]
        [TestCase("Cos($a) + Sin($a) * Cos('3') - Sin(4) / Cos(6)")]
        [TestCase("Cos($a) + Sin($a) * Cos('3') - Sin(4) / Cos(6) ^ Sin($c)")]
        [TestCase("Cos($a) + Sin($a) + Cos('3') ^ Sin(9) = Cos($a)")]
        [TestCase("(Cos($a) + Sin($a))")]
        [TestCase("(Cos($a) + Sin($a)) * Cos('3')")]
        [TestCase("Cos($a) + Sin($a) * (Cos('3') - Sin(4))")]
        [TestCase("Cos($a) + (Sin($a) * Cos('3') - Sin(4)) / Cos(6)")]
        [TestCase("Cos($a) + (Sin($a) * Cos('3') - Sin(4) / Cos(6)) ^ Sin($c)")]
        [TestCase("Cos($a) + Sin($a) + (Cos('3') ^ Sin(9)) = Cos($a)")]
        [TestCase("Cos(9) - Sin($a) + Cos($a) * (Cos('3') - Cos(4))")]
        [TestCase("Cos($a) + (Cos($a) * Sin('3') - Cos(4)) / Sin(6)")]
        [TestCase("Cos($a) - (Sin($a) * Cos('3') - Sin(4) / Cos(6)) ^ Sin($c)")]
        [TestCase("Cos($a) + Sin($a) + (Cos('3') ^ Sin(9)) = Cos($a)")]
        [TestCase("(Cos($a) + Sin($a) / Cos($a)) / Sin('3') * Cos(7)")]
        [TestCase("(Cos($a) + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin($c)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin($c) * Cos(9)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Cos('3') * Cos(7) / Cos($c) * Cos(9) / Cos(10)")]
        [TestCase("1312.1278 + 312.0312")]
        [TestCase("1312.1278 + 312.0312 * 3")]
        [TestCase("1312.1278 + 312.0312 * 3 - 4")]
        [TestCase("1312.1278 + 312.0312 * 3 - 4 / 6")]
        [TestCase("1312.1278 + 312.0312 * 3 - 4 / 6 ^ $c")]
        [TestCase("1312.1278 + 312.0312 + 3 ^ 2 = 1312.1278")]
        [TestCase("(1312.1278 + 312.0312)")]
        [TestCase("(1312.1278 + 312.0312) * 3")]
        [TestCase("1312.1278 + 312.0312 * (3 - 4)")]
        [TestCase("1312.1278 + (312.0312 * 3 - 4) / 6")]
        [TestCase("1312.1278 + (312.0312 * 3 - 4 / 6) ^ $c")]
        [TestCase("1312.1278 + 312.0312 + (3 ^ 2) = 1312.1278")]
        [TestCase("9 - 1312.1278 + 312.0312 * (3 - 4)")]
        [TestCase("1312.1278 + (312.0312 * 3 - 4) / 6")]
        [TestCase("1312.1278 - (312.0312 * 3 - 4 / 6) ^ $c")]
        [TestCase("1312.1278 + 312.0312 + (3 ^ 2) = 1312.1278")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / $c")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / $c * 9")]
        [TestCase("(1312.1278 + 312.0312 / 1312.1278) / 3 * 7 / $c * 9 / 1312.12780")]
        [TestCase("Cos(1312.1278) + Sin(312.0312)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos($b)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos($b) - Sin(4)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos($b) - Sin(4) / Cos(6)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * Cos($b) - Sin(4) / Cos(6) ^ Sin($c)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) + Cos($b) ^ Sin(9) = Cos(1312.1278)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312))")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312)) * Cos($b)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) * (Cos($b) - Sin(4))")]
        [TestCase("Cos(1312.1278) + (Sin(312.0312) * Cos($b) - Sin(4)) / Cos(6)")]
        [TestCase("Cos(1312.1278) + (Sin(312.0312) * Cos($b) - Sin(4) / Cos(6)) ^ Sin($c)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) + (Cos($b) ^ Sin(9)) = Cos(1312.1278)")]
        [TestCase("Cos(9) - Sin(1312.1278) + Cos(312.0312) * (Cos($b) - Cos(4))")]
        [TestCase("Cos(1312.1278) + (Cos(312.0312) * Sin($b) - Cos(4)) / Sin(6)")]
        [TestCase("Cos(1312.1278) - (Sin(312.0312) * Cos($b) - Sin(4) / Cos(6)) ^ Sin($c)")]
        [TestCase("Cos(1312.1278) + Sin(312.0312) + (Cos($b) ^ Sin(9)) = Cos(1312.1278)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin($b) * Cos(7)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin($b) * Cos(7) / Sin($c)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Sin($b) * Cos(7) / Sin($c) * Cos(9)")]
        [TestCase("(Cos(1312.1278) + Sin(312.0312) / Cos(1312.1278)) / Cos($b) * Cos(7) / Cos($c) * Cos(9) / Cos(1312.12780)")]
        [TestCase("'1312.1278' + '312.0312'")]
        [TestCase("'1312.1278' + '312.0312' * '3'")]
        [TestCase("'1312.1278' + '312.0312' * '3' - '4'")]
        [TestCase("'1312.1278' + '312.0312' * '3' - '4' / '6'")]
        [TestCase("'1312.1278' + '312.0312' * '3' - '4' / '6' ^ '2'")]
        [TestCase("'1312.1278' + '312.0312' + '3' ^ '9' = '1312.1278'")]
        [TestCase("('1312.1278' + '312.0312')")]
        [TestCase("('1312.1278' + '312.0312') * '3'")]
        [TestCase("'1312.1278' + '312.0312' * ('3' - '4')")]
        [TestCase("'1312.1278' + ('312.0312' * '3' - '4') / '6'")]
        [TestCase("'1312.1278' + ('312.0312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("'1312.1278' + '312.0312' + ('3' ^ '9') = '1312.1278'")]
        [TestCase("'9' - '1312.1278' + '312.0312' * ('3' - '4')")]
        [TestCase("'1312.1278' + ('312.0312' * '3' - '4') / '6'")]
        [TestCase("'1312.1278' - ('312.0312' * '3' - '4' / '6') ^ '2'")]
        [TestCase("'1312.1278' + '312.0312' + ('3' ^ '9') = '1312.1278'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8' * '9'")]
        [TestCase("('1312.1278' + '312.0312' / '1312.1278') / '3' * '7' / '8' * '9' / '1312.12780'")]
        [TestCase("Cos('1312.1278') + Sin('312.0312')")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3')")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6) ^ Sin($c)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') + Cos('3') ^ Sin(9) = Cos('1312.1278')")]
        [TestCase("(Cos('1312.1278') + Sin('1312.1278'))")]
        [TestCase("(Cos('1312.1278') + Sin('1312.1278')) * Cos('3')")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') * (Cos('3') - Sin(4))")]
        [TestCase("Cos('1312.1278') + (Sin('1312.1278') * Cos('3') - Sin(4)) / Cos(6)")]
        [TestCase("Cos('1312.1278') + (Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)) ^ Sin($c)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') + (Cos('3') ^ Sin(9)) = Cos('1312.1278')")]
        [TestCase("Cos(9) - Sin('1312.1278') + Cos('1312.1278') * (Cos('3') - Cos(4))")]
        [TestCase("Cos('1312.1278') + (Cos('1312.1278') * Sin('3') - Cos(4)) / Sin(6)")]
        [TestCase("Cos('1312.1278') - (Sin('1312.1278') * Cos('3') - Sin(4) / Cos(6)) ^ Sin($c)")]
        [TestCase("Cos('1312.1278') + Sin('1312.1278') + (Cos('3') ^ Sin(9)) = Cos('1312.1278')")]
        [TestCase("(Cos('1312.1278') + Sin('1312.1278') / Cos('1312.1278')) / Sin('3') * Cos(7)")]
        [TestCase("(Cos('1312.1278') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin($c)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Sin('3') * Cos(7) / Sin($c) * Cos(9)")]
        [TestCase("(Cos('123') + Sin('123') / Cos('123')) / Cos('3') * Cos(7) / Cos($c) * Cos(9) / Cos(10)")]
        [TestCase("123 ^ 2")]
        [TestCase("SetError(-1, -2, -1)")]
        [TestCase("SetError(-1, 0, -1)")]
        [TestCase("SetError(1, -1, 1)")]
        [TestCase("-$a + '--312' * '-+3'")]
        [TestCase("-$a + '-312' * '-3' - '--4'")]
        [TestCase("'-++-$a' + '-+-312' * '+-+3' - '-4' / '6'")]
        [TestCase("-$a + '--312' * '-+3'")]
        [TestCase("-$a + '-312' * '-3' - '--4'")]
        [TestCase("-++-$a + -+-312 * +-+3 - -4 / 6")]
        [TestCase("-1 - -1 - -8 + -1")]
        [TestCase("--(-(123 + 321) / 4 * 9) + - 1")]
        public void Test_ExpressionsVariables(string expression)
        {
            var random = new Random();
            Variant a = random.Next(0, 1337);
            Variant b = random.Next(0, 1337);
            Variant c = random.Next(0, 1337);

            var script = string.Format(_scriptTemplateWithParameters, expression);
            var assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false);
            var assembly = Assembly.Load(assemblyBytes);
            var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
            var method = type.GetMethod("f_ExpressionReturner");
            var instance = type.CreateInstanceWithDefaultParameters();
            var result = method.Invoke( instance, new object[] { a, b, c } ) as Variant;

            var au3Expression = expression.Replace( "$a", a.ToString() )
                                          .Replace( "$b", b.ToString() )
                                          .Replace( "$c", c.ToString() );

            var au3Result = GetAu3Result(string.Format("f!{0}", au3Expression), result.GetRealType());


            CompareResults(result, au3Result);
        }


        [TestCase("-$a[0] + '--312' * '-+3'")]
        [TestCase("-$a[1] + '-312' * '-3' - '--4'")]
        [TestCase("'-++-$a[3]' + '-+-312' * '+-+3' - '-4' / '6'")]
        [TestCase("-$a[1] + '--312' * '-+3'")]
        [TestCase("-$a[1] + '-312' * '-3' - '--4'")]
        [TestCase("-++-$a[1] + -+-312 * +-+3 - -4 / 6")]
        [TestCase("-1 - -1 - -8 + -1")]
        [TestCase("--(-($a[1] + $c[0]) / 4 * 9) + - 1")]
        [TestCase("-$a[$a[0]] + '--312' * '-+3'")]
        [TestCase("-$a[$a[0]] + '-312' * '-3' - '--4'")]
        [TestCase("'-++-$a[$a[0]]' + '-+-312' * '+-+3' - '-4' / '6'")]
        [TestCase("-$a[$a[$a[0]]] + '--312' * '-+3'")]
        [TestCase("-$a[$a[0]] + '-312' * '-3' - '--4'")]
        [TestCase("-++-$a[$a[0]] + -+-312 * +-+3 - -4 / 6")]
        [TestCase("-1 - -1 - -8 + -1")]
        [TestCase("--(-($a[$a[0]] + $c[$a[0]]) / 4 * 9) + - 1")]
        [TestCase("$a[0]")]
        public void Test_ExpressionArray( string expression ) {
            Assert.DoesNotThrow(
                () => {

                    var script = string.Format(_scriptTemplateWithParameters, expression);
                    var assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false, true);
                    var assembly = Assembly.Load(assemblyBytes);
                    var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
                    var method = type.GetMethod("f_ExpressionReturner");
                    var instance = type.CreateInstanceWithDefaultParameters();
                    
                    Variant variants = new Variant[] { 0, 0 };
                    Variant variants1 = new Variant[]{ 0, 0 };
                    Variant variants2 = new Variant[] { 0, 0 };
                    var result = method.Invoke(instance, new object[] { variants, variants1, variants2 });

                });
        }

        [TestCase("$a[0][0]")]
        [TestCase("-$a[0][0] + '--312' * '-+3'")]
        [TestCase("-$a[1][1] + '-312' * '-3' - '--4'")]
        [TestCase("-++-$a[3][0] + '-+-312' * '+-+3' - '-4' / '6'")]
        [TestCase("-$a[1][0] + '--312' * '-+3'")]
        [TestCase("-$a[1][0] + '-312' * '-3' - '--4'")]
        [TestCase("-++-$a[1][0] + -+-312 * +-+3 - -4 / 6")]
        [TestCase("-1 - -1 - -8 + -1")]
        [TestCase("--(-($a[1][0] + $c[0][0]) / 4 * 9) + - 1")]
        [TestCase("-$a[0][0] + '--312' * '-+3'")]
        [TestCase("-$a[$a[0][0]][$a[0][0]] + '-312' * '-3' - '--4'")]
        [TestCase("-++-$a[$a[0][0]][$a[0][0]] + '-+-312' * '+-+3' - '-4' / '6'")]
        [TestCase("-$a[1][$a[0][0]] + '--312' * '-+3'")]
        [TestCase("-$a[1][$a[0][0]] + '-312' * '-3' - '--4'")]
        [TestCase("-++-$a[1][$a[0][0]] + -+-312 * +-+3 - -4 / 6")]
        [TestCase("-1 - -1 - -8 + -1")]
        [TestCase("--(-($a[1][0] + $c[$a[0][0]][0]) / 4 * 9) + - 1")]
        public void Test_ExpressionArray2D( string expression ) {

            Assert.DoesNotThrow(
                () => {
                    
                    var script = string.Format(_scriptTemplateWithParameters, expression);
                    var assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false);
                    var assembly = Assembly.Load(assemblyBytes);
                    var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
                    var method = type.GetMethod("f_ExpressionReturner");
                    var instance = type.CreateInstanceWithDefaultParameters();
                    
                    Variant variants = new Variant[4, 4];
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            variants[i, j] = 0;
                        }
                    }
                    var variant = variants[variants[0, 0], variants[0, 0]];
                    var parameters = new object[] { variants, variants, variants };
                    var result = method.Invoke(instance, parameters) as Variant;

                } );
        }



        [Test]
        public void Test_ArrayInitExpression() {
            string script = File.ReadAllText("C:\\Users\\Brunnmeier\\Documents\\PrivateGIT\\Autoit.NET\\UnitTests\\testdata\\testarrayinit.au3");

            var assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false);
            var assembly = Assembly.Load(assemblyBytes);
            var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
            var method = type.GetMethod("f_ExpressionReturner");
            var instance = type.CreateInstanceWithDefaultParameters();
            var array = (Variant)method.Invoke( instance, null );
            Assert.IsTrue( array[0, 0] == 0 );
            Assert.IsTrue( array[1, 0] != 0 );
        }
        
        [Test]
        public void Test_ArrayInitExpression3() {
            string script = File.ReadAllText("C:\\Users\\Brunnmeier\\Documents\\PrivateGIT\\Autoit.NET\\UnitTests\\testdata\\testarrayinit2.au3");


            var assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false);
            var assembly = Assembly.Load(assemblyBytes);
            var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
            var method = type.GetMethod("f_ExpressionReturner");
            var instance = type.CreateInstanceWithDefaultParameters();
            var array = (Variant)method.Invoke( instance, null );
        }
        
        [Test]
        public void Test_ArrayInitExpression2() {
            var vGaInProcessWinApi = (Variant)new Variant[3, 3];
            var variants = new Variant[]
            {
                new Variant[]
                {
                    Variant.Create(1), Variant.Create(2), Variant.Create(3)}

                , new Variant[]
                {
                    Variant.Create(2), Variant.Create(3), Variant.Create(4)}

                , new Variant[]
                {
                    Variant.Create(3), Variant.Create(4), Variant.Create(5)}
            };
            vGaInProcessWinApi.InitArray( variants );
        }
    }
}