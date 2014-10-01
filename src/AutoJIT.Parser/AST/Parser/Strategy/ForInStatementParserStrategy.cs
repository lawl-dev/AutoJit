using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class ForInStatementParserStrategy : StatementParserStrategyBase<ForInStatement>
    {
        public ForInStatementParserStrategy(
            IStatementParser statementParser,
            IExpressionParser expressionParser,
            IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseForIn( block ).ToEnumerable();
        }

        private ForInStatement ParseForIn( TokenQueue block ) {
            var line = ParseUntilNewLine( block );
            var split = line.Split( x => x.Value.Keyword == Keywords.In );
            if ( split.Count != 2 ) {
                throw new SyntaxTreeException( "Unexpected IN count in forin statement", line.First().Col, line.First().Line );
            }
            var localVariableName = split[0];
            var toEnumerateToken = split[1];
            var toEnumerate = ExpressionParser.ParseBlock( new TokenCollection( toEnumerateToken ), true );
            if ( localVariableName.Count != 1 ) {
                throw new SyntaxTreeException(
                    string.Format( "Unexpected localvariable token{0}", string.Join( "", localVariableName.Select( x => x.ToString() ).ToArray() ) ),
                    split[0][0].Col, split[0][0].Line );
            }
            var statementTokenCollection = ParseInner( block, Keywords.For, Keywords.Next, true );
            var statements = StatementParser.ParseBlock( statementTokenCollection );
            var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( new TokenQueue( localVariableName ) );

            return AutoitStatementFactory.CreateForInStatement( variableExpression, toEnumerate, statements );
        }
    }
}
