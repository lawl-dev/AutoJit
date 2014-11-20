using System.Collections.Generic;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser
{
    public class AutoitParameterInfo : SyntaxNodeBase
    {
        public IExpressionNode DefaultValue { get; private set; }
        public bool IsByRef { get; private set; }
        public bool IsConst { get; private set; }
        public string ParameterName { get; private set; }

        public AutoitParameterInfo( string parameterName, IExpressionNode defaultValue, bool isByRef, bool isConst ) {
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
            toReturn += ParameterName;
            toReturn += " ";
            if ( DefaultValue != null ) {
                toReturn += DefaultValue.ToSource();
            }
            return toReturn;
        }

        public override object Clone() {
            throw new System.NotImplementedException();
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            throw new System.NotImplementedException();
        }
    }
}
