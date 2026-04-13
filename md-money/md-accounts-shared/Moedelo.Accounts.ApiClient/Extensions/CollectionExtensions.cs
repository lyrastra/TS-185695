using System;
using System.Collections.Generic;

namespace Moedelo.Accounts.Clients.Extensions
{
    internal static class CollectionExtensions
    {
        private const int MaxCollectionLength = 10000;

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
}