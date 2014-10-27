using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public abstract class EnumDeclarationStatement : StatementBase
    {
        protected EnumDeclarationStatement(
            VariableExpression variableExpression,
            IExpressionNode userInitExpression,
            IExpressionNode autoInitExpression ) {
            AutoInitExpression = autoInitExpression;
            VariableExpression = variableExpression;
            UserInitExpression = userInitExpression;
            Initialize();
        }

        public IExpressionNode AutoInitExpression { get; private set; }
        public VariableExpression VariableExpression { get; private set; }
        public IExpressionNode UserInitExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode> { AutoInitExpression, VariableExpression, UserInitExpression }; }
        } 

        public override string ToSource() {
            throw new NotImplementedException();
        }
    }
}
