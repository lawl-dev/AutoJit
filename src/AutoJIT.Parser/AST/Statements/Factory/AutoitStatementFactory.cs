using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Statements.Factory
{
    public sealed class AutoitStatementFactory : IAutoitStatementFactory
    {
        public AssignStatement CreateAssignStatement( VariableExpression variableExpression, IExpressionNode expression, Token @operator ) {
            if( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            if( expression == null ) {
                throw new ArgumentNullException( "expression" );
            }

            if( @operator == null ) {
                throw new ArgumentNullException( "operator" );
            }

            if( @operator.Type != TokenType.PowAssign
                && @operator.Type != TokenType.ConcatAssign
                && @operator.Type != TokenType.DivAssign
                && @operator.Type != TokenType.MinusAssign
                && @operator.Type != TokenType.MultAssign
                && @operator.Type != TokenType.PlusAssign
                && @operator.Type != TokenType.Equal ) {
                throw new ArgumentException( "operator" );
            }

            return new AssignStatement( variableExpression, expression, @operator );
        }

        public ContinueCaseStatement CreateContinueCaseStatement() {
            return new ContinueCaseStatement();
        }

        public ContinueloopStatement CreateContinueloopStatement( int level ) {
            return new ContinueloopStatement( level );
        }

        public DimStatement CreateDimStatement( VariableExpression variableExpression, IExpressionNode initExpression ) {
            if( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            return new DimStatement( variableExpression, initExpression );
        }

        public DoUntilStatement CreateDoUntilStatement( IExpressionNode condition, List<IStatementNode> block ) {
            if( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            if( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            return new DoUntilStatement( condition, block );
        }

        public ExitloopStatement CreateExitloopStatement( int level ) {
            return new ExitloopStatement( level );
        }

        public ExitStatement CreateExitStatement( IExpressionNode exitCode ) {
            return new ExitStatement( exitCode );
        }

        public ForInStatement CreateForInStatement( VariableExpression variableName, IExpressionNode toEnumerate, List<IStatementNode> block ) {
            if( variableName == null ) {
                throw new ArgumentNullException( "variableName" );
            }

            if( toEnumerate == null ) {
                throw new ArgumentNullException( "toEnumerate" );
            }

            if( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            return new ForInStatement( variableName, toEnumerate, block );
        }

        public ForToNextStatement CreateForToNextStatement(
        VariableExpression variableExpression,
        IExpressionNode startExpression,
        IExpressionNode endExpression,
        IExpressionNode stepExpression,
        List<IStatementNode> block ) {
            if( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            if( startExpression == null ) {
                throw new ArgumentNullException( "startExpression" );
            }

            if( endExpression == null ) {
                throw new ArgumentNullException( "endExpression" );
            }

            if( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            return new ForToNextStatement( variableExpression, startExpression, endExpression, stepExpression, block );
        }

        public FunctionCallStatement CreateFunctionCallStatement( CallExpression functionCallExpression ) {
            if( functionCallExpression == null ) {
                throw new ArgumentNullException( "functionCallExpression" );
            }

            return new FunctionCallStatement( functionCallExpression );
        }

        public GlobalDeclarationStatement CreateGlobalDeclarationStatement(
        VariableExpression variableExpression,
        IExpressionNode initExpression,
        bool isConst ) {
            if( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            return new GlobalDeclarationStatement( variableExpression, initExpression, isConst );
        }

        public EnumDeclarationStatement CreateEnumDeclarationStatement(
        VariableExpression variableExpression,
        IExpressionNode initExpression,
        IExpressionNode autoInitExpression,
        bool global ) {
            if( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }
            if( global ) {
                return new GlobalEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression );
            }
            return new LocalEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression );
        }

        public IfElseStatement CreateIfElseStatement(
        IExpressionNode condition,
        List<IStatementNode> ifBlock,
        Queue<IExpressionNode> elseIfConditions,
        Queue<List<IStatementNode>> elseIfBlocks,
        IEnumerable<IStatementNode> elseBlock ) {
            if( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            if( ifBlock == null ) {
                throw new ArgumentNullException( "ifBlock" );
            }

            return new IfElseStatement( condition, ifBlock, elseIfConditions, elseIfBlocks, elseBlock );
        }

        public LocalDeclarationStatement CreateLocalDeclarationStatement(
        VariableExpression variableExpression,
        IExpressionNode initExpression,
        bool isConst,
        bool isStatic ) {
            if( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            return new LocalDeclarationStatement( variableExpression, initExpression, isConst, isStatic );
        }

        public ReDimStatement CreateReDimStatement( ArrayExpression arrayExpression ) {
            if( arrayExpression == null ) {
                throw new ArgumentNullException( "arrayExpression" );
            }

            return new ReDimStatement( arrayExpression );
        }

        public ReturnStatement CreateReturnStatement( IExpressionNode returnExpression ) {
            if( returnExpression == null ) {
                throw new ArgumentNullException( "returnExpression" );
            }

            return new ReturnStatement( returnExpression );
        }

        public SelectCaseStatement CreateSelectStatement(
        Dictionary<IExpressionNode, IEnumerable<IStatementNode>> cases,
        IEnumerable<IStatementNode> elseStatements ) {
            if( cases == null ) {
                throw new ArgumentNullException( "cases" );
            }

            return new SelectCaseStatement( cases, elseStatements );
        }

        public WhileStatement CreateWhileStatement( IExpressionNode condition, List<IStatementNode> block ) {
            if( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            return new WhileStatement( condition, block );
        }
    }
}
