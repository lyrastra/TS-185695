using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Extensions;

internal static class PropertyInfoExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static IReadOnlyList<object> GetBulkCopyRowValuesList<TRow>(this PropertyInfo[] propertyInfos, TRow row) where TRow : class
    {
        return propertyInfos
            .Select(propertyInfo => GetValue(row, propertyInfo))
            .ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static object GetValue<TRow>(TRow row, PropertyInfo propertyInfo) where TRow : class
    {
        return propertyInfo.PropertyType.IsEnum
            ? propertyInfo.GetValue(row).ToString()
            : (propertyInfo.GetValue(row) ?? DBNull.Value);
    }
}
