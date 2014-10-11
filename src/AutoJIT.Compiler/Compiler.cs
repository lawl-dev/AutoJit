using System.IO;
using AutoJIT.CSharpConverter.ConversionModule;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Lex.Interface;
using AutoJIT.Parser.Optimizer;
using AutoJITRuntime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AutoJIT.Compiler
{
    public sealed class Compiler : ICompiler
    {
        private readonly IOptimizer _optimizer;
        private readonly IScriptParser _scriptParser;
        private readonly IPragmaParser _pragmaParser;
        private readonly IAutoitToCSharpConverter _autoitToCSharpConverter;

        public Compiler(
            IOptimizer optimizer,
            IScriptParser scriptParser,
            IPragmaParser pragmaParser,
            IAutoitToCSharpConverter autoitToCSharpConverter) {
            _optimizer = optimizer;
            _scriptParser = scriptParser;
            _pragmaParser = pragmaParser;
            _autoitToCSharpConverter = autoitToCSharpConverter;
        }

        public byte[] Compile( string script, OutputKind outputKind, bool warningAsError, bool optimize = false, string assemblyName = "AutoJITAssembly" ) {
            var pragmaOptions = new PragmaOptions();
            script = _pragmaParser.IncludeDependenciesAndResolvePragmas(script, pragmaOptions);

            var autoJITScript = _scriptParser.ParseScript(script, pragmaOptions);
           

            var cSharpTree = _autoitToCSharpConverter.Convert( autoJITScript );

            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers( cSharpTree )
                .AddUsings(
                    SyntaxFactory.UsingDirective( SyntaxFactory.IdentifierName( typeof (AutoitRuntime<>).Namespace ) ),
                    SyntaxFactory.UsingDirective( SyntaxFactory.IdentifierName( typeof (Int32Variant).Namespace ) ),
                    SyntaxFactory.UsingDirective( SyntaxFactory.IdentifierName( typeof (object).Namespace ) ) );
            

            
            var root = compilationUnit.SyntaxTree.GetRoot();
            
            if ( optimize ) {
                root = _optimizer.Optimize( root );
            }

            var compilation = CSharpCompilation.Create(
                assemblyName, new[] { root.SyntaxTree }, null, new CSharpCompilationOptions( outputKind, optimize: true,platform:Platform.X86 ) );

            compilation = compilation
                .WithReferences(
                    new MetadataFileReference( typeof (object).Assembly.Location ),
                    new MetadataFileReference( typeof (Int32Variant).Assembly.Location ) );

            
            var outputStream = new MemoryStream();

            var emit = compilation.Emit( outputStream );

            if ( emit.Success ) {
                return outputStream.ToArray();
            }

            throw new EmitException( emit.Diagnostics );
        }
    }
}
