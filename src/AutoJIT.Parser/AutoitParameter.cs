using System.Collections.Generic;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser
{
    public class AutoitParameter : SyntaxNodeBase
    {
        public IExpressionNode DefaultValue { get; private set; }
        public bool IsByRef { get; private set; }
        public bool IsConst { get; private set; }
        public TokenNode ParameterName { get; private set; }

        public AutoitParameter( TokenNode parameterName, IExpressionNode defaultValue, bool isByRef, bool isConst ) {
            IsConst = isConst;
            ParameterName = parameterName;
            DefaultValue = defaultValue;
            IsByRef = isByRef;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( DefaultValue );
                return nodes;
            }
        }

        public override string ToSource() {
            var toReturn = string.Empty;
            if ( IsByRef ) {
                toReturn += "ByRef ";
            }
            if ( IsConst ) {
                toReturn += "Const ";
            }
            toReturn += ParameterName.ToSource();
            toReturn += " ";
            if ( DefaultValue != null ) {
                toReturn += " = " + DefaultValue.ToSource();
            }
            return toReturn;
        }

        public override object Clone() {
            var autoitParameter = new AutoitParameter(
                (TokenNode) ParameterName.Clone(), DefaultValue != null
                    ? (IExpressionNode) DefaultValue.Clone()
                    : null, IsByRef, IsConst );

            autoitParameter.Initialize();
            return autoitParameter;
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitAutoitParameter(this);
        }

        public ISyntaxNode Update( TokenNode parameterName, IExpressionNode defaultValue, bool isByRef, bool isConst ) {
            if ( ParameterName == parameterName &&
                 DefaultValue == defaultValue &&
                 isByRef == IsByRef &&
                 IsConst == isConst ) {
                return this;
            }
            var parameter = new AutoitParameter(
                (TokenNode) parameterName.Clone(), defaultValue == null
                    ? null
                    : (IExpressionNode) defaultValue.Clone(), isByRef, isConst );

            parameter.Initialize();
            return parameter;
        }
    }
}
