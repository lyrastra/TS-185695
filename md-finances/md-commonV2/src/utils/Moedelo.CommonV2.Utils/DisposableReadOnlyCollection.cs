using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.CommonV2.Utils
{
    public sealed class DisposableReadOnlyCollection<TDisposable> : IReadOnlyCollection<TDisposable>, IDisposable
        where TDisposable : IDisposable
    {
        private IReadOnlyCollection<TDisposable> collection;

        public DisposableReadOnlyCollection(IReadOnlyCollection<TDisposable> collection)
        {
            this.collection = collection;
        }

        public void Dispose()
        {
            var list = collection;
            collection = Array.Empty<TDisposable>();

            foreach (var item in list.Reverse())
            {
                item.Dispose();
            }
        }

        public IEnumerator<TDisposable> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)collection).GetEnumerator();
        }

        public int Count => collection.Count;
    }
}
