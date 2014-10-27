using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    public interface IContext
    {
        bool IsGlobalContext {
            get;
            set;
        }
        string RuntimeInstanceName {
            get;
        }
        string ContextInstanceName {
            get;
        }
        IList<string> DeclaredVariables {
            get;
            set;
        }
        IList<string> DeclaredGlobalVariables {
            get;
            set;
        }
        IList<FieldDeclarationSyntax> FieldInstnaces {
            get;
            set;
        }
        Dictionary<int, int> LoopLevelCount {
            get;
            set;
        }
        int LoopLevel {
            get;
            set;
        }
        int SelectLevel {
            get;
            set;
        }
        Dictionary<int, int> SelectLevelCount {
            get;
            set;
        }
        int CaseCount {
            get;
            set;
        }
        Dictionary<string, Scope> VariableMap {
            get;
            set;
        }
    }
}
