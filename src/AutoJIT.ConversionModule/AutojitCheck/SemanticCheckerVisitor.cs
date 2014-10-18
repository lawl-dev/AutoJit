﻿using System.Collections.Generic;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.CSharpConverter.AutojitCheck
{
    public class SemanticCheckerVisitor : ISyntaxVisitor
    {
        private readonly Dictionary<string, List<LocalDeclarationStatement>> _constLocal = new Dictionary<string, List<LocalDeclarationStatement>>();
        private readonly Dictionary<string, GlobalDeclarationStatement> _constGlobal = new Dictionary<string, GlobalDeclarationStatement>();

        public void Visit( ISyntaxNode node ) {
            Visit( (dynamic) node );
        }

        public void Visit( object o ) {}

        public void Visit( GlobalDeclarationStatement global ) {
            if ( _constGlobal.ContainsKey( global.VariableExpression.IdentifierName ) ) {
                throw new InvalidSemanticException( string.Format( "{0} previously declared as a 'Const'", global.VariableExpression.IdentifierName ) );
            }
            _constGlobal.Add( global.VariableExpression.IdentifierName, global );
        }
    }
}
