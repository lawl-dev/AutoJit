using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoJIT.Compiler;
using AutoJIT.Infrastructure;
using AutoJITRuntime;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace UnitTests
{
	public class StatementTests : StatementTestsBase
	{
		private readonly ICompiler _compiler;

		public StatementTests() {
			var componentContainer = new CompilerBootStrapper();
			_compiler = componentContainer.GetInstance<ICompiler>();
		}

		[TestCase( "DimStatementTest_2" )]
		public void Test_DimStatementTest_2( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result == "" );
		}

		[TestCase( "test_all_the_Things" )]
		public void Test_test_all_the_Things( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			Assert.DoesNotThrow( () => _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false ) );
		}

		[TestCase( "DimStatementTest_3" )]
		public void Test_DimStatementTest_3( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result.IsArray );
		}

		[TestCase( "DimStatementTest_4" )]
		public void Test_DimStatementTest_4( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result.IsArray );
		}

		[TestCase( "WhileStatementTest_1" )]
		public void Test_WhileStatementTest_1( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result == 10 );
		}

		[TestCase( "WhileStatementTest_2" )]
		public void Test_WhileStatementTest_2( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );
			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result == 1 );
		}

		[TestCase( "ForStatementTest_1" )]
		public void Test_ForStatementTest_1( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result == 15 );
		}

		[TestCase( "FunctionCallStatementTest_1" )]
		public void Test_FunctionCallStatementTest_1( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result == 1 );
		}

		[TestCase( "IfElseIfElseStatementTest_1" )]
		public void Test_IfElseIfElseStatementTest_1( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result == 0x01 );
		}

		[TestCase( "IfElseIfElseStatementTest_2" )]
		public void Test_IfElseIfElseStatementTest_2( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result == 2 );
		}

		[TestCase( "SwitchStatementTest_1" )]
		public void Test_SwitchStatementTest_1( string file ) {
			string csresult = SwitchStatementTest_1_CSharp();

			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result == csresult );
		}

		[TestCase( "SwitchStatementTest_2" )]
		public void Test_SwitchStatementTest_2( string file ) {
			string csresult = SwitchStatementTest_1_CSharp();

			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;
			Assert.IsTrue( result == csresult );
		}

		[TestCase( "SelectStatementTest_1" )]
		public void Test_SelectStatementTest_1( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;

			int au3Res = base.ExecuteScript( file );

			Assert.IsTrue( result == au3Res );
		}

		[TestCase( "ForInStatementTest_1" )]
		public void Test_ForInStatementTest_1( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;

			int au3Res = base.ExecuteScript( file );

			Assert.IsTrue( result == au3Res );
		}

		[TestCase( "EnumStatementTest_1" )]
		public void Test_EnumStatementTest_1( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;

			int au3Res = base.ExecuteScript( file );

			Assert.IsTrue( result == au3Res );
		}

		[TestCase( "EnumStatementTest_2" )]
		public void Test_EnumStatementTest_2( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;

			int au3Res = base.ExecuteScript( file );

			Assert.IsTrue( result == au3Res );
		}

		[TestCase( "EnumStatementTest_3" )]
		public void Test_EnumStatementTest_3( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;

			int au3Res = base.ExecuteScript( file );

			Assert.IsTrue( result == au3Res );
		}

		[TestCase( "EnumStatementTest_4" )]
		public void Test_EnumStatementTest_4( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;

			int au3Res = base.ExecuteScript( file );

			Assert.IsTrue( result == au3Res );
		}

		[TestCase( "ForInStatementTest_2" )]
		public void Test_ForInStatementTest_2( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;

			Assert.IsTrue( result == 15 );
		}

		[TestCase( "LocalStatementTest_1" )]
		public void Test_LocalStatementTest_1( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;

			int au3Res = base.ExecuteScript( file );

			Assert.IsTrue( result == au3Res );
		}

		[TestCase( "ExitloopStatementTest_1" )]
		public void Test_ExitloopStatementTest_1( string file ) {
			string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file ) );
			byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

			Assembly assembly = Assembly.Load( assemblyBytes );
			Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
			MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
			object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
			var result = method.Invoke( instance, null ) as Variant;

			int au3Res = base.ExecuteScript( file );

			Assert.IsTrue( result == au3Res );
		}

		private string SwitchStatementTest_1_CSharp() {
			int hour = DateTime.Now.Hour;
			if( hour >= 6
				&& hour <= 11 ) {
				return "Good Morning";
			}
			if( hour >= 12
				&& hour <= 17 ) {
				return "Good Afternoon";
			}
			if( hour >= 18
				&& hour <= 21 ) {
				return "Good Evening";
			}
			return "What are you still doing up?";
		}
	}
}
