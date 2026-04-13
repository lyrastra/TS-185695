using System;
using System.Collections.Generic;

namespace Moedelo.Infrastructure.System.Extensions.Collections
{
    public static class CollectionsExtensions
    {
        public static IReadOnlyCollection<T> ToDistinctReadOnlyCollection<T>(
            this IReadOnlyCollection<T> source,
            IEqualityComparer<T> equalityComparer = null)
        {
            switch (source)
            {
                case null:
                    return Array.Empty<T>();
                case IReadOnlyCollection<T> readOnlyCollection when readOnlyCollection.Count == 0:
                    return readOnlyCollection;
                case ISet<T> _:
                    return source;
                default:
                    return new HashSet<T>(source, equalityComparer);
            }
        }
        
        public static bool NullOrEmpty<T>(this IReadOnlyCollection<T> source)
        {
            return source == null || source.Count == 0;
        }

        public static bool NotNullOrEmpty<T>(this IReadOnlyCollection<T> source)
        {
            return source != null && source.Count > 0;
        }

        public static TR[] MapToArray<T, TR>(this IReadOnlyCollection<T> source, Func<T, TR> mapElementFunc)
        {
            if (source.NullOrEmpty())
            {
                return Array.Empty<TR>();
            }

            var result = new TR[source.Count];
            var i = 0;

            foreach (var element in source)
            {
                result[i] = mapElementFunc(element);
                i++;
            }

            return result;
        }
    }
}