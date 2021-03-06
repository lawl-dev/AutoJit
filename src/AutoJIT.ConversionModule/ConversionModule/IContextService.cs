using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    public interface IContextService
    {
        void RegisterLocal( string identifierName );
        string GetConinueLoopLabelName();
        string GetConinueLoopLabelName( int level );
        string GetExitLoopLabelName();
        string GetExitLoopLabelName( int level );
        bool IsDeclared( string identifierName );
        void PushGlobalVariable( string identifierName, FieldDeclarationSyntax instance );
        IEnumerable<FieldDeclarationSyntax> PopGlobalVariables();
        void RegisterLoop();
        void UnregisterLoop();
        void ResetFunctionContext();
        string GetContextInstanceName();
        string GetRuntimeInstanceName();
        void Initialize( IContext context );
        bool GetIsGlobalContext();
        void SetGlobalContext( bool b );
        void UnregisterSelectSwitch();
        void RegisterSelectSwitch();
        void RegisterCase();
        string GetContinueCaseLabelName();
        string GetContinueCaseLabelName( int i );
        string GetVariableName( string key );
        string GetVariableName( string key, Scope scope );
        bool IsDeclaredLocal( string identifierName );
        void RegisterGlobal( string identifierName );
        bool IsDeclaredGlobal( string identifierName );
        void RegisterStatic( string identifierName );
        bool IsDeclaredStatic( string identifierName );
    }
}
