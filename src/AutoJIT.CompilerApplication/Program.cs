using System;
using System.IO;
using AutoJIT.Compiler;
using Microsoft.CodeAnalysis;

namespace AutoJIT.CompilerApplication
{
    class Program
    {
        private static ICompiler _compiler;

        public Program() {
            var bootStrapper = new CompilerBootStrapper();
            _compiler = bootStrapper.GetInstance<ICompiler>();
        }

        static void Main(string[] args)
        {
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
                    default:
                        throw new InvalidOperationException();
                }
            }

            var script = File.ReadAllText( compileOptions.InFile.ToString() );

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
                    : OutputKind.ConsoleApplication, false );

            File.WriteAllBytes( compileOptions.OutFile.AbsolutePath, assemblyBytes );
        }
    }
}
