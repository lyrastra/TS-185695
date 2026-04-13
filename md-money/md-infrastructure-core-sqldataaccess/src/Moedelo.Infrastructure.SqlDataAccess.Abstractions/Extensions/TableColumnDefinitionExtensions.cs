using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;

internal static class TableColumnDefinitionExtensions
{
    internal static string GetTableDeclarationSql(this TableColumnDefinition[] columns, string tempTableName)
    {
        var sqlBuilder = new StringBuilder();
        sqlBuilder.Append("create table ");
        sqlBuilder.Append(tempTableName);
        sqlBuilder.Append('(');
        var columnDeclarations = string.Join(", ", columns.Select(column => column.ColumnDeclaration));
        sqlBuilder.Append(columnDeclarations);
        sqlBuilder.Append(')');
        
        return sqlBuilder.ToString();
    }

    internal static DataTable CreateDataTable(this TableColumnDefinition[] columns, string tempTableName)
    {
        var dataTable = new DataTable(tempTableName);
        
        foreach (var columnDefinition in columns)
        {
            // Create a new DataColumn instance to avoid sharing between DataTables
            dataTable.Columns.Add(new DataColumn(columnDefinition.DataColumn.ColumnName)
            {
                DataType = columnDefinition.DataColumn.DataType,
                AllowDBNull = columnDefinition.DataColumn.AllowDBNull
            });
        }

        return dataTable;
    }

    internal static DataTable CreateAndFillDataTable<T>(this TableColumnDefinition[] columns, string tempTableName, IEnumerable<T>? collection)
    {
        var dataTable = columns.CreateDataTable(tempTableName);

        foreach (var item in collection ?? [])
        {
            var rowCellsList = item.CreateDataTableRow(columns);
            dataTable.Rows.Add(rowCellsList);
        }

        return dataTable;
    }

    internal static object[] CreateDataTableRow<T>(this T entity, TableColumnDefinition[] columns)
    {
        return columns
            .Select(x => x.PropertyInfo)
            .Select(x => x.PropertyType.IsEnum
                ? Convert.ChangeType(x.GetValue(entity), Enum.GetUnderlyingType(x.PropertyType)) ?? DBNull.Value 
                : (x.GetValue(entity) ?? DBNull.Value))
            .ToArray();
    }

    internal static TableColumnDefinition[] OrderProperties(this TableColumnDefinition[] propertyInfos, string[]? propertyOrder)
    {
        if (propertyOrder is not {Length: > 0})
        {
            return propertyInfos;
        }

        var unorderedOffset = propertyOrder.Length + 1;

        var result = propertyInfos
            .Select(p =>
            {
                var order = Array.IndexOf(propertyOrder!, p.PropertyName);

                if(order == -1)
                {
                    order = unorderedOffset++;
                }

                return KeyValuePair.Create(order, p);
            })
            .OrderBy(kv => kv.Key)
            .Select(kv => kv.Value)
            .ToArray();

        return result;
    }
}
