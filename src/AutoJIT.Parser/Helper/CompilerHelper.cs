using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.Parser.Helper
{
    public static class CompilerHelper
    {
        public static string GetCompilerMemberName( Expression<Action<AutoitRuntime<object>>> expression ) {
            if( expression == null ) {
                throw new ArgumentException( "The expression cannot be null." );
            }

            return GetMemberName( expression.Body );
        }

        public static string GetVariantMemberName( Expression<Action<Variant>> expression ) {
            if( expression == null ) {
                throw new ArgumentException( "The expression cannot be null." );
            }

            return GetMemberName( expression.Body );
        }

        public static IEnumerable<string> GetMacros( Func<MemberInfo, bool> expr ) {
            return typeof(AutoitContext<>).GetMembers().Where( expr ).Select( x => x.Name );
        }

        private static string GetMemberName( Expression expression ) {
            if( expression == null ) {
                throw new ArgumentException( "The expression cannot be null." );
            }

            if( expression is MemberExpression ) {
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if( expression is MethodCallExpression ) {
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if( expression is UnaryExpression ) {
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName( unaryExpression );
            }

            throw new ArgumentException( "Invalid expression" );
        }

        private static string GetMemberName( UnaryExpression unaryExpression ) {
            if( unaryExpression.Operand is MethodCallExpression ) {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ( (MemberExpression)unaryExpression.Operand ).Member.Name;
        }

        public static IEnumerable<CSharpParameterInfo> GetParameterInfo( string functionName, params ExpressionSyntax[] parameter ) {
            MethodInfo memberInfo = typeof(AutoitRuntime<object>).GetMethods().Single( x => x.Name == functionName && x.GetParameters().Length == parameter.Length );
            return memberInfo.GetParameters().Select( ( info, i ) => Map( info, parameter[i] ) );
        }

        private static CSharpParameterInfo Map( ParameterInfo parameterInfo, ExpressionSyntax expressionNode ) {
            return new CSharpParameterInfo( expressionNode, parameterInfo.ParameterType.IsByRef );
        }
    }
}
