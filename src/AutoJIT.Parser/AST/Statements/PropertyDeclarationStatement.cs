using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class PropertyDeclarationStatement : StatementBase
    {
        public VariableExpression VariableExpression { get; private set; }
        public PropertyGetter PropertyGetter { get; private set; }
        public PropertySetter PropertySetter { get; private set; }

        public PropertyDeclarationStatement( VariableExpression variableExpression, PropertyGetter propertyGetter, PropertySetter propertySetter ) {
            VariableExpression = variableExpression;
            PropertyGetter = propertyGetter;
            PropertySetter = propertySetter;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();

                nodes.Add( VariableExpression );

                if ( PropertyGetter != null ) {
                    nodes.Add( PropertyGetter );
                }

                if ( PropertySetter != null ) {
                    nodes.Add( PropertySetter );
                }


                return nodes;
            }
        }

        public bool IsAutoProperty {
            get { return PropertyGetter == null && PropertySetter == null; }
        }

        public override string ToSource() {
            var toReturn = string.Format( "{0} {1}", Keywords.Property, VariableExpression.ToSource() );
            if ( PropertyGetter != null ) {
                toReturn += string.Format( "{0}{1}", Environment.NewLine, PropertyGetter.ToSource() );
            }
            if ( PropertySetter != null ) {
                toReturn += string.Format( "{0}{1}", Environment.NewLine, PropertySetter.ToSource() );
            }

            if ( PropertyGetter != null ||
                 PropertySetter != null ) {
                toReturn += Keywords.EndProperty;
            }

            return toReturn;
        }

        public override object Clone() {
            var statement = new PropertyDeclarationStatement(
                (VariableExpression) VariableExpression.Clone(), PropertyGetter != null
                    ? (PropertyGetter) PropertyGetter.Clone()
                    : null,
                PropertySetter != null
                    ? (PropertySetter) PropertySetter.Clone()
                    : null );
            statement.Initialize();
            return statement;
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitProperty( this );
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitProperty( this );
        }

        public PropertyDeclarationStatement Update( VariableExpression variable, PropertyGetter getter, PropertySetter setter ) {
            if ( VariableExpression == variable &&
                 PropertyGetter == getter &&
                 PropertySetter == setter ) {
                return this;
            }

            var statement = new PropertyDeclarationStatement(
                (VariableExpression) variable.Clone(), (PropertyGetter) ( getter != null
                    ? getter.Clone()
                    : null ),
                setter != null
                    ? (PropertySetter) setter.Clone()
                    : null );
            statement.Initialize();
            return statement;
        }
    }
}