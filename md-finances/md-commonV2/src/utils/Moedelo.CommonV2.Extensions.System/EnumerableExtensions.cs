using System.Collections.Generic;
using System.Linq;

namespace Moedelo.CommonV2.Extensions.System
{
    public static class EnumerableExtensions
    {
        public static bool ContainsAll<T>(this IEnumerable<T> source, params T[] values)
        {
            return values.All(source.Contains);
        }

        public static bool ContainsAny<T>(this IEnumerable<T> source, params T[] values)
        {
            return values.Any(source.Contains);
        }
        
        public static bool ContainsAll<T>(this ISet<T> source, params T[] values)
        {
            return values.All(source.Contains);
        }

        public static bool ContainsAny<T>(this ISet<T> source, params T[] values)
        {
            return values.Any(source.Contains);
        }

        public static ISet<T> AsSet<T>(this IEnumerable<T> source)
        {
            return source as ISet<T> ?? source.ToHashSet();
        }
    }
}