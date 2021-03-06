using System.IO;
using AutoJIT.CSharpConverter.ConversionModule;
using AutoJIT.CSharpConverter.ConversionModule.Optimizer;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Lex.Interface;
using AutoJITRuntime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;

namespace AutoJIT.Compiler
{
    public sealed class Compiler : ICompiler
    {
        private readonly IAutoitToCSharpConverter _autoitToCSharpConverter;
        private readonly IContinueCaseMsilFixingService _continueCaseMsilFixingService;
        private readonly IOptimizer _optimizer;
        private readonly IPragmaParser _pragmaParser;
        private readonly IScriptParser _scriptParser;

        public Compiler( IOptimizer optimizer, IScriptParser scriptParser, IPragmaParser pragmaParser, IAutoitToCSharpConverter autoitToCSharpConverter, IContinueCaseMsilFixingService continueCaseMsilFixingService ) {
            _optimizer = optimizer;
            _scriptParser = scriptParser;
            _pragmaParser = pragmaParser;
            _autoitToCSharpConverter = autoitToCSharpConverter;
            _continueCaseMsilFixingService = continueCaseMsilFixingService;
        }

        public byte[] Compile( string script, OutputKind outputKind, bool warningAsError, bool optimize = false, string assemblyName = "AutoJITAssembly" ) {
            var pragmaOptions = new PragmaOptions();
            script = _pragmaParser.IncludeDependenciesAndResolvePragmas( script, pragmaOptions );

            AutoitScriptRoot autoJITScript = _scriptParser.ParseScript( script, pragmaOptions );

            NamespaceDeclarationSyntax cSharpTree = _autoitToCSharpConverter.Convert( autoJITScript );

            CompilationUnitSyntax compilationUnit = SyntaxFactory.CompilationUnit().AddMembers( cSharpTree ).AddUsings( SyntaxFactory.UsingDirective( SyntaxFactory.IdentifierName( typeof (AutoitRuntime<>).Namespace ) ), SyntaxFactory.UsingDirective( SyntaxFactory.IdentifierName( typeof (Variant).Namespace ) ), SyntaxFactory.UsingDirective( SyntaxFactory.IdentifierName( typeof (object).Namespace ) ) );

            SyntaxNode root = compilationUnit.SyntaxTree.GetRoot();

            if ( optimize ) {
                root = _optimizer.Optimize( root );
            }

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                new[] {
                    root.SyntaxTree
                },
                null,
                new CSharpCompilationOptions( outputKind, optimize: true, platform: Platform.X86 ) );

            compilation = compilation.WithReferences( new MetadataFileReference( typeof (object).Assembly.Location ), new MetadataFileReference( typeof (Variant).Assembly.Location ) );

            var outputStream = new MemoryStream();

            EmitResult emit = compilation.Emit( outputStream );

            if ( emit.Success ) {
                byte[] assembly = outputStream.ToArray();
                return _continueCaseMsilFixingService.Fix( assembly, "AutoJITScriptClass" );
            }

            throw new EmitException( emit.Diagnostics );
        }
    }
}
