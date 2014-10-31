﻿using System;
using System.Linq;
using System.Reflection;
using AutoJIT.Compiler;
using AutoJIT.Infrastructure;
using AutoJITRuntime;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace UnitTests
{
	public class VariantTests : AutoitFunctionTestBase
	{
		private readonly ICompiler _compiler;
		private readonly string _scriptTemplate = string.Format( "Func ExpressionReturner(){0}Return {{0}}{0}Endfunc{0}", Environment.NewLine );
		private readonly string _scriptTemplate2 = string.Format( "Func ExpressionReturner(){0}{{0}}{0}Endfunc{0}", Environment.NewLine );

		public VariantTests() {
			var componentContainer = new CompilerBootStrapper();
			_compiler = componentContainer.GetInstance<ICompiler>();
		}

		[TestCase( "123+321" )]
		[TestCase( "123+321.312" )]
		[TestCase( "123.123+321.321" )]
		[TestCase( "1231231231232222+321.23" )]
		[TestCase( "'23'+321" )]
		[TestCase( "123+'321.312'" )]
		[TestCase( "123.123+321.321" )]
		[TestCase( "1231231231232222+'a321.23'" )]
		[TestCase( "123-321" )]
		[TestCase( "123-321.312" )]
		[TestCase( "123.123-321.321" )]
		[TestCase( "1231231231232222+321.23" )]
		[TestCase( "'23'-321" )]
		[TestCase( "123-'321.312'" )]
		[TestCase( "123.123-321.321" )]
		[TestCase( "1231231231232222-'a321.23'" )]
		[TestCase( "123/321" )]
		[TestCase( "123/321.312" )]
		[TestCase( "123.123/321.321" )]
		[TestCase( "1231231231232222/321.23" )]
		[TestCase( "'23'/321" )]
		[TestCase( "123/'321.312'" )]
		[TestCase( "123.123/321.321" )]
		[TestCase( "1231231231232222/'a321.23'" )]
		[TestCase( "123*321" )]
		[TestCase( "123*321.312" )]
		[TestCase( "123.123*321.321" )]
		[TestCase( "1231231231232222*321.23" )]
		[TestCase( "'23'*321" )]
		[TestCase( "123*'321.312'" )]
		[TestCase( "123.123*321.321" )]
		[TestCase( "1231231231232222*'a321.23'" )]
		[TestCase( "123^2" )]
		[TestCase( "123^1.312" )]
		[TestCase( "123.123^1.321" )]
		[TestCase( "1231231231232222^1.23" )]
		[TestCase( "'23'^3" )]
		[TestCase( "123^'3'" )]
		[TestCase( "123.123^1.321" )]
		[TestCase( "1231231231232222^'a1.23'" )]
		[TestCase( "IsInt(123+321)" )]
		[TestCase( "IsInt(123+321.312)" )]
		[TestCase( "IsInt(123.123+321.321)" )]
		[TestCase( "IsInt(1231231231232222+321.23)" )]
		[TestCase( "IsInt('23'+321)" )]
		[TestCase( "IsInt(123+'321.312')" )]
		[TestCase( "IsInt(123.123+321.321)" )]
		[TestCase( "IsInt(1231231231232222+'a321.23')" )]
		[TestCase( "IsInt(123-321)" )]
		[TestCase( "IsInt(123-321.312)" )]
		[TestCase( "IsInt(123.123-321.321)" )]
		[TestCase( "IsInt(1231231231232222+321.23)" )]
		[TestCase( "IsInt('23'-321)" )]
		[TestCase( "IsInt(123-'321.312')" )]
		[TestCase( "IsInt(123.123-321.321)" )]
		[TestCase( "IsInt(1231231231232222-'a321.23')" )]
		[TestCase( "IsInt(123/321)" )]
		[TestCase( "IsInt(123/321.312)" )]
		[TestCase( "IsInt(123.123/321.321)" )]
		[TestCase( "IsInt(1231231231232222/321.23)" )]
		[TestCase( "IsInt('23'/321)" )]
		[TestCase( "IsInt(123/'321.312')" )]
		[TestCase( "IsInt(123.123/321.321)" )]
		[TestCase( "IsInt(1231231231232222/'a321.23')" )]
		[TestCase( "IsInt(123*321)" )]
		[TestCase( "IsInt(123*321.312)" )]
		[TestCase( "IsInt(123.123*321.321)" )]
		[TestCase( "IsInt(1231231231232222*321.23)" )]
		[TestCase( "IsInt('23'*321)" )]
		[TestCase( "IsInt(123*'321.312')" )]
		[TestCase( "IsInt(123.123*321.321)" )]
		[TestCase( "IsInt(1231231231232222*'a321.23')" )]
		[TestCase( "IsInt(123^2)" )]
		[TestCase( "IsInt(123^1.312)" )]
		[TestCase( "IsInt(123.123^1.321)" )]
		[TestCase( "IsInt(1231231231232222^1.23)" )]
		[TestCase( "IsInt('23'^3)" )]
		[TestCase( "IsInt(123^'3')" )]
		[TestCase( "IsInt(123.123^1.321)" )]
		[TestCase( "IsInt(1231231231232222^'a1.23')" )]
		[TestCase( "IsFloat(123+321)" )]
		[TestCase( "IsFloat(123+321.312)" )]
		[TestCase( "IsFloat(123.123+321.321)" )]
		[TestCase( "IsFloat(1231231231232222+321.23)" )]
		[TestCase( "IsFloat('23'+321)" )]
		[TestCase( "IsFloat(123+'321.312')" )]
		[TestCase( "IsFloat(123.123+321.321)" )]
		[TestCase( "IsFloat(1231231231232222+'a321.23')" )]
		[TestCase( "IsFloat(123-321)" )]
		[TestCase( "IsFloat(123-321.312)" )]
		[TestCase( "IsFloat(123.123-321.321)" )]
		[TestCase( "IsFloat(1231231231232222+321.23)" )]
		[TestCase( "IsFloat('23'-321)" )]
		[TestCase( "IsFloat(123-'321.312')" )]
		[TestCase( "IsFloat(123.123-321.321)" )]
		[TestCase( "IsFloat(1231231231232222-'a321.23')" )]
		[TestCase( "IsFloat(123/321)" )]
		[TestCase( "IsFloat(123/321.312)" )]
		[TestCase( "IsFloat(123.123/321.321)" )]
		[TestCase( "IsFloat(1231231231232222/321.23)" )]
		[TestCase( "IsFloat('23'/321)" )]
		[TestCase( "IsFloat(123/'321.312')" )]
		[TestCase( "IsFloat(123.123/321.321)" )]
		[TestCase( "IsFloat(1231231231232222/'a321.23')" )]
		[TestCase( "IsFloat(123*321)" )]
		[TestCase( "IsFloat(123*321.312)" )]
		[TestCase( "IsFloat(123.123*321.321)" )]
		[TestCase( "IsFloat(1231231231232222*321.23)" )]
		[TestCase( "IsFloat('23'*321)" )]
		[TestCase( "IsFloat(123*'321.312')" )]
		[TestCase( "IsFloat(123.123*321.321)" )]
		[TestCase( "IsFloat(1231231231232222*'a321.23')" )]
		[TestCase( "IsFloat(123^2)" )]
		[TestCase( "IsFloat(123^1.312)" )]
		[TestCase( "IsFloat(123.123^1.321)" )]
		[TestCase( "IsFloat(1231231231232222^1.23)" )]
		[TestCase( "IsFloat('23'^3)" )]
		[TestCase( "IsFloat(123^'3')" )]
		[TestCase( "IsFloat(123.123^1.321)" )]
		[TestCase( "IsFloat(1231231231232222^'a1.23')" )]
		[TestCase( "IsArray(123+321)" )]
		[TestCase( "IsArray(123+321.312)" )]
		[TestCase( "IsArray(123.123+321.321)" )]
		[TestCase( "IsArray(1231231231232222+321.23)" )]
		[TestCase( "IsArray('23'+321)" )]
		[TestCase( "IsArray(123+'321.312')" )]
		[TestCase( "IsArray(123.123+321.321)" )]
		[TestCase( "IsArray(1231231231232222+'a321.23')" )]
		[TestCase( "IsArray(123-321)" )]
		[TestCase( "IsArray(123-321.312)" )]
		[TestCase( "IsArray(123.123-321.321)" )]
		[TestCase( "IsArray(1231231231232222+321.23)" )]
		[TestCase( "IsArray('23'-321)" )]
		[TestCase( "IsArray(123-'321.312')" )]
		[TestCase( "IsArray(123.123-321.321)" )]
		[TestCase( "IsArray(1231231231232222-'a321.23')" )]
		[TestCase( "IsArray(123/321)" )]
		[TestCase( "IsArray(123/321.312)" )]
		[TestCase( "IsArray(123.123/321.321)" )]
		[TestCase( "IsArray(1231231231232222/321.23)" )]
		[TestCase( "IsArray('23'/321)" )]
		[TestCase( "IsArray(123/'321.312')" )]
		[TestCase( "IsArray(123.123/321.321)" )]
		[TestCase( "IsArray(1231231231232222/'a321.23')" )]
		[TestCase( "IsArray(123*321)" )]
		[TestCase( "IsArray(123*321.312)" )]
		[TestCase( "IsArray(123.123*321.321)" )]
		[TestCase( "IsArray(1231231231232222*321.23)" )]
		[TestCase( "IsArray('23'*321)" )]
		[TestCase( "IsArray(123*'321.312')" )]
		[TestCase( "IsArray(123.123*321.321)" )]
		[TestCase( "IsArray(1231231231232222*'a321.23')" )]
		[TestCase( "IsArray(123^2)" )]
		[TestCase( "IsArray(123^1.312)" )]
		[TestCase( "IsArray(123.123^1.321)" )]
		[TestCase( "IsArray(1231231231232222^1.23)" )]
		[TestCase( "IsArray('23'^3)" )]
		[TestCase( "IsArray(123^'3')" )]
		[TestCase( "IsArray(123.123^1.321)" )]
		[TestCase( "IsArray(1231231231232222^'a1.23')" )]
		[TestCase( "IsBool(123+321)" )]
		[TestCase( "IsBool(123+321.312)" )]
		[TestCase( "IsBool(123.123+321.321)" )]
		[TestCase( "IsBool(1231231231232222+321.23)" )]
		[TestCase( "IsBool('23'+321)" )]
		[TestCase( "IsBool(123+'321.312')" )]
		[TestCase( "IsBool(123.123+321.321)" )]
		[TestCase( "IsBool(1231231231232222+'a321.23')" )]
		[TestCase( "IsBool(123-321)" )]
		[TestCase( "IsBool(123-321.312)" )]
		[TestCase( "IsBool(123.123-321.321)" )]
		[TestCase( "IsBool(1231231231232222+321.23)" )]
		[TestCase( "IsBool('23'-321)" )]
		[TestCase( "IsBool(123-'321.312')" )]
		[TestCase( "IsBool(123.123-321.321)" )]
		[TestCase( "IsBool(1231231231232222-'a321.23')" )]
		[TestCase( "IsBool(123/321)" )]
		[TestCase( "IsBool(123/321.312)" )]
		[TestCase( "IsBool(123.123/321.321)" )]
		[TestCase( "IsBool(1231231231232222/321.23)" )]
		[TestCase( "IsBool('23'/321)" )]
		[TestCase( "IsBool(123/'321.312')" )]
		[TestCase( "IsBool(123.123/321.321)" )]
		[TestCase( "IsBool(1231231231232222/'a321.23')" )]
		[TestCase( "IsBool(123*321)" )]
		[TestCase( "IsBool(123*321.312)" )]
		[TestCase( "IsBool(123.123*321.321)" )]
		[TestCase( "IsBool(1231231231232222*321.23)" )]
		[TestCase( "IsBool('23'*321)" )]
		[TestCase( "IsBool(123*'321.312')" )]
		[TestCase( "IsBool(123.123*321.321)" )]
		[TestCase( "IsBool(1231231231232222*'a321.23')" )]
		[TestCase( "IsBool(123^2)" )]
		[TestCase( "IsBool(123^1.312)" )]
		[TestCase( "IsBool(123.123^1.321)" )]
		[TestCase( "IsBool(1231231231232222^1.23)" )]
		[TestCase( "IsBool('23'^3)" )]
		[TestCase( "IsBool(123^'3')" )]
		[TestCase( "IsBool(123.123^1.321)" )]
		[TestCase( "IsBool(1231231231232222^'a1.23')" )]
		[TestCase( "IsInt(0.01)" )]
		[TestCase( "IsInt(0.001)" )]
		[TestCase( "IsInt(0.0001)" )]
		[TestCase( "IsInt(0.00001)" )]
		[TestCase( "IsInt(0.000001)" )]
		[TestCase( "IsInt(0.0000001)" )]
		[TestCase( "IsInt(0.00000001)" )]
		[TestCase( "IsInt(0.000000001)" )]
		[TestCase( "IsInt(0.0000000001)" )]
		[TestCase( "IsInt(0.00000000001)" )]
		[TestCase( "IsInt(0.000000000001)" )]
		[TestCase( "IsInt(0.0000000000001)" )]
		[TestCase( "IsInt(0.00000000000001)" )]
		[TestCase( "IsInt(0.00000000000001)" )]
		[TestCase( "IsInt(0.000000000000001)" )]
		[TestCase( "IsInt(0.0000000000000001)" )]
		[TestCase( "IsInt(0.00000000000000001)" )]
		[TestCase( "IsInt(0.09)" )]
		[TestCase( "IsInt(0.009)" )]
		[TestCase( "IsInt(0.0009)" )]
		[TestCase( "IsInt(0.00009)" )]
		[TestCase( "IsInt(0.000009)" )]
		[TestCase( "IsInt(0.0000009)" )]
		[TestCase( "IsInt(0.00000009)" )]
		[TestCase( "IsInt(0.000000009)" )]
		[TestCase( "IsInt(0.0000000009)" )]
		[TestCase( "IsInt(0.00000000009)" )]
		[TestCase( "IsInt(0.000000000009)" )]
		[TestCase( "IsInt(0.0000000000009)" )]
		[TestCase( "IsInt(0.00000000000009)" )]
		[TestCase( "IsInt(0.00000000000009)" )]
		[TestCase( "IsInt(0.000000000000009)" )]
		[TestCase( "IsInt(0.0000000000000009)" )]
		[TestCase( "IsInt(0.00000000000000009)" )]
		[TestCase( "IsFloat(0.01)" )]
		[TestCase( "IsFloat(0.001)" )]
		[TestCase( "IsFloat(0.0001)" )]
		[TestCase( "IsFloat(0.00001)" )]
		[TestCase( "IsFloat(0.000001)" )]
		[TestCase( "IsFloat(0.0000001)" )]
		[TestCase( "IsFloat(0.00000001)" )]
		[TestCase( "IsFloat(0.000000001)" )]
		[TestCase( "IsFloat(0.0000000001)" )]
		[TestCase( "IsFloat(0.00000000001)" )]
		[TestCase( "IsFloat(0.000000000001)" )]
		[TestCase( "IsFloat(0.0000000000001)" )]
		[TestCase( "IsFloat(0.00000000000001)" )]
		[TestCase( "IsFloat(0.00000000000001)" )]
		[TestCase( "IsFloat(0.000000000000001)" )]
		[TestCase( "IsFloat(0.0000000000000001)" )]
		[TestCase( "IsFloat(0.00000000000000001)" )]
		[TestCase( "IsFloat(0.09)" )]
		[TestCase( "IsFloat(0.009)" )]
		[TestCase( "IsFloat(0.0009)" )]
		[TestCase( "IsFloat(0.00009)" )]
		[TestCase( "IsFloat(0.000009)" )]
		[TestCase( "IsFloat(0.0000009)" )]
		[TestCase( "IsFloat(0.00000009)" )]
		[TestCase( "IsFloat(0.000000009)" )]
		[TestCase( "IsFloat(0.0000000009)" )]
		[TestCase( "IsFloat(0.00000000009)" )]
		[TestCase( "IsFloat(0.000000000009)" )]
		[TestCase( "IsFloat(0.0000000000009)" )]
		[TestCase( "IsFloat(0.00000000000009)" )]
		[TestCase( "IsFloat(0.00000000000009)" )]
		[TestCase( "IsFloat(0.000000000000009)" )]
		[TestCase( "IsFloat(0.0000000000000009)" )]
		[TestCase( "IsFloat(0.00000000000000009)" )]
		[TestCase( "IsInt(0.9)" )]
		[TestCase( "IsInt(0.99)" )]
		[TestCase( "IsInt(0.999)" )]
		[TestCase( "IsInt(0.9999)" )]
		[TestCase( "IsInt(0.99999)" )]
		[TestCase( "IsInt(0.999999)" )]
		[TestCase( "IsInt(0.9999999)" )]
		[TestCase( "IsInt(0.99999999)" )]
		[TestCase( "IsInt(0.999999999)" )]
		[TestCase( "IsInt(0.9999999999)" )]
		[TestCase( "IsInt(0.99999999999)" )]
		[TestCase( "IsInt(0.999999999999)" )]
		[TestCase( "IsInt(0.9999999999999)" )]
		[TestCase( "IsInt(0.99999999999999)" )]
		[TestCase( "IsInt(0.999999999999999)" )]
		[TestCase( "IsInt(0.9999999999999999)" )]
		[TestCase( "IsInt(0.99999999999999999)" )]
		[TestCase( "IsInt(0.999999999999999999)" )]
		[TestCase( "IsInt(0.9999999999999999999)" )]
		[TestCase( "IsInt(0.99999999999999999999)" )]
		[TestCase( "IsInt(0.999999999999999999999)" )]
		[TestCase( "IsInt(0.9999999999999999999999)" )]
		[TestCase( "IsInt(0.09)" )]
		[TestCase( "IsInt(0.099)" )]
		[TestCase( "IsInt(0.0999)" )]
		[TestCase( "IsInt(0.09999)" )]
		[TestCase( "IsInt(0.099999)" )]
		[TestCase( "IsInt(0.0999999)" )]
		[TestCase( "IsInt(0.09999999)" )]
		[TestCase( "IsInt(0.099999999)" )]
		[TestCase( "IsInt(0.0999999999)" )]
		[TestCase( "IsInt(0.09999999999)" )]
		[TestCase( "IsInt(0.099999999999)" )]
		[TestCase( "IsInt(0.0999999999999)" )]
		[TestCase( "IsInt(0.09999999999999)" )]
		[TestCase( "IsInt(0.099999999999999)" )]
		[TestCase( "IsInt(0.0999999999999999)" )]
		[TestCase( "IsInt(0.09999999999999999)" )]
		[TestCase( "IsInt(0.099999999999999999)" )]
		[TestCase( "IsInt(0.0999999999999999999)" )]
		[TestCase( "IsInt(0.09999999999999999999)" )]
		[TestCase( "IsInt(0.099999999999999999999)" )]
		[TestCase( "IsInt(0.0999999999999999999999)" )]
		[TestCase( "IsInt(0.09999999999999999999999)" )]
		[TestCase( "IsFloat(0.9)" )]
		[TestCase( "IsFloat(0.99)" )]
		[TestCase( "IsFloat(0.999)" )]
		[TestCase( "IsFloat(0.9999)" )]
		[TestCase( "IsFloat(0.99999)" )]
		[TestCase( "IsFloat(0.999999)" )]
		[TestCase( "IsFloat(0.9999999)" )]
		[TestCase( "IsFloat(0.99999999)" )]
		[TestCase( "IsFloat(0.999999999)" )]
		[TestCase( "IsFloat(0.9999999999)" )]
		[TestCase( "IsFloat(0.99999999999)" )]
		[TestCase( "IsFloat(0.999999999999)" )]
		[TestCase( "IsFloat(0.9999999999999)" )]
		[TestCase( "IsFloat(0.99999999999999)" )]
		[TestCase( "IsFloat(0.999999999999999)" )]
		[TestCase( "IsFloat(0.9999999999999999)" )]
		[TestCase( "IsFloat(0.99999999999999999)" )]
		[TestCase( "IsFloat(0.999999999999999999)" )]
		[TestCase( "IsFloat(0.9999999999999999999)" )]
		[TestCase( "IsFloat(0.99999999999999999999)" )]
		[TestCase( "IsFloat(0.999999999999999999999)" )]
		[TestCase( "IsFloat(0.9999999999999999999999)" )]
		[TestCase( "IsFloat(0.09)" )]
		[TestCase( "IsFloat(0.099)" )]
		[TestCase( "IsFloat(0.0999)" )]
		[TestCase( "IsFloat(0.09999)" )]
		[TestCase( "IsFloat(0.099999)" )]
		[TestCase( "IsFloat(0.0999999)" )]
		[TestCase( "IsFloat(0.09999999)" )]
		[TestCase( "IsFloat(0.099999999)" )]
		[TestCase( "IsFloat(0.0999999999)" )]
		[TestCase( "IsFloat(0.09999999999)" )]
		[TestCase( "IsFloat(0.099999999999)" )]
		[TestCase( "IsFloat(0.0999999999999)" )]
		[TestCase( "IsFloat(0.09999999999999)" )]
		[TestCase( "IsFloat(0.099999999999999)" )]
		[TestCase( "IsFloat(0.0999999999999999)" )]
		[TestCase( "IsFloat(0.09999999999999999)" )]
		[TestCase( "IsFloat(0.099999999999999999)" )]
		[TestCase( "IsFloat(0.0999999999999999999)" )]
		[TestCase( "IsFloat(0.09999999999999999999)" )]
		[TestCase( "IsFloat(0.099999999999999999999)" )]
		[TestCase( "IsFloat(0.0999999999999999999999)" )]
		[TestCase( "IsFloat(0.09999999999999999999999)" )]
		[TestCase( "BitNOT(1.9999999999999999999999)" )]
		[TestCase( "BitNOT(12.09999999999999999999999)" )]
		[TestCase( "BitNOT(-123.09999999999999999999999)" )]
		[TestCase( "BitNOT(1.999999999999999999999)" )]
		[TestCase( "BitNOT(12.0999999999999999999999)" )]
		[TestCase( "BitNOT(-123.0999999999999999999999)" )]
		[TestCase( "BitNOT(1.9999999999999999999)" )]
		[TestCase( "BitNOT(12.09999999999999999999)" )]
		[TestCase( "BitNOT(-123.09999999999999999999)" )]
		[TestCase( "String(Binary(18841))" )]
		[TestCase( "String(Binary(54483))" )]
		[TestCase( "String(Binary(79018))" )]
		[TestCase( "String(Binary(45132))" )]
		[TestCase( "String(Binary(130616))" )]
		[TestCase( "String(Binary(41701))" )]
		[TestCase( "String(Binary(13532))" )]
		[TestCase( "String(Binary(108091))" )]
		[TestCase( "String(Binary(30444))" )]
		[TestCase( "String(Binary(25991))" )]
		[TestCase( "String(Binary(4279))" )]
		[TestCase( "String(Binary(28282))" )]
		[TestCase( "String(Binary(67810))" )]
		[TestCase( "String(Binary(31981))" )]
		[TestCase( "String(Binary(54759))" )]
		[TestCase( "String(Binary(92545))" )]
		[TestCase( "String(Binary(121999))" )]
		[TestCase( "String(Binary(47382))" )]
		[TestCase( "String(Binary(133528))" )]
		[TestCase( "String(Binary(53323))" )]
		[TestCase( "String(Binary(32394))" )]
		[TestCase( "String(Binary(4188))" )]
		[TestCase( "String(Binary(26577))" )]
		[TestCase( "String(Binary(122663))" )]
		[TestCase( "String(Binary(128161))" )]
		[TestCase( "String(Binary(42907))" )]
		[TestCase( "String(Binary(50415))" )]
		[TestCase( "String(Binary(52877))" )]
		[TestCase( "String(Binary(6234))" )]
		[TestCase( "String(Binary(3466))" )]
		[TestCase( "String(Binary(10940))" )]
		[TestCase( "String(Binary(90868))" )]
		[TestCase( "String(Binary(66415))" )]
		[TestCase( "String(Binary(67289))" )]
		[TestCase( "String(Binary(1799))" )]
		[TestCase( "String(Binary(105150))" )]
		[TestCase( "String(Binary(10199))" )]
		[TestCase( "String(Binary(110259))" )]
		[TestCase( "String(Binary(2241))" )]
		[TestCase( "String(Binary(101247))" )]
		[TestCase( "String(Binary(41520))" )]
		[TestCase( "String(Binary(61493))" )]
		[TestCase( "String(Binary(51516))" )]
		[TestCase( "String(Binary(118834))" )]
		[TestCase( "String(Binary(50203))" )]
		[TestCase( "String(Binary(49754))" )]
		[TestCase( "String(Binary(3210))" )]
		[TestCase( "String(Binary(49137))" )]
		[TestCase( "String(Binary(21952))" )]
		[TestCase( "String(Binary(23697))" )]
		[TestCase( "String(Binary(74194))" )]
		[TestCase( "String(Binary(132021))" )]
		[TestCase( "String(Binary(21700))" )]
		[TestCase( "String(Binary(23803))" )]
		[TestCase( "String(Binary(82564))" )]
		[TestCase( "String(Binary(129759))" )]
		[TestCase( "String(Binary(128964))" )]
		[TestCase( "String(Binary(131357))" )]
		[TestCase( "String(Binary(54375))" )]
		[TestCase( "String(Binary(54655))" )]
		[TestCase( "String(Binary(71849))" )]
		[TestCase( "String(Binary(20904))" )]
		[TestCase( "String(Binary(24560))" )]
		[TestCase( "String(Binary(22860))" )]
		[TestCase( "String(Binary(38315))" )]
		[TestCase( "String(Binary(69112))" )]
		[TestCase( "String(Binary(115210))" )]
		[TestCase( "String(Binary(117913))" )]
		[TestCase( "String(Binary(2663))" )]
		[TestCase( "String(Binary(101941))" )]
		[TestCase( "String(Binary(41179))" )]
		[TestCase( "String(Binary(110030))" )]
		[TestCase( "String(Binary(42659))" )]
		[TestCase( "String(Binary(32246))" )]
		[TestCase( "String(Binary(14946))" )]
		[TestCase( "String(Binary(126150))" )]
		[TestCase( "String(Binary(63793))" )]
		[TestCase( "String(Binary(61926))" )]
		[TestCase( "String(Binary(11296))" )]
		[TestCase( "String(Binary(14469))" )]
		[TestCase( "String(Binary(119201))" )]
		[TestCase( "String(Binary(18653))" )]
		[TestCase( "String(Binary(117805))" )]
		[TestCase( "String(Binary(23121))" )]
		[TestCase( "String(Binary(32687))" )]
		[TestCase( "String(Binary(92968))" )]
		[TestCase( "String(Binary(63572))" )]
		[TestCase( "String(Binary(132570))" )]
		[TestCase( "String(Binary(89783))" )]
		[TestCase( "String(Binary(93388))" )]
		[TestCase( "String(Binary(69107))" )]
		[TestCase( "String(Binary(94993))" )]
		[TestCase( "String(Binary(18184))" )]
		[TestCase( "String(Binary(71009))" )]
		[TestCase( "String(Binary(56906))" )]
		[TestCase( "String(Binary(11109))" )]
		[TestCase( "String(Binary(53751))" )]
		[TestCase( "String(Binary(82304))" )]
		[TestCase( "String(Binary(121117))" )]
		[TestCase( "String(Binary(101763))" )]
		[TestCase( "String(Binary('rwdwKvfVL6Lj0fbkrjHW'))" )]
		[TestCase( "String(Binary('4zWnSGLkzpAontSmFA23'))" )]
		[TestCase( "String(Binary('d15JVpnFODSYqO1QjhUY'))" )]
		[TestCase( "String(Binary('pSOMik5dqFPgqcf0CL06'))" )]
		[TestCase( "String(Binary('p8Y4CbMsyPdAomhLe2qz'))" )]
		[TestCase( "String(Binary('E0yUr7W3dXhcMuM5IJpS'))" )]
		[TestCase( "String(Binary('rzyUTdQN6QgQJo3KOVaL'))" )]
		[TestCase( "String(Binary('G8d95xVOAr5z0cMzwTCa'))" )]
		[TestCase( "String(Binary('p8EBG2y2vKOYZyOrcUih'))" )]
		[TestCase( "String(Binary('IdBxKKvtNpU2UUZ3rjCV'))" )]
		[TestCase( "String(Binary('FEwph2du18iw4EVxDOXA'))" )]
		[TestCase( "String(Binary('WIcU0hQOjyDgumpZXv3K'))" )]
		[TestCase( "String(Binary('jRLPm6bkcE1hYknioH7c'))" )]
		[TestCase( "String(Binary('PD7HlGwHiQP36QqW4u9f'))" )]
		[TestCase( "String(Binary('nN3YJMtjV8J16ZQVuI0d'))" )]
		[TestCase( "String(Binary('UGCaKyyuYFPOQrGFNCTI'))" )]
		[TestCase( "String(Binary('8E8CQzN0lJ2EtzNoZb7v'))" )]
		[TestCase( "String(Binary('YvgPdDxZt1BnsWK93pvh'))" )]
		[TestCase( "String(Binary('RpXRK9R5WeNAYzuQZcXK'))" )]
		[TestCase( "String(Binary('EoMpPPKRpoZ6Rx2VK8UX'))" )]
		[TestCase( "String(Binary('3RDL7I5m2HCe0FU0s7Ns'))" )]
		[TestCase( "String(Binary('GRfRx0tU68LQCpVJpfyi'))" )]
		[TestCase( "String(Binary('LmuSLsPIn9JzGzH8nR9D'))" )]
		[TestCase( "String(Binary('4NTqlTbEgtHkSOZsSrL2'))" )]
		[TestCase( "String(Binary('Rw1bWdNmzy7kfrpPOetW'))" )]
		[TestCase( "String(Binary('iXUZitrhqBiWAjBWqkEp'))" )]
		[TestCase( "String(Binary('JiEJez5g1vunNopH521o'))" )]
		[TestCase( "String(Binary('7sSOI4bvBMlTdILCtuOy'))" )]
		[TestCase( "String(Binary('ibaWa5ASHhZEihhXzts7'))" )]
		[TestCase( "String(Binary('mrybpeO716LQSH75mgrB'))" )]
		[TestCase( "String(Binary('6EXVVxHLrBiDXvbgXMpG'))" )]
		[TestCase( "String(Binary('F2EiR2rJGwqT4lGvrOHm'))" )]
		[TestCase( "String(Binary('pQfs6FdSn1s0DQYS4v8J'))" )]
		[TestCase( "String(Binary('IRXPYVlNtjHF1GYHB4RJ'))" )]
		[TestCase( "String(Binary('IoHF8tD6DKHZYK7F5NqJ'))" )]
		[TestCase( "String(Binary('eILPxBN10w8FE3fEIRt8'))" )]
		[TestCase( "String(Binary('Jd7n8d3XosI4UNZrC7f8'))" )]
		[TestCase( "String(Binary('eLYlPHx4cGf19W1WsKHq'))" )]
		[TestCase( "String(Binary('WLFcSlwo6aSBrejsF56t'))" )]
		[TestCase( "String(Binary('LzizLDrhz1HEhZkmYEpJ'))" )]
		[TestCase( "String(Binary('kiKHsgZOQDtNLc9bEAsK'))" )]
		[TestCase( "String(Binary('rovWc2Hg0kSbI9UBfD1Q'))" )]
		[TestCase( "String(Binary('g8RuFg5MRmxUyU97MDb7'))" )]
		[TestCase( "String(Binary('AGzwR1rgAxA6G4PcduuN'))" )]
		[TestCase( "String(Binary('eZRCn4dcue8DYXIimTYI'))" )]
		[TestCase( "String(Binary('ZK6X8w9NLd7HH1dvnbT8'))" )]
		[TestCase( "String(Binary('EdM76kLLnFbgcfuyeXb1'))" )]
		[TestCase( "String(Binary('vDpu11bmh6eGgDZC7MOP'))" )]
		[TestCase( "String(Binary('7Opvgza0t6arDgPGPQju'))" )]
		[TestCase( "String(Binary('sd9p7rq2Y7ITlQvXOJtS'))" )]
		[TestCase( "String(Binary('EuNzDZJrww11P7tpKBSM'))" )]
		[TestCase( "String(Binary('lxOHMosFAvJZYxOXCBIb'))" )]
		[TestCase( "String(Binary('7RBw6ACRCaPi3zrKeUwo'))" )]
		[TestCase( "String(Binary('8m40vABAJ8yZ33Sy4sne'))" )]
		[TestCase( "String(Binary('ZWOAHyEYhfSbZI8E9niP'))" )]
		[TestCase( "String(Binary('NN3ZPj6BGISvACDeyj3B'))" )]
		[TestCase( "String(Binary('2W6uUDXHHegoBRtns7xv'))" )]
		[TestCase( "String(Binary('FJktbs3qjHbZQjEMuNCP'))" )]
		[TestCase( "String(Binary('CPtMUZTcXl3emo6XtxtN'))" )]
		[TestCase( "String(Binary('y0PJU5AF3TEk6aT7Nc0n'))" )]
		[TestCase( "String(Binary('1weRHFd9THU8jP2XvAH0'))" )]
		[TestCase( "String(Binary('i9TAChWiVmK5YbEAVUg1'))" )]
		[TestCase( "String(Binary('lO5UjVyTU8jR42Qdjabz'))" )]
		[TestCase( "String(Binary('zRLHn7AcfWC819SFffbm'))" )]
		[TestCase( "String(Binary('Ueu7UqZVfd29xY9rJGPo'))" )]
		[TestCase( "String(Binary('Njosb7tGPra2UsUL6mfD'))" )]
		[TestCase( "String(Binary('jfercqvGJSSHEVecasSo'))" )]
		[TestCase( "String(Binary('ge5HRxFYB2mP3dWf3NLD'))" )]
		[TestCase( "String(Binary('PcvxXDTA1U52aaPC6IYn'))" )]
		[TestCase( "String(Binary('NsjZTK9j96LoJDULTG5l'))" )]
		[TestCase( "String(Binary('2pAQ4TVwtt9fbGYi7CCZ'))" )]
		[TestCase( "String(Binary('fJTx7alqW2s9VjqIpgrB'))" )]
		[TestCase( "String(Binary('rZqc96tQthc4AD524VSo'))" )]
		[TestCase( "String(Binary('mcP3cvbWwOZQYRC9JpRU'))" )]
		[TestCase( "String(Binary('r0G7mt8SzJv2xf7BK4GT'))" )]
		[TestCase( "String(Binary('HpYWv9XozmIINDkc4T4G'))" )]
		[TestCase( "String(Binary('rP7EOLiAkzq4Dl5Xonyz'))" )]
		[TestCase( "String(Binary('h1CcarleJ3HcHzdbG75g'))" )]
		[TestCase( "String(Binary('Hoe17WDzf7GOaPmdnisN'))" )]
		[TestCase( "String(Binary('cuVYV2L3IurFmArREtzL'))" )]
		[TestCase( "String(Binary('IAlwHGnkcCIqP3RKsD4X'))" )]
		[TestCase( "String(Binary('Ku7smLJIW8hdmweZBFfG'))" )]
		[TestCase( "String(Binary('gs0MmF6B4BvgmWNuGJ2w'))" )]
		[TestCase( "String(Binary('MTAzuXdyivFMGGfJb74y'))" )]
		[TestCase( "String(Binary('2Z24tcaQ7VESDgldFzQ3'))" )]
		[TestCase( "String(Binary('qOyPJK3IgRiruzllzuEQ'))" )]
		[TestCase( "String(Binary('wkJXz4PCxpVOQN0e5Vwr'))" )]
		[TestCase( "String(Binary('pwS2PchVIDb4t3Ed9oiM'))" )]
		[TestCase( "String(Binary('PkTsYr7gMnK5iZnglvCW'))" )]
		[TestCase( "String(Binary('pOWltnjQ6OujYibzLcHA'))" )]
		[TestCase( "String(Binary('y2mKBVGeoRM2ZbZGjRLC'))" )]
		[TestCase( "String(Binary('1cKERYLK58grVaIFrGrD'))" )]
		[TestCase( "String(Binary('7KrSVIzPMCUF5bgpkMVv'))" )]
		[TestCase( "String(Binary('ZIzyPsXxZfAwNXK0Sms6'))" )]
		[TestCase( "String(Binary('h3Sp6N2pRuD83ofRKakh'))" )]
		[TestCase( "String(Binary('lrc1osE59v21PtkSMRxl'))" )]
		[TestCase( "String(Binary('Nfu2y0oAwwf0rsB785sU'))" )]
		[TestCase( "String(Binary('QyQFhCqvTZkL5L8JbxhE'))" )]
		[TestCase( "String(Binary('T0xrhoYVi9XQtahACoeg'))" )]
		[TestCase( "String(Binary('1ZcstHe5aWa95mCa6ba1'))" )]
		public void TestOperatorAndResultDataType( string expression ) {
			string script = string.Format( _scriptTemplate, expression );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false, true );
			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			object au3Result = GetAu3Result( string.Format( "f!{0}", expression ), result.GetRealType() );

			CompareResults( result, au3Result );
		}

		[Test]
		public void Test_Variant() {
			Variant a = 123;
			a = 321.123;
			a = "awdawd12312312";
			a = GetArray();
			Assert.IsTrue( a[0] == 1 );
			Assert.IsTrue( a[1] == 2 );
			Assert.IsTrue( a[2] == "awd" );

			a[0] = GetArray();
			a[1] = GetArray();
			a[2] = GetArray();

			foreach(Variant i in a) {
				foreach(Variant z in i) {
					Assert.IsTrue( z == 1 || z == 2 || z == "awd" );
				}
			}

			var variants = new Variant[1000];
			for( int i = 0; i < variants.Length; i++ ) {
				variants[i] = new Variant[10];
				for( int j = 0; j < 10; j++ ) {
					variants[i][j] = "abcd";
				}
			}

			foreach(Variant variant in variants) {
				foreach(Variant v in variant) {
					Assert.IsTrue( v == "abcd" );
				}
			}
		}

		private Variant GetArray() {
			return new Variant[] {
				1,
				2,
				"awd"
			};
		}
	}
}
