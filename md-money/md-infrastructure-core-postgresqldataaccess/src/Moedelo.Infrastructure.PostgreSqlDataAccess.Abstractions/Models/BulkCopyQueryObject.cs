using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Helpers;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

public sealed class BulkCopyQueryObject : IBulkCopyQueryObject
{
    public BulkCopyQueryObject(string tableName, 
        IList<DbTypedColumnInfo> tableHeaders, 
        IList<IReadOnlyList<object>> tableRows)
    {
        if (string.IsNullOrEmpty(tableName))
        {
            throw new ArgumentNullException(nameof(tableName));
        }

        TableName = tableName;
        TableHeaders = tableHeaders ?? throw new ArgumentNullException(nameof(tableHeaders));
        TableRows = tableRows ?? throw new ArgumentNullException(nameof(tableRows));
    }

    public static BulkCopyQueryObject FromCollection<TRow>(string tableName, IEnumerable<TRow> collection)
        where TRow : class
    {
        _ = (tableName is {Length: > 0} ? tableName : null) ?? throw new ArgumentNullException(nameof(tableName));  
        _ = collection ?? throw new ArgumentNullException(nameof(collection));

        var propertyInfoList = typeof(TRow).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
        var tableHeaderItemList = GetTableHeader(typeof(TRow), propertyInfoList);
        
        var tableData = collection
            .Select(collectionRow => propertyInfoList.GetBulkCopyRowValuesList(collectionRow))
            .ToList();

        return new BulkCopyQueryObject(tableName, tableHeaderItemList, tableData);
    }

    private static List<DbTypedColumnInfo> GetTableHeader(Type rowType, PropertyInfo[] propertyInfos)
    {
        var tableHeaders = new List<DbTypedColumnInfo>(propertyInfos.Length);

        for (var i = 0; i < propertyInfos.Length; i++)
        {
            var propertyInfo = propertyInfos[i];
            var columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>()
                                  ?? throw new ArgumentNullException($"Не найден ColumnAttribute для свойства {rowType.Name}.{propertyInfo.Name}");
            var columnName = columnAttribute.Name ?? propertyInfo.Name;
            var dbType = columnAttribute.TypeName ?? propertyInfo.PropertyType.GetDbTypeName();
            tableHeaders.Add(new DbTypedColumnInfo(columnName, dbType));
        }

        return tableHeaders;
    }

    public string TableName { get; }

    public IList<DbTypedColumnInfo> TableHeaders { get; }

    public IList<IReadOnlyList<object>> TableRows { get; }
}