using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    public interface IContext
    {
        bool IsGlobalContext { get; set; }
        string RuntimeInstanceName { get; }
        string ContextInstanceName { get; }
        bool IsDeclared( string identifierName );
        void Declare( string identifierName );
        void PushGlobalVariable( string identifierName, FieldDeclarationSyntax instance );
        IEnumerable<FieldDeclarationSyntax> PopGlobalVariables();
        void ResetFunctionContext();
        void RegisterLoop();
        void UnregisterLoop();
        string GetExitLoopLabelName( int level );
        string GetExitLoopLabelName();
        string GetConinueLoopLabelName( int level );
        string GetConinueLoopLabelName();
    }
}
