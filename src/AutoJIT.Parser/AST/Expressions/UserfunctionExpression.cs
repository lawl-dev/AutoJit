namespace AutoJIT.Parser.AST.Expressions
{
    public class UserfunctionExpression : FunctionExpression
    {
        public UserfunctionExpression( TokenNode identifierName ) : base( identifierName ) {}

        public override object Clone() {
            return new UserfunctionExpression( (TokenNode) IdentifierName.Clone() );
        }

        public override FunctionExpression Update( TokenNode identifierName ) {
            if (IdentifierName == identifierName)
            {
                return this;
            }
            var expression = new UserfunctionExpression((TokenNode)identifierName.Clone());
            expression.Initialize();
            return expression;
        }
    }
}
