using System;
using System.Collections.Generic;
using System.IO;
using AutoJIT.Compiler;
using AutoJITRuntime.Variants;
using ILRepacking;
using Lawl.Reflection;
using Microsoft.CodeAnalysis;

namespace AutoJIT.CompilerApplication
{
    public class Program
    {
        private static readonly ICompiler _compiler;

        static Program() {
            var bootStrapper = new CompilerBootStrapper();
            _compiler = bootStrapper.GetInstance<ICompiler>();
        }

        private static void Main( string[] args ) {
            Compile( args );
        }

        public static void Compile( params string[] args ) {
            var compileOptions = new CompileOptions();
            for( int i = 0; i < args.Length; i++ ) {
                switch(args[i].ToUpper()) {
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

            string script = File.ReadAllText( compileOptions.InFile.AbsolutePath );

            if( compileOptions.IsConsole
                && compileOptions.IsForms ) {
                throw new InvalidOperationException();
            }

            if( !compileOptions.IsConsole
                && !compileOptions.IsForms ) {
                compileOptions.IsForms = true;
            }
            byte[] assemblyBytes = _compiler.Compile(
                                                     script,
                                                     compileOptions.IsForms
                                                     ? OutputKind.WindowsApplication
                                                     : OutputKind.ConsoleApplication,
                                                     false,
                                                     compileOptions.Optimize );

            var toMerge = new List<string>();
            string tempPath = Path.Combine( Path.GetTempPath(), Guid.NewGuid().ToString( "n" ) );
            File.WriteAllBytes( tempPath, assemblyBytes );

            toMerge.Add( tempPath );
            toMerge.Add( typeof(StringVariant).Assembly.Location );
            toMerge.Add( typeof(TypeExtensions).Assembly.Location );

            File.Delete( compileOptions.OutFile.AbsolutePath );
            var repack = new ILRepack {
                OutputFile = compileOptions.OutFile.AbsolutePath,
                TargetKind = ILRepack.Kind.Exe,
                InputAssemblies = toMerge.ToArray()
            };
            repack.Repack();
        }
    }
}
