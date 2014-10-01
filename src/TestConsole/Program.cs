using System;
using System.Linq;
using System.IO;
using AutoJIT;
using AutoJIT.Compiler;
using Microsoft.CodeAnalysis;

namespace TestConsole
{
    internal class Program
    {
        private static readonly ICompiler Compiler = new CompilerBootStrapper().GetInstance<ICompiler>();

        private static void Main(string[] args) {
            if ( !args.Any() ) {
                Console.WriteLine("Please enter a filename");
                return;
            }

            if ( !File.Exists( args.First() ) ) {
                Console.WriteLine("File not found: " + args.First());
                return;
            }

            try {
                var assembly = Compiler.Compile(File.ReadAllText(args.First()), OutputKind.ConsoleApplication, false, true);
                File.WriteAllBytes( args.Last(), assembly );
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
