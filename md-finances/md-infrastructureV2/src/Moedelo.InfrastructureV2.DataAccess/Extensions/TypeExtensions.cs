using System;
using System.Data;
using System.Text;
using Moedelo.InfrastructureV2.DataAccess.Internals;
using Moedelo.InfrastructureV2.Domain.Helpers;

namespace Moedelo.InfrastructureV2.DataAccess.Extensions;

internal static class TypeExtensions
{
    internal static TemporaryTable CreateTemporaryTable(this DataTable dataTable)
    {
        var creationSql = dataTable.CreateTemporaryTableSql();

        return new TemporaryTable(dataTable, creationSql);
    }

    private static string CreateTemporaryTableSql(this DataTable dataTable)
    {
        var sql = new StringBuilder();

        sql.Append($"create table #{dataTable.TableName} (");

        for (var i = 0; i < dataTable.Columns.Count; i++)
        {
            var dataColumn = dataTable.Columns[i];
            var columnType = dataColumn.DataType;
            var sqlType = SqlHelper.GetDbTypeName(columnType);
            var isNullable = columnType.IsGenericType
                             && columnType.GetGenericTypeDefinition() == typeof(Nullable<>)
                             || columnType == typeof(string);
                
            var nullableExpression = dataColumn.AllowDBNull ? "null" : "not null";
            var delimiter = (i == 0 ? "" : ",");

            sql.Append($"{delimiter}{dataColumn.ColumnName} {sqlType} {nullableExpression}");
        }

        sql.Append(")");

        return sql.ToString();
    }
}
