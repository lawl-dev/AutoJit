using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoJIT.Compiler;
using AutoJITRuntime;
using ILRepacking;
using Microsoft.CodeAnalysis;

namespace AutoJIT.CompilerApplication
{
    class Program
    {
        private static ICompiler _compiler;

        static Program() {
            var bootStrapper = new CompilerBootStrapper();
            _compiler = bootStrapper.GetInstance<ICompiler>();
        }

        static void Main(string[] args) {
            args = new[] {
                "/in",
                @"C:\Users\Brunnmeier\Documents\PrivateGIT\OPENSOURCE\Autojit\src\IntegrationTests\testdata\userfunctions\DES.au3",
                "/Out",
                @"C:\Users\Brunnmeier\Documents\PrivateGIT\OPENSOURCE\Autojit\src\IntegrationTests\testdata\userfunctions\des.exe",
                "/console"
            };
            var compileOptions = new CompileOptions();
            for ( int i = 0; i < args.Length; i++ ) {
                switch (args[i].ToUpper()) {
                    case "/IN":
                        compileOptions.InFile = new Uri( args[++i] );
                        break;
                    case "/OUT":
                        compileOptions.OutFile = new Uri( args[++i] );
                        break;
                    case "/ICON":
                        compileOptions.Icon = new Uri( args[++i] );
                        break;
                    case "/CONSOLE":
                        compileOptions.IsConsole = true;
                        break;
                    case "/GUI":
                        compileOptions.IsForms = true;
                        break;
                    case "/OPT":
                        compileOptions.Optimize = true;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            var script = File.ReadAllText( compileOptions.InFile.AbsolutePath );

            if ( compileOptions.IsConsole &&
                 compileOptions.IsForms ) {
                throw new InvalidOperationException();
            }

            if ( !compileOptions.IsConsole &&
                 !compileOptions.IsForms ) {
                compileOptions.IsForms = true;
            }
            var assemblyBytes = _compiler.Compile(
                script, compileOptions.IsForms
                    ? OutputKind.WindowsApplication
                    : OutputKind.ConsoleApplication, false, compileOptions.Optimize );

            var toMerge = new List<string>();
            var tempPath = Path.Combine( Path.GetTempPath(), Guid.NewGuid().ToString( "n" ) );
            File.WriteAllBytes( tempPath, assemblyBytes );


            toMerge.Add( tempPath );
            toMerge.Add( typeof(StringVariant).Assembly.Location );

            var repack = new ILRepack { OutputFile = compileOptions.OutFile.AbsolutePath, TargetKind = ILRepack.Kind.Exe, InputAssemblies = toMerge.ToArray() };
            repack.Repack();
        }
    }
}
