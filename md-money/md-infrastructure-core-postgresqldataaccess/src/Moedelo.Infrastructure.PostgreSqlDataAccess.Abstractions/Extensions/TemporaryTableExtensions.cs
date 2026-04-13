using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Extensions
{
    public static class TemporaryTableExtensions
    {
        /// <summary>
        /// Все свойства collection обязательно должны быть размечены аттрибутами System.ComponentModel.DataAnnotations.Schema.ColumnAttribute
        /// Пример - [Column("create_date")]
        /// </summary>
        public static TemporaryTable ToTemporaryTable<T>(this IEnumerable<T> collection, string tableName, bool autoPrefix = false, string[] propertyOrder = null)
            where T : class
        {
            var type = typeof(T);
            var tempTableName = autoPrefix ? $"temp_{tableName}" : tableName;
            var dataTable = new DataTable(tempTableName);
            var createTableSqlBuilder = new StringBuilder();
            createTableSqlBuilder.Append($"create temporary table {tempTableName} (");

            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
            
            if (propertyOrder?.Length > 0)
            {
                propertyInfos = OrderProperties(propertyInfos, propertyOrder);
            }
            
            var columns = propertyInfos.Select(x => x.GetCustomAttribute<ColumnAttribute>()).ToArray(); //Будет намерено падать при отсутствии атрибута
            
            for (var i = 0; i < propertyInfos.Length; i++)
            {
                var property = propertyInfos[i];
                var propertyType = property.PropertyType;
                var dbType = columns[i].TypeName ?? propertyType.GetDbTypeName();
                var nullable = propertyType.CanBeNull() ? "null" : "not null";
                var columnName = columns[i].Name ?? property.Name;
                createTableSqlBuilder.Append($"{(i == 0 ? "" : " , ")}{columnName} {dbType} {nullable}");
                var column = new DataColumn(columnName)
                {
                    DataType = propertyType.GetColumnDataType()
                };
                dataTable.Columns.Add(column);
            }

            createTableSqlBuilder.Append(")");

            if (collection == null)
            {
                return new TemporaryTable(tempTableName, createTableSqlBuilder.ToString(), dataTable);
            }
            
            foreach (var item in collection)
            {
                var cells = propertyInfos.Select(x => x.PropertyType.IsEnum
                        ? x.GetValue(item).ToString()
                        : (x.GetValue(item) ?? DBNull.Value))
                    .ToArray();
                dataTable.Rows.Add(cells);
            }

            return new TemporaryTable(tempTableName, createTableSqlBuilder.ToString(), dataTable);
        }
        
        public static TemporaryTable ToTemporaryTableDistinct<T>(this IEnumerable<T> collection, IEqualityComparer<T> comparer, string tableName, bool autoPrefix = false, string[] propertyOrder = null)
            where T : class
        {
            if (collection == null)
            {
                return Array.Empty<T>().ToTemporaryTable(tableName, autoPrefix, propertyOrder);
            }
            
            ISet<T> collectionDistinct;

            if (collection is ISet<T> hashSet)
            {
                collectionDistinct = hashSet;
            }
            else
            {
                collectionDistinct = new HashSet<T>(collection, comparer);
            }
            
            return collectionDistinct.ToTemporaryTable(tableName, autoPrefix, propertyOrder);
        }

        public static TemporaryTable ToTempBigIntIds(this IEnumerable<long> collection, string tableName, bool autoPrefix = false)
        {
            if (collection == null)
            {
                return Array.Empty<BigIntIds>().ToTemporaryTable(tableName, autoPrefix);
            }
            
            ISet<long> collectionDistinct;
            
            if (collection is ISet<long> hashSet)
            {
                collectionDistinct = hashSet;
            }
            else
            {
                collectionDistinct = new HashSet<long>(collection);
            }
            
            return collectionDistinct.Select(x => new BigIntIds { Id = x }).ToTemporaryTable(tableName, autoPrefix);
        }

        public static TemporaryTable ToTempIntIds(this IEnumerable<int> collection, string tableName, bool autoPrefix = false)
        {
            if (collection == null)
            {
                return Array.Empty<IntIds>().ToTemporaryTable(tableName, autoPrefix);
            }
            
            ISet<int> collectionDistinct;
            
            if (collection is ISet<int> hashSet)
            {
                collectionDistinct = hashSet;
            }
            else
            {
                collectionDistinct = new HashSet<int>(collection);
            }
            
            return collectionDistinct.Select(x => new IntIds { Id = x }).ToTemporaryTable(tableName, autoPrefix);
        }
        
        private static PropertyInfo[] OrderProperties(PropertyInfo[] propertyInfos, string[] propertyOrder)
        {
            int unorderedOffset = 1000;
            var result = propertyInfos
                .Select(p =>
                {
                    int order = Array.IndexOf(propertyOrder, p.Name);
                    
                    if(order == -1)
                    {
                        order = unorderedOffset++;
                    }
                    
                    return new KeyValuePair<int, PropertyInfo>(order, p);
                })
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value)
                .ToArray();

            return result;
        }
        
        private class BigIntIds
        {
            [Column("id")]
            public long Id { get; set; }
        }
        
        private class IntIds
        {
            [Column("id")]
            public int Id { get; set; }
        }
    }
}