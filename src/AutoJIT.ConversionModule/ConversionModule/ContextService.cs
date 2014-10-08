using System.Collections.Generic;
using System.Globalization;
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
            _context.DeclaredVariables.Add(identifierName);
        }

        public string GetConinueLoopLabelName() {
            return string.Format(
                "ConinueLoop_level_{0}_count_{1}", _context.LoopLevel.ToString(CultureInfo.InvariantCulture), _context.LoopLevelCount.ContainsKey(_context.LoopLevel)
                    ? _context.LoopLevelCount[_context.LoopLevel]
                    : 0);
        }
        
        public string GetConinueLoopLabelName(int level) {
            return string.Format(
                "ConinueLoop_level_{0}_count_{1}", (_context.LoopLevel - level + 1).ToString(CultureInfo.InvariantCulture),
                _context.LoopLevelCount.ContainsKey(_context.LoopLevel - level + 1)
                    ? _context.LoopLevelCount[_context.LoopLevel - level + 1]
                    : 0);
        }

        public string GetExitLoopLabelName() {
            return string.Format(
                "ExitLooP_level_{0}_count_{1}", _context.LoopLevel.ToString(CultureInfo.InvariantCulture), _context.LoopLevelCount.ContainsKey(_context.LoopLevel)
                ? _context.LoopLevelCount[_context.LoopLevel]
                    : 0);
        }

        public string GetExitLoopLabelName(int level)
        {
            return string.Format(
                "ExitLooP_level_{0}_count_{1}", (_context.LoopLevel - level + 1).ToString(CultureInfo.InvariantCulture),
                _context.LoopLevelCount.ContainsKey(_context.LoopLevel - level + 1)
                    ? _context.LoopLevelCount[_context.LoopLevel - level + 1]
                    : 0);
        }

        public bool IsDeclared( string identifierName ) {
            return _context.DeclaredVariables.Contains( identifierName ) || _context.DeclaredGlobalVariables.Contains( identifierName );
        }

        public void PushGlobalVariable(string identifierName, FieldDeclarationSyntax instance)
        {
            _context.FieldInstnaces.Add(instance);
            _context.DeclaredGlobalVariables.Add(identifierName);
        }

        public IEnumerable<FieldDeclarationSyntax> PopGlobalVariables() {
            return _context.FieldInstnaces;
        }

        public void RegisterLoop() {
            _context.LoopLevel++;
            if (_context.LoopLevelCount.ContainsKey(_context.LoopLevel))
            {
                _context.LoopLevelCount[_context.LoopLevel]++;
            }
            else
            {
                _context.LoopLevelCount.Add(_context.LoopLevel, 0);
            }
        }

        public void UnregisterLoop()
        {
            _context.LoopLevel--;
        }

        public void ResetFunctionContext() {
            _context.IsGlobalContext = false;
            _context.DeclaredVariables.Clear();
            _context.FieldInstnaces.Clear();
            _context.LoopLevel = 0;
            _context.LoopLevelCount.Clear();
        }

        public string GetContextInstanceName() {
            return _context.ContextInstanceName;
        }

        public string GetRuntimeInstanceName() {
            return _context.RuntimeInstanceName;
        }
    }
}