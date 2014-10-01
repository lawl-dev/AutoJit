using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    public class ContextService : IContextService
    {
        private IContext _context;

        public void Initialize( IContext context ) {
            _context = context;
        }

        public bool GetIsGlobalContext() {
            return _context.IsGlobalContext;
        }

        public void SetGlobalContext( bool b ) {
            _context.IsGlobalContext = b;
        }

        public void Declare( string identifierName ) {
            _context.Declare( identifierName );
        }

        public string GetConinueLoopLabelName() {
            return _context.GetConinueLoopLabelName();
        }
        
        public string GetConinueLoopLabelName(int level) {
            return _context.GetConinueLoopLabelName(level);
        }

        public string GetExitLoopLabelName() {
            return _context.GetExitLoopLabelName();
        }

        public string GetExitLoopLabelName(int level)
        {
            return _context.GetExitLoopLabelName(level);
        }

        public bool IsDeclared( string identifierName ) {
            return _context.IsDeclared( identifierName );
        }

        public void PushGlobalVariable(string identifierName, FieldDeclarationSyntax instance)
        {
            _context.PushGlobalVariable( identifierName, instance );
        }

        public IEnumerable<FieldDeclarationSyntax> PopGlobalVariables() {
            return _context.PopGlobalVariables();
        }

        public void RegisterLoop() {
            _context.RegisterLoop();
        }

        public void UnregisterLoop()
        {
            _context.UnregisterLoop();
        }

        public void ResetFunctionContext() {
            _context.ResetFunctionContext();
        }

        public string GetContextInstanceName() {
            return _context.ContextInstanceName;
        }

        public string GetRuntimeInstanceName() {
            return _context.RuntimeInstanceName;
        }
    }
}