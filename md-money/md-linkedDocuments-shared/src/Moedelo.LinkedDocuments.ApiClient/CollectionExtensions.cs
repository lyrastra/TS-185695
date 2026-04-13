using System.Collections.Generic;

namespace Moedelo.LinkedDocuments.ApiClient
{
    internal static class CollectionExtensions
    {
        internal static IReadOnlyCollection<T> ToDistinctReadOnlyCollection<T>(
            this IReadOnlyCollection<T> source,
            IEqualityComparer<T> equalityComparer = null)
        {
            switch (source)
            {
                case null:
                    return null;
                case IReadOnlyCollection<T> readOnlyCollection when readOnlyCollection.Count == 0:
                    return readOnlyCollection;
                case ISet<T> _:
                    return source;
                default:
                    return new HashSet<T>(source, equalityComparer);
            }
        }
    }
}