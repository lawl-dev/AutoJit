using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser
{
    public struct AutoitParameterInfo
    {
        public readonly IExpressionNode DefaultValue;
        public readonly bool IsByRef;
        public readonly bool IsConst;
        public readonly string ParameterName;

        public AutoitParameterInfo( string parameterName, IExpressionNode defaultValue, bool isByRef, bool isConst ) {
            IsConst = isConst;
            ParameterName = parameterName;
            DefaultValue = defaultValue;
            IsByRef = isByRef;
        }
    }
}
