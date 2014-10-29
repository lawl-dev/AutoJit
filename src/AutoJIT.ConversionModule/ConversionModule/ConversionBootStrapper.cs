using AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter;
using AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter.Interface;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.StatementConverter;
using AutoJIT.CSharpConverter.ConversionModule.StatementConverter.Interface;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    public class ConversionBootStrapper : ComponentContainerBase
    {
        protected override void Bind() {
            Bind<IAutoitToCSharpConverter, AutoitToCSharpConverter>();
            Bind<ICSharpStatementFactory, CSharpStatementFactory>();
            Bind<ICSharpSkeletonFactory, CSharpSkeletonFactory>();
            Bind<IContextService, ContextService>();

            RegisterStatementConverter();
            RegisterExpressionConverter();
        }

        private void RegisterStatementConverter() {
            Bind<IAutoitStatementConverter<AssignStatement, StatementSyntax>, AutoitAssignStatementConverter>();
            Bind<IAutoitStatementConverter<DoUntilStatement, StatementSyntax>, AutoitDoUntilStatementConverter>();
            Bind<IAutoitStatementConverter<ForToNextStatement, StatementSyntax>, AutoitForToNextStatementConverter>();
            Bind<IAutoitStatementConverter<FunctionCallStatement, StatementSyntax>, AutoitFunctionCallStatementConverter>();
            Bind<IAutoitStatementConverter<IfElseStatement, StatementSyntax>, AutoitIfElseStatementConverter>();
            Bind<IAutoitStatementConverter<ReturnStatement, StatementSyntax>, AutoitReturnStatementConverter>();
            Bind<IAutoitStatementConverter<WhileStatement, StatementSyntax>, AutoitWhileStatementConverter>();
            Bind<IAutoitStatementConverter<SwitchCaseStatement, StatementSyntax>, AutoitSwitchCaseStatementConverter>();
            Bind<IAutoitStatementConverter<ExitStatement, StatementSyntax>, AutoitExitStatementConverter>();
            Bind<IAutoitStatementConverter<SelectCaseStatement, StatementSyntax>, AutoitSelectCaseStatementConverter>();
            Bind<IAutoitStatementConverter<ForInStatement, StatementSyntax>, AutoitForInStatementConverter>();
            Bind<IAutoitStatementConverter<LocalDeclarationStatement, StatementSyntax>, AutoitLocalDeclarationStatementConverter>();
            Bind<IAutoitStatementConverter<DimStatement, StatementSyntax>, AutoitDimStatementConverter>();
            Bind<IAutoitStatementConverter<GlobalDeclarationStatement, StatementSyntax>, AutoitGlobalDeclarationStatementConverter>();
            Bind<IAutoitStatementConverter<ExitloopStatement, StatementSyntax>, AutoitExitloopStatementConverter>();
            Bind<IAutoitStatementConverter<ContinueloopStatement, StatementSyntax>, AutoitContinueloopStatementConverter>();
            Bind<IAutoitStatementConverter<ReDimStatement, StatementSyntax>, AutoitRedimStatementConverter>();
            Bind<IAutoitStatementConverter<InitDefaultParameterStatement, StatementSyntax>, AutoitInitDefaultParameterStatementConverter>();
            Bind<IAutoitStatementConverter<ContinueCaseStatement, StatementSyntax>, AutoitContinueCaseStatementConverter>();
            Bind<IAutoitStatementConverter<GlobalEnumDeclarationStatement, StatementSyntax>, AutoitEnumDeclarationStatementConverter>();
            Bind<IAutoitStatementConverter<LocalEnumDeclarationStatement, StatementSyntax>, AutoitEnumDeclarationStatementConverter>();
            Bind<IAutoitStatementConverter<StaticDeclarationStatement, StatementSyntax>, AutoitStaticDeclarationStatementConverter>();
        }

        private void RegisterExpressionConverter() {
            Bind<IAutoitExpressionConverter<VariableExpression, ExpressionSyntax>, AutoitVariableExpressionConverter>();
            Bind<IAutoitExpressionConverter<ArrayExpression, ExpressionSyntax>, AutoitArrayExpressionConverter>();
            Bind<IAutoitExpressionConverter<BinaryExpression, ExpressionSyntax>, AutoitBinaryExpressionConverter>();
            Bind<IAutoitExpressionConverter<CallExpression, ExpressionSyntax>, AutoitCallExpressionConverter>();
            Bind<IAutoitExpressionConverter<UserfunctionCallExpression, ExpressionSyntax>, AutoitUserfunctionCallExpressionConverter>();
            Bind<IAutoitExpressionConverter<NegateExpression, ExpressionSyntax>, AutoitNegateExpressionConverter>();
            Bind<IAutoitExpressionConverter<BooleanNegateExpression, ExpressionSyntax>, AutoitBooleanNegateExpressionConverter>();
            Bind<IAutoitExpressionConverter<MacroExpression, ExpressionSyntax>, AutoitMacroExpressionConverter>();
            Bind<IAutoitExpressionConverter<StringLiteralExpression, ExpressionSyntax>, AutoitStringLiteralExpressionConverter>();
            Bind<IAutoitExpressionConverter<NumericLiteralExpression, ExpressionSyntax>, AutoitNumericLiteralExpressionConverter>();
            Bind<IAutoitExpressionConverter<ArrayInitExpression, ExpressionSyntax>, AutoitArrayInitExpressionConverter>();
            Bind<IAutoitExpressionConverter<FalseLiteralExpression, ExpressionSyntax>, AutoitFalseLiteralExpressionConverter>();
            Bind<IAutoitExpressionConverter<TrueLiteralExpression, ExpressionSyntax>, AutoitTrueLiteralExpressionConverter>();
            Bind<IAutoitExpressionConverter<NullExpression, ExpressionSyntax>, AutoitNullExpressionConverter>();
            Bind<IAutoitExpressionConverter<TernaryExpression, ExpressionSyntax>, AutoitTernaryExpressionConverter>();
            Bind<IAutoitExpressionConverter<DefaultExpression, ExpressionSyntax>, AutoitDefaultExpressionConverter>();
        }
    }
}
