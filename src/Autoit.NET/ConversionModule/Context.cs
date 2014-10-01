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
            _declaredVariables = new List<string>();
            IsGlobalContext = false;
        }

        public bool IsGlobalContext { get; set; }
        public string RuntimeInstanceName { get; private set; }
        public string ContextInstanceName { get; private set; }

        private readonly IList<string> _declaredVariables = new List<string>();
        private readonly IList<string> _declaredGlobalVariables = new List<string>();
        private readonly IList<FieldDeclarationSyntax> _fieldInstnaces = new List<FieldDeclarationSyntax>();
        private readonly Dictionary<int, int> _loopLevelCount = new Dictionary<int, int>();
        private int _loopLevel = 0;

        public bool IsDeclared( string identifierName ) {
            return _declaredVariables.Contains( identifierName ) || _declaredGlobalVariables.Contains( identifierName );
        }

        public void Declare( string identifierName ) {
            _declaredVariables.Add( identifierName );
        }

        public void PushGlobalVariable( string identifierName, FieldDeclarationSyntax instance ) {
            _fieldInstnaces.Add( instance );
            _declaredGlobalVariables.Add( identifierName );
        }

        public IEnumerable<FieldDeclarationSyntax> PopGlobalVariables() {
            return _fieldInstnaces;
        }

        public void ResetFunctionContext() {
            IsGlobalContext = false;
            _declaredVariables.Clear();
            _fieldInstnaces.Clear();
            _loopLevel = 0;
            _loopLevelCount.Clear();
        }

        public void RegisterLoop() {
            _loopLevel++;
            if ( _loopLevelCount.ContainsKey( _loopLevel ) ) {
                _loopLevelCount[_loopLevel]++;
            }
            else {
                _loopLevelCount.Add( _loopLevel, 0 );
            }
        }

        public void UnregisterLoop() {
            _loopLevel--;
        }

        public string GetExitLoopLabelName( int level ) {
            return string.Format(
                "ExitLooP_level_{0}_count_{1}", ( _loopLevel-level+1 ).ToString( CultureInfo.InvariantCulture ),
                _loopLevelCount.ContainsKey( _loopLevel-level+1 )
                    ? _loopLevelCount[_loopLevel-level+1]
                    : 0 );
        }

        public string GetExitLoopLabelName() {
            return string.Format(
                "ExitLooP_level_{0}_count_{1}", _loopLevel.ToString( CultureInfo.InvariantCulture ), _loopLevelCount.ContainsKey( _loopLevel )
                    ? _loopLevelCount[_loopLevel]
                    : 0 );
        }

        public string GetConinueLoopLabelName( int level ) {
            return string.Format(
                "ConinueLoop_level_{0}_count_{1}", ( _loopLevel-level+1 ).ToString( CultureInfo.InvariantCulture ),
                _loopLevelCount.ContainsKey( _loopLevel-level+1 )
                    ? _loopLevelCount[_loopLevel-level+1]
                    : 0 );
        }

        public string GetConinueLoopLabelName() {
            return string.Format(
                "ConinueLoop_level_{0}_count_{1}", _loopLevel.ToString( CultureInfo.InvariantCulture ), _loopLevelCount.ContainsKey( _loopLevel )
                    ? _loopLevelCount[_loopLevel]
                    : 0 );
        }
    }
}
