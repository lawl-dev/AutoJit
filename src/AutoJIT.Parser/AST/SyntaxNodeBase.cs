using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST
{
    public abstract class SyntaxNodeBase : ISyntaxNode
    {
        public void AcceptSingle( ISyntaxVisitor visitor ) {
            visitor.Visit( this );
        }

        public abstract IEnumerable<ISyntaxNode> Children { get; }

        public void Accept( ISyntaxVisitor visitor ) {
            foreach (var child in Children) {
                child.Accept( visitor );
                visitor.Visit( child );
            }
        }

        public void Accept( ISyntaxVisitor<ISyntaxNode, ISyntaxNode> visitor ) {
            throw new NotImplementedException();
        }

        protected void Initialize() {
            foreach (var child in Children.Where( x => x != null )) {
                child.Parent = this;
            }
        }

        private ISyntaxNode _parent;

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

        public abstract string ToSource();
        public abstract object Clone();
    }
}
