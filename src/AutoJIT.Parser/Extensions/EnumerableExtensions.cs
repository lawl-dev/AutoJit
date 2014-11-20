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
            foreach (T item in source) {
                if ( expression( item ) ) {
                    res.Add( new List<T>() );
                }
                else {
                    res.Last().Add( item );
                }
            }
            return res;
        }
    }
}
