using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Parser.Strategy.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
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
            if ( block == null ) {
                return new List<IStatementNode>();
            }

            var statements = new List<IStatementNode>();

            while ( block.Any() ) {
                Token current = block.Peek();
                switch (current.Type) {
                    case TokenType.Variable:
                        statements.AddRange( ResolveStrategy<AssignStatement>().Parse( block ) );
                        break;
                    case TokenType.Userfunction:
                    case TokenType.Function:
                        statements.AddRange( ResolveStrategy<FunctionCallStatement>().Parse( block ) );
                        break;
                    case TokenType.Keyword:
                        SkipAndAssert( block, TokenType.Keyword );
                        switch (current.Value.Keyword) {
                            case Keywords.If:
                                statements.AddRange( ResolveStrategy<IfElseStatement>().Parse( block ) );
                                break;
                            case Keywords.Global:
                                statements.AddRange( ResolveStrategy<GlobalDeclarationStatement>().Parse( block ) );
                                break;
                            case Keywords.Enum:
                            case Keywords.Local:
                                statements.AddRange( ResolveStrategy<LocalDeclarationStatement>().Parse( block ) );
                                break;
                            case Keywords.Dim:
                                statements.AddRange( ResolveStrategy<DimStatement>().Parse( block ) );
                                break;
                            case Keywords.Redim:
                                statements.AddRange( ResolveStrategy<ReDimStatement>().Parse( block ) );
                                break;
                            case Keywords.Return:
                                statements.AddRange( ResolveStrategy<ReturnStatement>().Parse( block ) );
                                break;
                            case Keywords.While:
                                statements.AddRange( ResolveStrategy<WhileStatement>().Parse( block ) );
                                break;
                            case Keywords.Do:
                                statements.AddRange( ResolveStrategy<DoUntilStatement>().Parse( block ) );
                                break;
                            case Keywords.For:
                                statements.AddRange( ParseFor( block ) );
                                break;
                            case Keywords.Switch:
                                statements.AddRange( ResolveStrategy<SwitchCaseStatement>().Parse( block ) );
                                break;
                            case Keywords.Select:
                                statements.AddRange( ResolveStrategy<SelectCaseStatement>().Parse( block ) );
                                break;
                            case Keywords.Exit:
                                statements.AddRange( ResolveStrategy<ExitStatement>().Parse( block ) );
                                break;
                            case Keywords.Exitloop:
                                statements.AddRange( ResolveStrategy<ExitloopStatement>().Parse( block ) );
                                break;
                            case Keywords.Continueloop:
                                statements.AddRange( ResolveStrategy<ContinueloopStatement>().Parse( block ) );
                                break;
                            case Keywords.ContinueCase:
                                statements.AddRange( ResolveStrategy<ContinueCaseStatement>().Parse( block ) );
                                break;
                            default:
                                throw new NotImplementedException( string.Format( "Keyword Strategy: {0}", current.Value.Keyword ) );
                        }
                        break;
                    case TokenType.NewLine:
                        SkipAndAssert( block, TokenType.NewLine );
                        break;
                    default:
                        throw new NotImplementedException( current.Type.ToString() );
                }
            }
            return statements;
        }

        private IStatementParserStrategy<T> ResolveStrategy<T>() where T : IStatementNode {
            return _parserResolver.Resolve<T>();
        }

        private IEnumerable<IStatementNode> ParseFor( TokenQueue block ) {
            bool isForInLoop = block.TakeWhile( x => x.Type != TokenType.NewLine ).Any( x => x.Value.Keyword == Keywords.In );

            if ( isForInLoop ) {
                return ResolveStrategy<ForInStatement>().Parse( block );
            }
            return ResolveStrategy<ForToNextStatement>().Parse( block );
        }

        private void SkipAndAssert( TokenQueue block, TokenType tokenType ) {
            if ( block.Peek().Type != tokenType ) {
                throw new SyntaxTreeException( string.Format( "Expected {0} but was {1}", tokenType, block.Peek().Type ), block.Peek().Col, block.Peek().Line );
            }
            block.Dequeue();
        }
    }
}
