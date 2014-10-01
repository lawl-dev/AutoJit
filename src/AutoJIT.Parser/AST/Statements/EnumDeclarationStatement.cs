using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class EnumDeclarationStatement : StatementBase
    {
        public readonly IExpressionNode AutoInitExpression;
        public readonly bool IsGlobal;
        public readonly VariableExpression VariableExpression;
        public readonly IExpressionNode UserInitExpression;

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
                (VariableExpression) VariableExpression.Clone(), (IExpressionNode) UserInitExpression.Clone(), (IExpressionNode) AutoInitExpression.Clone(),
                IsGlobal );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get { return new List<ISyntaxNode>() { AutoInitExpression, VariableExpression, UserInitExpression }; }
        }
    }
}
