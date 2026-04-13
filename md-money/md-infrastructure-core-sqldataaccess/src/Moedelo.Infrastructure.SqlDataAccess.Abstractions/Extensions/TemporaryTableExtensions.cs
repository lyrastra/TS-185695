using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;

public static class TemporaryTableExtensions
{
    public static TemporaryTable ToTemporaryTable<TRow>(this IEnumerable<TRow>? collection,
        string tableName,
        string[]? propertyOrder = null)
        where TRow : class
    {
        var tempTableName = $"#{tableName}";

        var columns = typeof(TRow).GetDataColumnList(propertyOrder);
        var dataTable = columns.CreateAndFillDataTable(tempTableName, collection ?? []);
        var tableSqlDeclaration = columns.GetTableDeclarationSql(dataTable.TableName);

        return new TemporaryTable(tempTableName, tableSqlDeclaration, dataTable);
    }

    public static TemporaryTable ToTemporaryTableDistinct<T>(this IEnumerable<T>? collection, IEqualityComparer<T> comparer,
        string tableName, string[]? propertyOrder = null)
        where T : class
    {
        if (collection == null)
        {
            return Array.Empty<T>().ToTemporaryTable(tableName, propertyOrder);
        }

        var collectionDistinct = collection as IReadOnlySet<T> ?? collection.ToHashSet(comparer);

        return collectionDistinct.ToTemporaryTable(tableName, propertyOrder);
    }

    public static TemporaryTable ToTempBigIntIds(this IEnumerable<long>? collection,
        string tableName)
    {
        if (collection == null)
        {
            return Array.Empty<BigIntIds>().ToTemporaryTable(tableName);
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
            
        return collectionDistinct.Select(x => new BigIntIds { Id = x }).ToTemporaryTable(tableName);
    }

    public static TemporaryTable ToTempIntIds(this IEnumerable<int>? collection,
        string tableName)
    {
        if (collection == null)
        {
            return Array.Empty<IntIds>().ToTemporaryTable(tableName);
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
            
        return collectionDistinct.Select(x => new IntIds { Id = x }).ToTemporaryTable(tableName);
    }
    
    public static TemporaryTable ToTemporaryStringIdTable(this IEnumerable<string>? collection,
        string tableName)
    {
        if (collection == null)
        {
            return Array.Empty<StringIds>().ToTemporaryTable(tableName);
        }

        var collectionDistinct = collection as IReadOnlySet<string> ?? collection.ToHashSet();

        return collectionDistinct.Select(value => new StringIds{Id = value}).ToTemporaryTable(tableName);
    }


    private sealed class BigIntIds
    {
        public long Id { get; set; }
    }

    private sealed class IntIds
    {
        public int Id { get; set; }
    }

    private sealed class StringIds
    {
        public string? Id { get; set; }
    }
}