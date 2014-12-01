using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using AutoJIT.Compiler;
using AutoJIT.CompilerApplication;
using AutoJIT.Contrib;
using AutoJIT.Parser;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;
using AutoJITRuntime.Variants;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace IntegrationTests
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
                    MethodInfo method = type.GetMethod( "Example" );
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
                    MethodInfo method = type.GetMethod( "Example" );
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
                    MethodInfo method = type.GetMethod( "Example" );
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
                    while ( !process.HasExited ) {
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

                    while ( !process.HasExited ) {
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

                    while ( !process.HasExited ) {
                        Thread.Sleep( 1000 );
                    }
                    Debug.Write( process.ExitCode );
                } );
        }

        [TestCase( "WinAPI.au3" )]
        public void Test_compile__WinAPI_GetCurrentProcessID( string file ) {
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            string script = File.ReadAllText( path );

            var assemblyBytes = new byte[] { };
            Assert.DoesNotThrow(
                () => { assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false ); } );

            Assembly assembly = Assembly.Load( assemblyBytes );
            Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
            object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
            MethodInfo methodInfo = instance.GetType().GetMethods().Single( x => x.Name.Equals( "_WinAPI_GetCurrentProcessID" ) );
            MethodInfo _WinAPI_FloatToInt = instance.GetType().GetMethods().Single( x => x.Name.Equals( "_WinAPI_FloatToInt" ) );
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

            var assemblyBytes = new byte[] { };
            Assert.DoesNotThrow(
                () => { assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false ); } );

            Assembly assembly = Assembly.Load( assemblyBytes );
            Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
            object instance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
            MethodInfo methodInfo = instance.GetType().GetMethods().Single( x => x.Name.Equals( "_WinAPI_GetCurrentThemeName" ) );
            object invoke = methodInfo.Invoke( instance, new object[0] );
        }

        [TestCase( "ContinueCase.au3" )]
        public void Test_compile_ContinueCase( string file ) {
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            string script = File.ReadAllText( path );

            var assemblyBytes = new byte[] { };
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
            string script = File.ReadAllText( @"C:\Users\Brunnmeier\Documents\PrivateGIT\OPENSOURCE\Autojit\src\IntegrationTests\testdata\userfunctions\DES.au3" );
            AutoitScriptRoot autoitScriptRoot = scriptParser.ParseScript( script, new PragmaOptions() );
            new ConsoleDumpWalker().Visit( autoitScriptRoot );

        }

        [Test]
        public void Foo3() {
            var methodInfos = AppDomain.CurrentDomain.GetAssemblies().SelectMany( x => x.GetTypes() ).Where( x => x.IsPublic && x.Namespace.StartsWith( "System" )).SelectMany( x => x.GetMethods() ).Where( x =>x.IsPublic ).ToList();

            var allowedTypes = new List<Type>() {
                typeof (int),
                typeof (Int64),
                typeof (double),
                typeof (float),
                typeof (byte[]),
                typeof (int[]),
                typeof (Int64[]),
                typeof (double[]),
                typeof (float[]),
                typeof (string[]),
                typeof (string),
                typeof (bool),
                typeof (bool[])
            };

            var withParameters = methodInfos.Where( x=>x.IsStatic && x.GetParameters().All( y=>allowedTypes.Contains(y.ParameterType) || y.ParameterType.IsEnum) ).ToList();

            var infos = withParameters.Where( x=>allowedTypes.Contains(x.ReturnType) || x.ReturnType.IsEnum).ToList();

            var statics = infos.Where( x=>!x.Name.StartsWith( "get_" ) && !x.Name.StartsWith( "set_" ) && !x.Name.StartsWith( "op_" )).ToList();

            var nonstatics = methodInfos.Where(x =>x.GetParameters().All(y => allowedTypes.Contains(y.ParameterType) || y.ParameterType.IsEnum)).ToList();

            var nstatics = nonstatics.Where( x=>allowedTypes.Contains(x.DeclaringType) && allowedTypes.Contains(x.ReturnType) || x.ReturnType.IsEnum).ToList();

            var res = nstatics.Where( x=>!x.Name.StartsWith( "get_" ) && !x.Name.StartsWith( "set_" ) && !x.Name.StartsWith( "op_" )).ToList();

            var enumerable = typeof(object).GetMethods().Select( x=>x.Name ).ToList();

            var list = res.Where( x=>!enumerable.Contains(x.Name) ).ToList();


            string cl = string.Empty;

            var dictionary = new Dictionary<string, bool>();
            foreach (var info in statics)
            {
                if(dictionary.ContainsKey( info.Name +  info.GetParameters().Length )) continue;
                dictionary.Add( info.Name + info.GetParameters().Length, true );

                string mt = string.Empty;
                var @join = string.Join(", ", info.GetParameters().Select( x=>"Variant " + x.Name ));
                var s = string.Join(", ", info.GetParameters().Select( x=>x.ParameterType.IsEnum ? "(" + x.ParameterType.Name +")(int)" + x.Name: "(" + x.ParameterType +  ")" + x.Name ));

                mt += string.Format( "public Variant {0}{1}({2})", info.DeclaringType.Name, info.Name, @join );
                mt += "{";
                if ( info.ReturnType.IsEnum ) {
                    mt += string.Format("return Variant.Create((int){0}.{1}({2}));", info.DeclaringType, info.Name, s);
                }
                else {
                    mt += string.Format("return Variant.Create({0}.{1}({2}));", info.DeclaringType, info.Name, s);
                }
                mt += "}";
                cl += mt;
            }

            foreach (var info in list) {
                if (dictionary.ContainsKey(info.Name + info.GetParameters().Length)) continue;
                dictionary.Add(info.Name + info.GetParameters().Length, true);

                string mt = string.Empty;
                var @join = string.Join( ", ", ( "Variant "+info.DeclaringType.Name ).ToEnumerable().Concat( info.GetParameters().Select( x => "Variant "+x.Name ) ) );

                var s = string.Join(", ", info.GetParameters().Select(x => x.ParameterType.IsEnum ? "(" + x.ParameterType.Name + ")(int)" + x.Name : "(" + x.ParameterType + ")" + x.Name));

                mt += string.Format("public Variant {0}{1}({2})", info.DeclaringType.Name, info.Name, @join);
                mt += "{";
                if (info.ReturnType.IsEnum)
                {
                    mt += string.Format("return Variant.Create((int){0}.{1}({2}));", info.DeclaringType.Name, info.Name, s);
                }
                else
                {
                    mt += string.Format("return Variant.Create((({0}){1}).{2}({3}));", info.DeclaringType, info.DeclaringType.Name, info.Name, s);
                }
                mt += "}";
                cl += mt;
            }
        }


        [Test]
        public void Foo2() {
            var parserBootStrapper = new ParserBootStrapper();
            var scriptParser = parserBootStrapper.GetInstance<IScriptParser>();
            string script = File.ReadAllText( @"C:\Users\Brunnmeier\Documents\PrivateGIT\OPENSOURCE\Autojit\src\IntegrationTests\testdata\Statements\test_all_the_things" );
            AutoitScriptRoot autoitScriptRoot = scriptParser.ParseScript( script, new PragmaOptions() );

            var list = new List<List<NodeVisualizer>>();
            GetMap( autoitScriptRoot, 1, list );

            int sum = list.First( x => x.Count == list.Max( y => y.Count ) ).Sum( x => x.witdh );

            int i = ( list[0][0].height ) * list.Count * 40;

            var bitmap = new Bitmap( sum, i );
            Graphics fromImage = Graphics.FromImage( bitmap );
            int ypos = 0;
            foreach (var level in list) {
                int wi = level.Sum( x => x.witdh );
                int xpos = ( ( sum-wi ) / 2 );

                foreach (NodeVisualizer node in level) {
                    fromImage.DrawString( node.String, new Font( FontFamily.GenericMonospace, 10 ), new SolidBrush( Color.Blue ), (float) ( xpos+( ( node.witdh / 1.5 ) / 4 ) ), ypos );

                    fromImage.DrawString( node.String2, new Font( FontFamily.GenericMonospace, 10 ), new SolidBrush( Color.Blue ), (float) ( xpos+( ( node.witdh / 1.5 ) / 4 ) ), ypos+node.height * 2 );

                    fromImage.DrawRectangle( new Pen( Color.CadetBlue ), xpos, ypos, node.witdh, node.height );
                    node.SetPos( xpos, ypos );
                    xpos += node.witdh;
                }
                ypos += i / list.Count;
            }

            foreach (var level in list) {
                foreach (NodeVisualizer nodeVisualizer in level) {
                    if ( nodeVisualizer.Parent != null ) {
                        int hashCode = nodeVisualizer.Parent.GetHashCode();
                        Color fromArgb = Color.FromArgb( hashCode );
                        fromArgb = Color.FromArgb( 255, fromArgb );

                        fromImage.DrawLine( new Pen( fromArgb ), nodeVisualizer.XPOS+nodeVisualizer.witdh / 2, nodeVisualizer.YPOS, nodeVisualizer.Parent.XPOS+nodeVisualizer.Parent.witdh / 2, nodeVisualizer.Parent.YPOS+nodeVisualizer.Parent.height );
                    }
                }
            }
            fromImage.Flush();
            bitmap.Save( @"C:\Users\Brunnmeier\Desktop\backup\lol\lolololoöö.png", ImageFormat.Png );
        }

        private static SizeF MeasureString( string s ) {
            var font = new Font( FontFamily.GenericMonospace, 10 );
            SizeF result;
            using ( var image = new Bitmap( 1, 1 ) ) {
                using ( Graphics g = Graphics.FromImage( image ) ) {
                    result = g.MeasureString( s, font );
                }
            }

            return result;
        }

        private void GetMap( ISyntaxNode node, int level, List<List<NodeVisualizer>> list, NodeVisualizer parent = null ) {
            if ( list.Count < level ) {
                list.Add( new List<NodeVisualizer>() );
            }

            var nodeVisualizer = new NodeVisualizer( node, parent );

            list[level-1].Add( nodeVisualizer );

            ++level;
            foreach (ISyntaxNode child in node.Children.Where( x => x != null )) {
                GetMap( child, level, list, nodeVisualizer );
            }
        }

        private class NodeVisualizer
        {
            private readonly ISyntaxNode _node;
            public int XPOS;
            public int YPOS;

            public NodeVisualizer( ISyntaxNode node, NodeVisualizer parent ) {
                Parent = parent;
                _node = node;
            }

            public NodeVisualizer Parent { get; set; }

            public int witdh {
                get { return (int) ( MeasureString( _node.GetType().Name ).Width * 1.5 ); }
            }

            public int height {
                get { return (int) MeasureString( _node.GetType().Name ).Height; }
            }

            public string String {
                get { return _node.GetType().Name; }
            }

            public string String2 {
                get {
                    return _node is IExpressionNode || _node is TokenNode
                        ? _node.ToSource()
                        : string.Empty;
                }
            }

            public void SetPos( int xpos, int ypos ) {
                XPOS = xpos;
                YPOS = ypos;
            }
        }
    }

    class ConsoleDumpWalker : SyntaxWalkerBase
    {
        public override void Visit( ISyntaxNode node ) {
            int padding = node.Ancestors().Count();
            //To identify leaf nodes vs nodes with children
            string prepend = node.Children.Any() ? "[>]" : "[-]";
            //Get the type of the node
            string line = new String(' ', padding * 3) + prepend +
                                    " " + node.GetType().ToString();
            //Write the line
            System.Console.WriteLine(line);
            base.Visit(node);
        }
    }
}
