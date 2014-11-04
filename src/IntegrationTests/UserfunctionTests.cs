using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using AutoJIT.Compiler;
using AutoJIT.CompilerApplication;
using AutoJIT.Infrastructure;
using AutoJIT.Parser;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;
using AutoJITRuntime;
using AutoJITRuntime.Variants;
using Microsoft.CodeAnalysis;
using NUnit.Framework;
using SwitchCase = AutoJIT.Parser.AST.Statements.SwitchCase;

namespace UnitTests
{
	public class UserfunctionTests
	{
		private readonly ICompiler _compiler;

		public UserfunctionTests() {
			var standardAutoJITContainer = new CompilerBootStrapper();
			_compiler = standardAutoJITContainer.GetInstance<ICompiler>();
		}

		[TestCase( "_WinAPI_GetIconInfo.au3" )]
		public void Test_compile_includes( string file ) {
			string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
			string script = File.ReadAllText( path );

			Assert.DoesNotThrow(
								() => {
									byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );
									Assembly assembly = Assembly.Load( assemblyBytes );
									Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
									MethodInfo method = type.GetMethod( "f_Example" );
									object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
									object res = method.Invoke( instance, null );
								} );
		}

		[TestCase( "_WinAPI_CreateFile.au3" )]
		public void Test_compile_includes3( string file ) {
			string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
			string script = File.ReadAllText( path );

			Assert.DoesNotThrow(
								() => {
									byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

									Assembly assembly = Assembly.Load( assemblyBytes );
									Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
									MethodInfo method = type.GetMethod( "f_Example" );
									object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
								} );
		}

		[TestCase( "_WinAPI_EqualMemory.au3" )]
		public void Test_compile_includes4( string file ) {
			string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
			string script = File.ReadAllText( path );

			Assert.DoesNotThrow(
								() => {
									byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

									Assembly assembly = Assembly.Load( assemblyBytes );
									Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
									MethodInfo method = type.GetMethod( "f_Example" );
									object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
									object res = method.Invoke( instance, null );
								} );
		}

		[TestCase( "Benchmark.au3" )]
		public void Test_compile_Benchmark( string file ) {
			string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
			string script = File.ReadAllText( path );

			Assert.DoesNotThrow(
								() => {
									byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
									File.WriteAllBytes( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe", assemblyBytes );
									Process process = Process.Start( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe" );
									while( !process.HasExited ) {
										Thread.Sleep( 1000 );
									}
									Debug.Write( process.ExitCode );
								} );
		}

		[TestCase( "DES.au3" )]
		public void Test_compile_DES( string file ) {
			string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
			string script = File.ReadAllText( path );

			Assert.DoesNotThrow(
								() => {
									Program.Compile( "/IN", path, "/OUT", path+".exe", "/CONSOLE" );

									Process process = Process.Start( path+".exe" );

									Assert.NotNull( process );

									while( !process.HasExited ) {
										Thread.Sleep( 1000 );
									}
									Debug.Write( process.ExitCode );
								} );
		}

		[TestCase( "RC4.au3" )]
		public void Test_compile_RC4( string file ) {
			string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
			string script = File.ReadAllText( path );

			Assert.DoesNotThrow(
								() => {
									Program.Compile( "/IN", path, "/OUT", path+".exe", "/CONSOLE" );

									Process process = Process.Start( path+".exe" );

									Assert.NotNull( process );

									while( !process.HasExited ) {
										Thread.Sleep( 1000 );
									}
									Debug.Write( process.ExitCode );
								} );
		}

		[TestCase( "WinAPI.au3" )]
		public void Test_compile__WinAPI_GetCurrentProcessID( string file ) {
			string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
			string script = File.ReadAllText( path );

			var assemblyBytes = new byte[] {};
			Assert.DoesNotThrow(
								() => {
									assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
								} );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			MethodInfo methodInfo = instance.GetType().GetMethods().Single( x => x.Name.Equals( "f__WinAPI_GetCurrentProcessID" ) );
			MethodInfo _WinAPI_FloatToInt = instance.GetType().GetMethods().Single( x => x.Name.Equals( "f__WinAPI_FloatToInt" ) );
			object invoke = _WinAPI_FloatToInt.Invoke(
													  instance,
													  new[] {
														  new DoubleVariant( 1234.012335 )
													  } );

			object res = methodInfo.Invoke( instance, null );
		}

		[TestCase( "WinAPITheme.au3" )]
		public void Test_compile__WinAPI_GetCurrentThemeName( string file ) {
			string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
			string script = File.ReadAllText( path );

			var assemblyBytes = new byte[] {};
			Assert.DoesNotThrow(
								() => {
									assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
								} );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			MethodInfo methodInfo = instance.GetType().GetMethods().Single( x => x.Name.Equals( "f__WinAPI_GetCurrentThemeName" ) );
			object invoke = methodInfo.Invoke( instance, new object[0] );
		}

		[TestCase( "ContinueCase.au3" )]
		public void Test_compile_ContinueCase( string file ) {
			string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
			string script = File.ReadAllText( path );

			var assemblyBytes = new byte[] {};
			Assert.DoesNotThrow(
								() => {
									assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
									object instanceWithDefaultParameters = Assembly.Load( assemblyBytes ).GetTypes()[0].GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
									object invoke = instanceWithDefaultParameters.GetType().GetMethods().Single( x => x.Name.Contains( "Foo" ) ).Invoke( instanceWithDefaultParameters, null );
									File.WriteAllBytes( path+".exe", assemblyBytes );
								} );
		}

		[Test]
		public void Foo() {
		    var parserBootStrapper = new ParserBootStrapper();
		    var scriptParser = parserBootStrapper.GetInstance<IScriptParser>();
		    string script = "Func F()"+Environment.NewLine;
		    script += "$i = 0"+Environment.NewLine;
		    script += "EndFunc";
		    var autoitScriptRoot = scriptParser.ParseScript( script, new PragmaOptions() );
		    var rewriter = new Rewriter();
		    var newTree = rewriter.Visit( autoitScriptRoot );
            Console.Write(newTree.ToSource());
		}

	    class Rewriter:SyntaxRewriterBase
	    {
	        public override ISyntaxNode VisitAssignStatement( AssignStatement node ) {
	            return new SwitchCaseStatement( new TrueLiteralExpression(), new[] { new SwitchCase( new[] { new CaseCondition( new TrueLiteralExpression(), null ), new CaseCondition( new FalseLiteralExpression(), null ) }, Enumerable.Empty<IStatementNode>() ) }, Enumerable.Empty<IStatementNode>() );
	        }
	    }
	}
}
