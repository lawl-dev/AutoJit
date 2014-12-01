using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.ExtendIt
{
    public class PropertyRewriter : SyntaxRewriterBase
    { 
        private static readonly IAutoitSyntaxFactory SyntaxFactory = new AutoitSyntaxFactory( new TokenFactory() );
        private static readonly ITokenFactory TokenFactory = new TokenFactory();

        private readonly List<Function> _propertyFunctions = new List<Function>();
        private readonly Dictionary<string, PropertyInfo> _propertyInfos = new Dictionary<string, PropertyInfo>();

        public AutoitScriptRoot ImplementProperties( AutoitScriptRoot root ) {
            var newTree = (AutoitScriptRoot) VisitAutoitScriptRoot( root );

            foreach (var propertyInfo in _propertyInfos) {
                newTree = (AutoitScriptRoot) new PropertyReferenceRewriter( propertyInfo.Key, propertyInfo.Value.GetterName, propertyInfo.Value.SetterName ).Visit( newTree );
            }

            var functions = new List<Function>(_propertyFunctions);
            functions.AddRange( newTree.Functions.Select( x=>(Function)x.Clone() ) );
            return newTree.Update( (BlockStatement) newTree.MainFunction.Clone(), functions, new PragmaOptions() );
        }

        public override ISyntaxNode VisitProperty( PropertyDeclarationStatement node ) {
            var property = (PropertyDeclarationStatement) base.VisitProperty( node );

            if ( property.IsAutoProperty ) {
                return SyntaxFactory.CreateGlobalDeclarationStatement( (VariableExpression) node.VariableExpression.Clone(), null, false );
            }
            
            var valueName = string.Format( "value_{0}", Guid.NewGuid().ToString("N") );

            
            var propertyName = property.VariableExpression.IdentifierName.Token.Value.StringValue;
            var setterName = string.Format("set_{0}", propertyName);
            var getterName = string.Format("get_{0}", propertyName);

            if ( property.PropertySetter != null ) {
                var valueParameter = SyntaxFactory.CreateParameter( TokenFactory.CreateVariable( valueName, 0, 0 ), null, false, false );
                
                var setterToken = TokenFactory.CreateUserfunction(setterName, 0, 0);

                var transformedSetterBlock = ValueKeywordExpressionRewriter.TransformBlock(property.PropertySetter.StatementBlock, valueName);


                var setFunction = SyntaxFactory.CreateFunction( SyntaxFactory.CreateTokenNode( setterToken ), new List<AutoitParameter>() { valueParameter }, (BlockStatement) transformedSetterBlock.Clone() );
                _propertyFunctions.Add( setFunction );
            }

            if ( property.PropertyGetter != null ) {
                var getterToken = TokenFactory.CreateUserfunction(getterName, 0, 0);

                var getFunction = SyntaxFactory.CreateFunction(SyntaxFactory.CreateTokenNode(getterToken), new List<AutoitParameter>(), (BlockStatement) property.PropertyGetter.StatementBlock.Clone());
                _propertyFunctions.Add( getFunction );
            }

            _propertyInfos.Add(
                propertyName, new PropertyInfo() {
                    GetterName = property.PropertyGetter != null
                        ? getterName
                        : null,
                    SetterName = property.PropertySetter != null
                        ? setterName
                        : null
                } );
            return SyntaxFactory.CreateEmptyStatement();
        }

        class PropertyReferenceRewriter : SyntaxRewriterBase
        {
            private readonly string _variableName;
            private readonly string _getterName;
            private readonly string _setterName;

            public PropertyReferenceRewriter(string variableName, string getterName, string setterName) {
                _variableName = variableName;
                _getterName = getterName;
                _setterName = setterName;
            }

            public override ISyntaxNode VisitVariableExpression( VariableExpression node ) {
                if ( (node.Parent is AssignStatement || node.Parent is LocalDeclarationStatement || node.Parent is GlobalDeclarationStatement || node.Parent is EnumDeclarationStatement) ) {
                    return base.VisitVariableExpression( node );
                }

                var variableExpression = (VariableExpression)base.VisitVariableExpression( node );
                if ( variableExpression.IdentifierName.Token.Value.StringValue != _variableName ) {
                    return variableExpression;
                }

                var isPropertyReference = IsPropertyReferenceWalker.IsPropertyReference(node);

                if (_getterName == null)
                {
                    throw new InvalidOperationException();
                }

                if ( isPropertyReference ) {
                    return SyntaxFactory.CreateUserfunctionCallExpression( SyntaxFactory.CreateTokenNode( _getterName ), new List<IExpressionNode>() );
                }

                return variableExpression;
            }

            public override ISyntaxNode VisitAssignStatement( AssignStatement node ) {
                var assignStatement = (AssignStatement)base.VisitAssignStatement( node );

                if (node.Variable.IdentifierName.Token.Value.StringValue != _variableName)
                {
                    return assignStatement;
                }

                var isPropertyReference = IsPropertyReferenceWalker.IsPropertyReference(node.Variable);

                if ( _setterName == null ) {
                    throw new InvalidOperationException();
                }

                if (isPropertyReference)
                {
                    return SyntaxFactory.CreateFunctionCallStatement(SyntaxFactory.CreateUserfunctionCallExpression(SyntaxFactory.CreateTokenNode(_setterName), new List<IExpressionNode>(){(IExpressionNode) assignStatement.ExpressionToAssign.Clone()}));
                }

                return assignStatement;
            }
        }

        class ValueKeywordExpressionRewriter : SyntaxRewriterBase
        {
            private readonly string _variableName;
            
            private ValueKeywordExpressionRewriter( string variableName ) {
                _variableName = variableName;
            }

            public static BlockStatement TransformBlock( BlockStatement block, string variableName ) {
                return (BlockStatement) new ValueKeywordExpressionRewriter(variableName).VisitBlockStatement( block );
            }

            public override ISyntaxNode VisitValueExpression( ValueExpression node ) {
                return SyntaxFactory.CreateVariableExpression( SyntaxFactory.CreateTokenNode( TokenFactory.CreateVariable( _variableName, 0, 0 ) ) );
            }
        }

        class IsPropertyReferenceWalker : SyntaxWalkerBase
        {
            private readonly VariableExpression _variable;
            private bool _isPropertyReference = true;

            public static bool IsPropertyReference( VariableExpression variable ) {
                var isInFunction = variable.Ancestors().Any(x => x is Function);
                if (isInFunction) {
                    var self = new IsPropertyReferenceWalker( variable );
                    self.Visit(variable.Ancestors().Single(x => x is Function));
                    return self._isPropertyReference;
                }
                return false;
            }

            private IsPropertyReferenceWalker(VariableExpression variable) {
                _variable = variable;
            }


            public override void Visit( ISyntaxNode node ) {
                if (_variable == node)
                {
                    return;
                }
                base.Visit( node );
            }

            public override void VisitLocalDeclarationStatement( LocalDeclarationStatement node ) {
                base.VisitLocalDeclarationStatement( node );
                if ( node.VariableExpression.IdentifierName.Token.Value.StringValue == _variable.IdentifierName.Token.Value.StringValue ) {
                    _isPropertyReference = false;
                }
            }

            public override void VisitLocalEnumDeclarationStatement( LocalEnumDeclarationStatement node ) {
                base.VisitLocalEnumDeclarationStatement( node );
                if (node.VariableExpression.IdentifierName.Token.Value.StringValue == _variable.IdentifierName.Token.Value.StringValue)
                {
                    _isPropertyReference = false;
                }
            }
        }

        class PropertyInfo
        {
            public string SetterName { get; set; }
            public string GetterName { get; set; }
        }
    }
}