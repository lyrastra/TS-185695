using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Moedelo.InfrastructureV2.MySqlDataAccess.Extensions;

public static class TemporaryTableExtensions
{
    public static TemporaryTable ToTemporaryTable<T>(this IEnumerable<T> collection, string tableName)
        where T : class
    {
        var type = typeof(T);
        var tempTableName = $"{tableName}";
        var createTableSqlBuilder = new StringBuilder();
        var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
            
        createTableSqlBuilder.Append($"create temporary table {tempTableName} (");
            
        for (var i = 0; i < propertyInfos.Length; i++)
        {
            var property = propertyInfos[i];
            var propertyType = property.PropertyType;
            var dbType = propertyType.GetDbTypeName();
            var nullable = propertyType.CanBeNull() ? "null" : "not null";
                
            createTableSqlBuilder.Append($"{(i == 0 ? "" : " , ")}{property.Name} {dbType} {nullable}");
        }

        createTableSqlBuilder.Append(")");

        var data = new StringBuilder();
            
        foreach (var item in collection)
        {
            data.AppendLine(string.Join(";", propertyInfos.Select(x => x.GetValue(item).ToString())));
        }

        return new TemporaryTable(tempTableName, createTableSqlBuilder.ToString(), data.ToString());
    }
}