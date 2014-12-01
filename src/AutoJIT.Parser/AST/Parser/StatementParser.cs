using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Parser.Strategy.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser
{
    public sealed class StatementParser : IStatementParser
    {
        private readonly IStatementParserStrategyResolver _parserResolver;

        public StatementParser( IStatementParserStrategyResolver parserResolver ) {
            _parserResolver = parserResolver;
        }

        public List<IStatementNode> ParseBlock( TokenQueue block ) {
            return ParseStatementNodes( block ).ToList();
        }

        private IEnumerable<IStatementNode> ParseStatementNodes( TokenQueue block ) {
            var statements = new List<IStatementNode>();

            while ( block.Any() ) {
                Token current = block.Peek();

                IStatementParserStrategy parser = GetParser( block, current );

                IEnumerable<IStatementNode> nodes = parser.Parse( block );

                statements.AddRange( nodes );
            }
            return statements;
        }

        private IStatementParserStrategy GetParser( TokenQueue block, Token current ) {
            switch (current.Type) {
                case TokenType.NewLine:
                    return ResolveStrategy<EmptyStatement>();
                case TokenType.Variable:
                    bool isVariableFunctionCall = block.Skip( 1 ).First().Type == TokenType.Leftparen;
                    if ( isVariableFunctionCall ) {
                        return ResolveStrategy<VariableFunctionCallStatement>();
                    }
                    return ResolveStrategy<AssignStatement>();
                case TokenType.Userfunction:
                case TokenType.Function:
                    return ResolveStrategy<FunctionCallStatement>();
                case TokenType.Keyword:
                    ConsumeAndEnsure( block, TokenType.Keyword );
                    switch (current.Value.Keyword) {
                        case Keywords.Static:
                            if ( Consume( block, Keywords.Global ) ) {
                                goto case Keywords.Global;
                            }
                            Consume( block, Keywords.Local );
                            return ResolveStrategy<StaticDeclarationStatement>();
                        case Keywords.Property:
                            return ResolveStrategy<PropertyDeclarationStatement>();
                        case Keywords.Global:
                            if ( Consume( block, Keywords.Enum ) ) {
                                return ResolveStrategy<GlobalEnumDeclarationStatement>();
                            }
                            Consume( block, Keywords.Static ); //irrelevant
                            return ResolveStrategy<GlobalDeclarationStatement>();
                        case Keywords.If:
                            return ResolveStrategy<IfElseStatement>();
                        case Keywords.Enum:
                            return ResolveStrategy<LocalEnumDeclarationStatement>();
                        case Keywords.Local:
                            if ( Consume( block, Keywords.Enum ) ) {
                                return ResolveStrategy<LocalEnumDeclarationStatement>();
                            }

                            return Consume( block, Keywords.Static )
                                ? ResolveStrategy<StaticDeclarationStatement>()
                                : ResolveStrategy<LocalDeclarationStatement>();
                        case Keywords.Dim:
                            return ResolveStrategy<DimStatement>();
                        case Keywords.Redim:
                            return ResolveStrategy<ReDimStatement>();
                        case Keywords.Return:
                            return ResolveStrategy<ReturnStatement>();
                        case Keywords.While:
                            return ResolveStrategy<WhileStatement>();
                        case Keywords.Do:
                            return ResolveStrategy<DoUntilStatement>();
                        case Keywords.For:
                            return ParseFor( block );
                        case Keywords.Switch:
                            return ResolveStrategy<SwitchCaseStatement>();
                        case Keywords.Select:
                            return ResolveStrategy<SelectCaseStatement>();
                        case Keywords.Exit:
                            return ResolveStrategy<ExitStatement>();
                        case Keywords.Exitloop:
                            return ResolveStrategy<ExitloopStatement>();
                        case Keywords.Continueloop:
                            return ResolveStrategy<ContinueLoopStatement>();
                        case Keywords.ContinueCase:
                            return ResolveStrategy<ContinueCaseStatement>();
                        default:
                            throw new NotImplementedException( string.Format( "Keyword Strategy: {0}", current.Value.Keyword ) );
                    }
                    break;
            }
            throw new NotImplementedException( current.Type.ToString() );
        }

        private IStatementParserStrategy<T> ResolveStrategy<T>() where T : IStatementNode {
            return _parserResolver.Resolve<T>();
        }

        private IStatementParserStrategy ParseFor( IEnumerable<Token> block ) {
            bool isForInLoop = block.TakeWhile( x => x.Type != TokenType.NewLine ).Any( x => x.Value.Keyword == Keywords.In );

            if ( isForInLoop ) {
                return ResolveStrategy<ForInStatement>();
            }
            return ResolveStrategy<ForToNextStatement>();
        }

        private void ConsumeAndEnsure( TokenQueue block, TokenType tokenType ) {
            if ( block.Peek().Type != tokenType ) {
                throw new SyntaxTreeException( string.Format( "Expected {0} but was {1}", tokenType, block.Peek().Type ), block.Peek().Col, block.Peek().Line );
            }
            block.Dequeue();
        }

        private bool Consume( TokenQueue block, Keywords keyword ) {
            if ( block.Peek().Value.Keyword != keyword ) {
                return false;
            }
            block.Dequeue();
            return true;
        }

        private void ConsumeNewLine( TokenQueue block ) {
            block.DequeueWhile( x => x.Type == TokenType.NewLine ).ToList();
        }
    }
}
