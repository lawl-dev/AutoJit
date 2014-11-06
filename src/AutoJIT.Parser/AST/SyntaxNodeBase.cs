using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST
{
    public abstract class SyntaxNodeBase : ISyntaxNode
    {
        private ISyntaxNode _parent;

        public void AcceptSingle( ISyntaxVisitor visitor ) {
            visitor.Visit( this );
        }

        public abstract IEnumerable<ISyntaxNode> Children { get; }

        public void Accept( ISyntaxVisitor visitor ) {
            foreach (ISyntaxNode child in Children) {
                child.Accept( visitor );
                visitor.Visit( child );
            }
        }

        public abstract TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor );

        public ISyntaxNode Parent {
            get { return _parent; }
            set {
                if ( _parent == null ) {
                    _parent = value;
                    return;
                }
                throw new InvalidOperationException();
            }
        }

        public abstract string ToSource();
        public abstract object Clone();

        protected void Initialize() {
            foreach (ISyntaxNode child in Children.Where( x => x != null )) {
                child.Parent = this;
            }
        }

        protected T CloneAs<T>( ICloneable obj ) where T : ISyntaxNode {
            return (T) ( obj == null
                ? default( T )
                : obj.Clone() );
        }

        protected IEnumerable<T> CloneEnumerableAs<T>( IEnumerable<ICloneable> objects ) where T : ISyntaxNode {
            return objects == null
                ? default( T ).ToEnumerable()
                : objects.Select( x => (T) x.Clone() );
        }
    }
}
