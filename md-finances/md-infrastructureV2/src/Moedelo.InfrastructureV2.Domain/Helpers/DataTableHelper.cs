using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Moedelo.InfrastructureV2.Domain.Helpers;

public static class DataTableHelper
{
    // ReSharper disable once InconsistentNaming
    public static DataTable ToBigIntListTVP(this IEnumerable<long> list) => ToBigIntListTvp(list);
    
    public static DataTable ToBigIntListTvp(this IEnumerable<long> list)
    {
        const string tableName = "BigIntList";
        const string columnName = "Id";

        return list.ToUniqueValuesOneColumnDataTable(tableName, columnName);
    }

    // ReSharper disable once InconsistentNaming
    public static DataTable ToIntListTVP(this IEnumerable<int> list) => ToIntListTvp(list); 

    public static DataTable ToIntListTvp(this IEnumerable<int> list)
    {
        const string tableName = "IntList";
        const string columnName = "Id";

        return list.ToUniqueValuesOneColumnDataTable(tableName, columnName);
    }

    public static DataTable ToVarcharListTvp(this IEnumerable<string> list)
    {
        const string tableName = "VarcharList";
        const string columnName = "Item";

        return list.ToUniqueValuesOneColumnDataTable(tableName, columnName);
    }

    /// <summary>
    /// Конвертирует список объектов в табличную переменную.
    /// Применим для вставки через bulkCopy.
    /// </summary>
    public static DataTable ToListTvp<T>(this IEnumerable<T> list, string tableName = null)
        where T : class
    {
        return ToListTvp(list, typeof(T), tableName);
    }

    /// <summary>
    /// Конвертирует список объектов в табличную переменную.
    /// Применим для вставки через bulkCopy.
    /// </summary>
    public static DataTable ToListTvp<T>(this IEnumerable<T> list, params string[] orderedPropertyList)
        where T : class
    {
        return ToListTvp(list, typeof(T), null, orderedPropertyList);
    }

    /// <summary>
    /// Конвертирует список объектов в табличную переменную.
    /// Применим для вставки через bulkCopy.
    /// </summary>
    public static DataTable ToListTvp(this IEnumerable list, Type listType, string tableName = null,
        params string[] orderedPropertyList)
    {
        var table = new DataTable(tableName);

        var properties = listType
            .EnumerateDataColumnProperties()
            .OrderBy(orderedPropertyList)
            .ToArray();

        foreach (var property in properties)
        {
            var (columnType, allowDbNull) = property.GetDataColumnType();
            
            var column = new DataColumn(property.Name, columnType)
            {
                AllowDBNull = allowDbNull
            };

            table.Columns.Add(column);
        }

        if (list != null)
        {
            foreach (var item in list)
            {
                var cells = properties
                    .Select(item.GetDataRowCellValue)
                    .ToArray();
                table.Rows.Add(cells);
            }
        }

        return table;
    }

    private static IEnumerable<PropertyInfo> EnumerateDataColumnProperties(this IReflect type)
    {
        return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
    }

    private static IEnumerable<PropertyInfo> OrderBy(
        this IEnumerable<PropertyInfo> properties,
        string[] orderedPropertyList)
    {
        if (orderedPropertyList?.Length > 0)
        {
            var unorderedOffset = 1000;

            return properties
                .Select(propertyInfo =>
                {
                    var order = Array.IndexOf(orderedPropertyList, propertyInfo.Name);
                    if (order == -1)
                    {
                        order = unorderedOffset++;
                    }

                    return new KeyValuePair<int, PropertyInfo>(order, propertyInfo);
                })
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value);
        }

        return properties;
    }

    internal static DataColumnType GetDataColumnType(this PropertyInfo property)
    {
        var columnType = property.PropertyType;
        var isNullableGeneric = columnType.IsGenericType && columnType.GetGenericTypeDefinition() == typeof(Nullable<>);

        if (isNullableGeneric)
        {
            var underlyingType = Nullable.GetUnderlyingType(columnType);

            var type = underlyingType!.IsEnum
                ? Enum.GetUnderlyingType(underlyingType)
                : underlyingType;
            
            return new DataColumnType(type, true);
        }

        if (columnType.IsEnum)
        {
            return new DataColumnType(Enum.GetUnderlyingType(columnType), false);
        }

        var allowDbNull = columnType == typeof(string);

        return new DataColumnType(columnType, allowDbNull);
    }

    private static object GetDataRowCellValue(this object item, PropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType.IsEnum)
        {
            var underlyingType = Enum.GetUnderlyingType(propertyInfo.PropertyType);

            return Convert.ChangeType(propertyInfo.GetValue(item), underlyingType);
        }

        return propertyInfo.GetValue(item) ?? DBNull.Value;
    }

    private static DataTable WithColumnValues<T>(this DataTable table, string columnName, IEnumerable<T> list)
    {
        table.Columns.Add(columnName);

        foreach (var value in list ?? Array.Empty<T>())
        {
            table.Rows.Add(value);
        }

        return table;
    }

    private static DataTable ToUniqueValuesOneColumnDataTable<T>(
        this IEnumerable<T> list,
        string tableName,
        string columnName)
    {
        var data = new HashSet<T>(list ?? Array.Empty<T>());

        return new DataTable(tableName)
            .WithColumnValues(columnName, data);
    }
}
