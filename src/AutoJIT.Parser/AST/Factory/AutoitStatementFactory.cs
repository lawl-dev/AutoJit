using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Factory
{
    public sealed class AutoitStatementFactory : IAutoitStatementFactory
    {
        public AssignStatement CreateAssignStatement( VariableExpression variableExpression, IExpressionNode expression, Token @operator ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            if ( expression == null ) {
                throw new ArgumentNullException( "expression" );
            }

            if ( @operator == null ) {
                throw new ArgumentNullException( "operator" );
            }

            if ( @operator.Type != TokenType.PowAssign
                 &&
                 @operator.Type != TokenType.ConcatAssign
                 &&
                 @operator.Type != TokenType.DivAssign
                 &&
                 @operator.Type != TokenType.MinusAssign
                 &&
                 @operator.Type != TokenType.MultAssign
                 &&
                 @operator.Type != TokenType.PlusAssign
                 &&
                 @operator.Type != TokenType.Equal ) {
                throw new ArgumentException( "operator" );
            }

            return new AssignStatement( variableExpression, expression, new TokenNode( @operator ) );
        }

        public ContinueCaseStatement CreateContinueCaseStatement() {
            return new ContinueCaseStatement();
        }

        public ContinueLoopStatement CreateContinueloopStatement( TokenNode level ) {
            return new ContinueLoopStatement( level );
        }

        public DimStatement CreateDimStatement( VariableExpression variableExpression, IExpressionNode initExpression ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            return new DimStatement( variableExpression, initExpression );
        }

        public DoUntilStatement CreateDoUntilStatement( IExpressionNode condition, IEnumerable<IStatementNode> block ) {
            if ( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            if ( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            return new DoUntilStatement( condition, new BlockStatement( block ) );
        }

        public ExitloopStatement CreateExitloopStatement( TokenNode level ) {
            return new ExitloopStatement( level );
        }

        public ExitStatement CreateExitStatement( IExpressionNode exitCode ) {
            return new ExitStatement( exitCode );
        }

        public ForInStatement CreateForInStatement( VariableExpression variableName, IExpressionNode toEnumerate, IEnumerable<IStatementNode> block ) {
            if ( variableName == null ) {
                throw new ArgumentNullException( "variableName" );
            }

            if ( toEnumerate == null ) {
                throw new ArgumentNullException( "toEnumerate" );
            }

            if ( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            return new ForInStatement( variableName, toEnumerate, new BlockStatement( block ) );
        }

        public ForToNextStatement CreateForToNextStatement( VariableExpression variableExpression, IExpressionNode startExpression, IExpressionNode endExpression, IExpressionNode stepExpression, IEnumerable<IStatementNode> block ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            if ( startExpression == null ) {
                throw new ArgumentNullException( "startExpression" );
            }

            if ( endExpression == null ) {
                throw new ArgumentNullException( "endExpression" );
            }

            if ( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            return new ForToNextStatement( variableExpression, startExpression, endExpression, stepExpression, new BlockStatement( block ) );
        }

        public FunctionCallStatement CreateFunctionCallStatement( CallExpression functionCallExpression ) {
            if ( functionCallExpression == null ) {
                throw new ArgumentNullException( "functionCallExpression" );
            }

            return new FunctionCallStatement( functionCallExpression );
        }

        public GlobalDeclarationStatement CreateGlobalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            return new GlobalDeclarationStatement( variableExpression, initExpression, isConst );
        }

        public EnumDeclarationStatement CreateEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, IExpressionNode autoInitExpression, bool global ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }
            if ( global ) {
                return new GlobalEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression );
            }
            return new LocalEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression );
        }

        public IfElseStatement CreateIfElseStatement( IExpressionNode condition, IEnumerable<IStatementNode> ifBlock, IEnumerable<IExpressionNode> elseIfConditions, IEnumerable<IEnumerable<IStatementNode>> elseIfBlocks, IEnumerable<IStatementNode> elseBlock ) {
            if ( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            if ( ifBlock == null ) {
                throw new ArgumentNullException( "ifBlock" );
            }

            return new IfElseStatement( condition, new BlockStatement( ifBlock ), elseIfConditions, elseIfBlocks.Select( x => new BlockStatement( x ) ), new BlockStatement( elseBlock ) );
        }

        public LocalDeclarationStatement CreateLocalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            return new LocalDeclarationStatement( variableExpression, initExpression, isConst );
        }

        public ReDimStatement CreateReDimStatement( ArrayExpression arrayExpression ) {
            if ( arrayExpression == null ) {
                throw new ArgumentNullException( "arrayExpression" );
            }

            return new ReDimStatement( arrayExpression );
        }

        public ReturnStatement CreateReturnStatement( IExpressionNode returnExpression ) {
            if ( returnExpression == null ) {
                throw new ArgumentNullException( "returnExpression" );
            }

            return new ReturnStatement( returnExpression );
        }

        public SelectCaseStatement CreateSelectStatement( IEnumerable<SelectCase> cases, IEnumerable<IStatementNode> elseStatements ) {
            if ( cases == null ) {
                throw new ArgumentNullException( "cases" );
            }

            return new SelectCaseStatement(
                cases, elseStatements != null && elseStatements.Any()
                    ? new BlockStatement( elseStatements )
                    : null );
        }

        public WhileStatement CreateWhileStatement( IExpressionNode condition, List<IStatementNode> block ) {
            if ( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            return new WhileStatement( condition, new BlockStatement( block ) );
        }

        public IStatementNode CreateStaticDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }
            return new StaticDeclarationStatement( variableExpression, initExpression );
        }
    }
}
