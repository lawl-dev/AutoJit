using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoJIT.Parser.Extensions
{
    internal static class StackExtensions
    {
        public static IEnumerable<T> PopWhile<T>( this Stack<T> stack, Func<T, bool> expression ) {
            var list = new List<T>();

            var peek = stack.Peek();
            while ( expression( peek ) ) {
                list.Add( stack.Pop() );
                peek = stack.Peek();
            }

            return list;
        }

        public static IEnumerable<T> PopWhile<T>( this Stack<T> stack, Func<T, int, bool> expression ) {
            var list = new List<T>();
            int i = 0;
            var peek = stack.Peek();
            while ( expression( peek, i++ ) &&
                    stack.Any() ) {
                list.Add( stack.Pop() );
                if ( stack.Any() ) {
                    peek = stack.Peek();
                }
            }
            return list;
        }
    }
}
