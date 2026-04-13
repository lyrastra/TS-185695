using System;
using System.Collections;
using System.Collections.Generic;
using Moedelo.Infrastructure.System.Extensions.Collections.Wrappers;

namespace Moedelo.Infrastructure.System.Extensions.Collections
{
    public static class EnumerableChunkEx
    {
        /// <summary>
        /// Разбивает последовательность элементов на набор подпоследовательностей, в каждой из которых элементы равны по указанному признаку
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer">Функтор сравнения двух элементов</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            var wrapper = new EnumeratorWrapper<T>(source);
            var currentPos = 0;

            try
            {
                wrapper.AddRef();
                T first;
                while (wrapper.Get(currentPos, out first))
                {
                    var chunkSize = 1;
                    T next;
                    while (wrapper.Get(currentPos + chunkSize, out next) && comparer(first, next))
                    {
                        ++chunkSize;
                    }
                    yield return new ChunkedEnumerable<T>(wrapper, chunkSize, currentPos);
                    currentPos += chunkSize;
                }
            }
            finally
            {
                wrapper.RemoveRef();
            }
        }

        /// <summary>
        /// Разбивает последовательность элементов на набор подпоследовательностей, в каждой из которых элементы равны по указанному признаку
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            var wrapper = new EnumeratorWrapper<T>(source);
            var currentPos = 0;

            try
            {
                wrapper.AddRef();
                T first;
                while (wrapper.Get(currentPos, out first))
                {
                    var chunkSize = 1;
                    T next;
                    while (wrapper.Get(currentPos + chunkSize, out next) && comparer.Equals(first, next))
                    {
                        ++chunkSize;
                    }
                    yield return new ChunkedEnumerable<T>(wrapper, chunkSize, currentPos);
                    currentPos += chunkSize;
                }
            }
            finally
            {
                wrapper.RemoveRef();
            }
        }

        /// <summary>
        /// Разбивает последовательность элементов на набор подпоследовательностей указанной длины
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            if (chunkSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(chunkSize), "Chunk size must be positive");
            }

            var wrapper = new EnumeratorWrapper<T>(source);
            var currentPos = 0;

            try
            {
                wrapper.AddRef();
                T ignore;
                while (wrapper.Get(currentPos, out ignore))
                {
                    yield return new ChunkedEnumerable<T>(wrapper, chunkSize, currentPos);
                    currentPos += chunkSize;
                }
            }
            finally
            {
                wrapper.RemoveRef();
            }
        }

        internal class ChunkedEnumerable<T> : IEnumerable<T>
        {
            private readonly int chunkSize;
            private readonly int start;

            private readonly EnumeratorWrapper<T> wrapper;

            public ChunkedEnumerable(EnumeratorWrapper<T> wrapper, int chunkSize, int start)
            {
                this.wrapper = wrapper;
                this.chunkSize = chunkSize;
                this.start = start;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new ChildEnumerator(this);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private class ChildEnumerator : IEnumerator<T>
            {
                private readonly ChunkedEnumerable<T> parent;
                private T current;
                private bool done;
                private int position;

                public ChildEnumerator(ChunkedEnumerable<T> parent)
                {
                    this.parent = parent;
                    position = -1;
                    parent.wrapper.AddRef();
                }

                public T Current
                {
                    get
                    {
                        if (position == -1 || done)
                        {
                            throw new InvalidOperationException();
                        }
                        return current;
                    }
                }

                public void Dispose()
                {
                    if (!done)
                    {
                        done = true;
                        parent.wrapper.RemoveRef();
                    }
                }

                object IEnumerator.Current => Current;

                public bool MoveNext()
                {
                    position++;

                    if (position + 1 > parent.chunkSize)
                    {
                        done = true;
                    }

                    if (!done)
                    {
                        done = !parent.wrapper.Get(position + parent.start, out current);
                    }

                    return !done;
                }

                public void Reset()
                {
                    // per http://msdn.microsoft.com/en-us/library/system.collections.ienumerator.reset.aspx
                    throw new NotSupportedException();
                }
            }
        }
    }
}