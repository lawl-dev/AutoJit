namespace AutoJIT.Parser.AST.Expressions
{
    public class UserfunctionExpression : FunctionExpression
    {
        public UserfunctionExpression( string identifierName ) : base( identifierName ) {}

        public override object Clone() {
            return new UserfunctionExpression( IdentifierName );
        }
    }
}
