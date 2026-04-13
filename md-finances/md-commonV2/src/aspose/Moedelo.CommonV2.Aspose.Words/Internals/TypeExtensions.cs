using System;
using System.Collections;
using System.Collections.Generic;

namespace Moedelo.CommonV2.Reports.Aspose.Words.Internals
{
    internal static class TypeExtensions
    {
        public static bool IsTable(this Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type) &&
                   type != typeof(string) &&
                   !IsDictionary(type);
        }

        public static bool IsImage(this Type type)
        {
            return type == typeof(byte[]);
        }

        public static bool IsDictionary(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }

        public static bool IsComplexObject(this Type type)
        {
            return type.IsClass && type != typeof(string);
        }
    }
}