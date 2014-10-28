using System;
using System.Collections.Generic;
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
            SelectLevel = 0;
            SelectLevelCount = new Dictionary<int, int>();
            CaseCount = 0;
            VariableMap = new Dictionary<string, Scope>();
            StaticVariableGuids = new Dictionary<string, Guid>();
        }

        public Dictionary<string, Guid> StaticVariableGuids { get; set; }

        public Dictionary<string, Scope> VariableMap { get; set; }
        public bool IsGlobalContext { get; set; }
        public string RuntimeInstanceName { get; private set; }
        public string ContextInstanceName { get; private set; }

        public IList<string> DeclaredVariables { get; set; }
        public IList<string> DeclaredGlobalVariables { get; set; }
        public IList<FieldDeclarationSyntax> FieldInstnaces { get; set; }
        public Dictionary<int, int> LoopLevelCount { get; set; }
        public int LoopLevel { get; set; }
        public int SelectLevel { get; set; }
        public Dictionary<int, int> SelectLevelCount { get; set; }
        public int CaseCount { get; set; }
    }
}
