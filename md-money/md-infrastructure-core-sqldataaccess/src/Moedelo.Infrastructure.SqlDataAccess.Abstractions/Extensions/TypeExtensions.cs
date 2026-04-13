using System;
using System.Collections.Concurrent;
using System.Data;
using System.Linq;
using System.Reflection;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;

internal static class TypeExtensions
{
    private static readonly ConcurrentDictionary<Type, TableColumnDefinition[]> DbTypeTableColumnDefinitions = new ();

    internal static TableColumnDefinition[] GetDataColumnList(this Type type, string[]? propertyOrder = null)
    {
        var columns = DbTypeTableColumnDefinitions
            .GetOrAdd(type, CreateDbColumnDefinitions)
            .OrderProperties(propertyOrder);

        return columns;
    }

    private static TableColumnDefinition[] CreateDbColumnDefinitions(Type type)
    {
        const BindingFlags propertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
        var propertyInfos = type.GetProperties(propertyFlags);

        return propertyInfos
            .Select(propertyInfo =>
            {
                var propertyType = propertyInfo.PropertyType;
                var dbType = propertyType.GetDbTypeName();
                var nullable = propertyType.CanBeNull() ? "null" : "not null";
                var column = new DataColumn(propertyInfo.Name)
                {
                    DataType = propertyType.GetColumnDataType(),
                    AllowDBNull = propertyType.CanBeNull()
                };
                var columnDeclaration = $"{propertyInfo.Name} {dbType} {nullable}";

                return new TableColumnDefinition(propertyInfo, columnDeclaration);
            })
            .ToArray();
    }
}
