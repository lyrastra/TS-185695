using System.Collections.Generic;

namespace Moedelo.Infrastructure.System.Extensions.Collections.Wrappers
{
    /// <summary>
    /// (используется в реализации Chunk)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumeratorWrapper<T>
    {
        private Enumeration currentEnumeration;

        private int refs;

        public EnumeratorWrapper(IEnumerable<T> source)
        {
            SourceEumerable = source;
        }

        private IEnumerable<T> SourceEumerable { get; }

        public bool Get(int pos, out T item)
        {
            if (currentEnumeration != null && currentEnumeration.Position > pos)
            {
                currentEnumeration.Source.Dispose();
                currentEnumeration = null;
            }

            if (currentEnumeration == null)
            {
                currentEnumeration = new Enumeration
                {
                    Position = -1,
                    Source = SourceEumerable.GetEnumerator(),
                    AtEnd = false
                };
            }

            item = default(T);
            if (currentEnumeration.AtEnd)
            {
                return false;
            }

            while (currentEnumeration.Position < pos)
            {
                currentEnumeration.AtEnd = !currentEnumeration.Source.MoveNext();
                currentEnumeration.Position++;

                if (currentEnumeration.AtEnd)
                {
                    return false;
                }
            }

            item = currentEnumeration.Source.Current;

            return true;
        }

        // needed for dispose semantics
        public void AddRef()
        {
            refs++;
        }

        public void RemoveRef()
        {
            refs--;
            if (refs == 0 && currentEnumeration != null)
            {
                var copy = currentEnumeration;
                currentEnumeration = null;
                copy.Source.Dispose();
            }
        }

        private class Enumeration
        {
            public IEnumerator<T> Source { get; set; }
            public int Position { get; set; }
            public bool AtEnd { get; set; }
        }
    }
}