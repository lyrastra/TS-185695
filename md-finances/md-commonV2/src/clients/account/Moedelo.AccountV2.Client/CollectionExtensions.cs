using System;
using System.Collections.Generic;

namespace Moedelo.AccountV2.Client;

internal static class CollectionExtensions
{
    private const int MaxCollectionLength = 10000;

    internal static IReadOnlyCollection<int> AsSet(this IReadOnlyCollection<int> collection)
    {
        if (collection == null)
        {
            return Array.Empty<int>();
        }

        if (collection is ISet<int>)
        {
            return collection;
        }

        return new HashSet<int>(collection);
    }
        
    internal static IReadOnlyCollection<long> AsSet(this IReadOnlyCollection<long> collection)
    {
        if (collection == null)
        {
            return Array.Empty<long>();
        }

        if (collection is ISet<long>)
        {
            return collection;
        }

        return new HashSet<long>(collection);
    }
        
    internal static IReadOnlyCollection<string> AsSet(this IReadOnlyCollection<string> collection)
    {
        if (collection == null)
        {
            return Array.Empty<string>();
        }

        if (collection is ISet<string>)
        {
            return collection;
        }

        return new HashSet<string>(collection);
    }

    internal static void Validation(this IReadOnlyCollection<int> collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        if (collection.Count > MaxCollectionLength)
        {
            throw new ArgumentException($"Invalid collection length, max-length: {MaxCollectionLength}");
        }
    }
}