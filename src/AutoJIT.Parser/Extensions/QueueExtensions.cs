using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoJIT.Parser.Extensions
{
    internal static class QueueExtensions
    {
        public static IEnumerable<T> DequeueWhile<T>( this Queue<T> queue, Func<T, bool> expression ) {
            var list = new List<T>();

            if( queue.Any() ) {
                T peek = queue.Peek();
                while( expression( peek )
                       && queue.Any() ) {
                    list.Add( queue.Dequeue() );
                    if( queue.Any() ) {
                        peek = queue.Peek();
                    }
                }
            }
            return list;
        }

        public static IEnumerable<T> DequeueUntil<T>( this Queue<T> queue, Func<T, bool> expression ) {
            var list = new List<T>();

            if( queue.Any() ) {
                T peek = queue.Peek();
                while( !expression( peek )
                       && queue.Any() ) {
                    list.Add( queue.Dequeue() );
                    if( queue.Any() ) {
                        peek = queue.Peek();
                    }
                }
            }

            list.Add( queue.Dequeue() );
            return list;
        }

        public static IEnumerable<T> Dequeue<T>( this Queue<T> queue, int count ) {
            var list = new List<T>();

            for( int i = 0; i < count; i++ ) {
                list.Add( queue.Dequeue() );
            }
            return list;
        }

        public static IEnumerable<T> DequeueWhile<T>( this Queue<T> queue, Func<T, int, bool> expression ) {
            var list = new List<T>();

            if( queue.Any() ) {
                int i = 0;
                T peek = queue.Peek();
                while( expression( peek, i++ )
                       && queue.Any() ) {
                    list.Add( queue.Dequeue() );
                    if( queue.Any() ) {
                        peek = queue.Peek();
                    }
                }
            }
            return list;
        }
    }
}
