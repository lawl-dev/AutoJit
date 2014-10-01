using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AutoJIT.Parser.Extensions
{
    public static class EnumerableExtensions
    {
        public static Queue<T> ToQueue<T>( this IEnumerable<T> source ) {
            return new Queue<T>( source );
        }

        public static List<List<T>> Split<T>( this IEnumerable<T> source, Func<T, bool> expression ) {
            var res = new List<List<T>>();
            res.Add( new List<T>() );
            foreach (var item in source) {
                if ( expression( item ) ) {
                    res.Add( new List<T>() );
                }
                else {
                    res.Last().Add( item );
                }
            }
            return res;
        }

        public static SeparatedSyntaxList<TSyntax> ToSeparatedSyntaxList<TSyntax>( this IEnumerable<TSyntax> source ) where TSyntax : SyntaxNode {
            return new SeparatedSyntaxList<TSyntax>().AddRange( source );
        }

        public static SyntaxList<TSyntax> ToSyntaxList<TSyntax>( this IEnumerable<TSyntax> source ) where TSyntax : SyntaxNode {
            return new SyntaxList<TSyntax>().AddRange( source );
        }

        public static SeparatedSyntaxList<TSyntax> ToSeparatedSyntaxList<TSyntax>( this TSyntax source ) where TSyntax : SyntaxNode {
            return new SeparatedSyntaxList<TSyntax>().Add( source );
        }
    }
}
