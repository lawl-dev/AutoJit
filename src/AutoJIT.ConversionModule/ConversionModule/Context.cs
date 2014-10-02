using System.Collections.Generic;
using System.Globalization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    internal sealed class Context : IContext
    {
        public Context( string runtimeInstanceName, string contextInstanceName ) {
            RuntimeInstanceName = runtimeInstanceName;
            ContextInstanceName = contextInstanceName;
            DeclaredVariables = new List<string>();
            IsGlobalContext = false;
            DeclaredGlobalVariables = new List<string>();
            FieldInstnaces = new List<FieldDeclarationSyntax>();
            LoopLevelCount = new Dictionary<int, int>();
            LoopLevel = 0;
        }

        public bool IsGlobalContext { get; set; }
        public string RuntimeInstanceName { get; private set; }
        public string ContextInstanceName { get; private set; }

        public IList<string> DeclaredVariables { get; set; }
        public IList<string> DeclaredGlobalVariables { get; set; }
        public IList<FieldDeclarationSyntax> FieldInstnaces { get; set; }
        public Dictionary<int, int> LoopLevelCount { get; set; }
        public int LoopLevel { get; set; }
    }
}
