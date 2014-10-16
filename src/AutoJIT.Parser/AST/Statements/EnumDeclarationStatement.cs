using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class EnumDeclarationStatement : StatementBase
    {
        public IExpressionNode AutoInitExpression { get; private set; }
        public bool IsGlobal { get; private set; }
        public VariableExpression VariableExpression { get; private set; }
        public IExpressionNode UserInitExpression { get; private set; }

        public EnumDeclarationStatement(
            VariableExpression variableExpression,
            IExpressionNode userInitExpression,
            IExpressionNode autoInitExpression,
            bool isGlobal ) {
            AutoInitExpression = autoInitExpression;
            IsGlobal = isGlobal;
            VariableExpression = variableExpression;
            UserInitExpression = userInitExpression;
            Initialize();
        }

        public override string ToSource() {
            throw new NotImplementedException();
        }

        public override object Clone() {
            return new EnumDeclarationStatement(
                (VariableExpression) VariableExpression.Clone(), CloneAs<IExpressionNode>( UserInitExpression), CloneAs<IExpressionNode>( AutoInitExpression),
                IsGlobal );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get { return new List<ISyntaxNode>() { AutoInitExpression, VariableExpression, UserInitExpression }; }
        }
    }
}
