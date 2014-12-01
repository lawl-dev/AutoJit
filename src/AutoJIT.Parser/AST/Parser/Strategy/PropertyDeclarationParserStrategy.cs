using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class PropertyDeclarationParserStrategy : StatementParserStrategyBase<PropertyDeclarationStatement>
    {
        public PropertyDeclarationParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            VariableExpression variableExpression;
            PropertyGetter propertyGetter = null;
            PropertySetter propertySetter = null;
            
            var isAutoProperty = IsAutoProperty( block );

            if ( isAutoProperty ) {
                var propertyToken = new TokenQueue( block.DequeueUntil( x=>x.Type != TokenType.NewLine ) );
                ConsumeAndEnsure( block, TokenType.NewLine );
                variableExpression = ExpressionParser.ParseSingle<VariableExpression>(propertyToken);
            }
            else {
                var propertyToken = new TokenQueue(block.DequeueWhile(x => x.Value.Keyword != Keywords.EndProperty));

                variableExpression = ExpressionParser.ParseSingle<VariableExpression>(propertyToken);
                ConsumeAndEnsure(propertyToken, TokenType.NewLine);

                if (Consume(propertyToken, Keywords.Get)) {
                    var getStatementToken = new TokenQueue( ParseInner( propertyToken, Keywords.Get, Keywords.EndGet, true ) );
                    ConsumeAndEnsure( getStatementToken, TokenType.NewLine );
                    var getStatementNodes = StatementParser.ParseBlock(getStatementToken);
                    propertyGetter = AutoitSyntaxFactory.CreatePropertyGetter(AutoitSyntaxFactory.CreateBlockStatement(getStatementNodes));
                    ConsumeAndEnsure( propertyToken, TokenType.NewLine );
                }

                if (Consume(propertyToken, Keywords.Set)) {
                    var setStatementToken = new TokenQueue( ParseInner( propertyToken, Keywords.Set, Keywords.EndSet, true ) );
                    ConsumeAndEnsure( setStatementToken, TokenType.NewLine );
                    var setStatementNodes = StatementParser.ParseBlock(setStatementToken);
                    propertySetter = AutoitSyntaxFactory.CreatePropertySetter(AutoitSyntaxFactory.CreateBlockStatement(setStatementNodes));
                    ConsumeAndEnsure(propertyToken, TokenType.NewLine);
                }

                ConsumeAndEnsure(block, Keywords.EndProperty);
                ConsumeAndEnsure(block, TokenType.NewLine);
                Ensure(() => !propertyToken.Any());
            }

            return AutoitSyntaxFactory.CreateProperty(variableExpression, propertyGetter, propertySetter).ToEnumerable();
        }

        private static bool IsAutoProperty( IEnumerable<Token> block ) {
            var token = block.FirstOrDefault(x=>x.Value.Keyword == Keywords.Property || x.Value.Keyword == Keywords.EndProperty);
            return token == null || token.Value.Keyword == Keywords.Property;
        }
    }
}