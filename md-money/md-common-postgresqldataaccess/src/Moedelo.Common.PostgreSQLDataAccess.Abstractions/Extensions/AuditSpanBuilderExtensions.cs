using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.PostgreSqlDataAccess.Abstractions.Extensions;

internal static class AuditSpanBuilderExtensions
{
    internal static IAuditSpanBuilder WithConnectionString(
        this IAuditSpanBuilder spanBuilder,
        string connectionString)
    {
        spanBuilder.WithTag("DbConnectionString", connectionString);
            
        return spanBuilder;
    }
        
    internal static IAuditSpanBuilder WithQueryObject(
        this IAuditSpanBuilder spanBuilder,
        IQueryObject queryObject)
    {
        try
        {
            if (spanBuilder.IsEnabled == false)
            {
                return spanBuilder;
            }
                
            spanBuilder.WithTag("QueryObject", new
            {
                TemporaryTables = Convert(queryObject.TemporaryTables),
                Sql = queryObject.Sql,
                Params = queryObject.QueryParams,
                CommandType = queryObject.CommandType,
            });
        }
        catch
        {
            //ignore
        }
            
        return spanBuilder;
    }
        
    internal static IAuditSpanBuilder WithQueryObject(
        this IAuditSpanBuilder spanBuilder,
        IQueryObjectWithDynamicParams queryObject)
    {
        try
        {
            if (spanBuilder.IsEnabled == false)
            {
                return spanBuilder;
            }
                
            spanBuilder.WithTag("QueryObject", new
            {
                TemporaryTables = Convert(queryObject.TemporaryTables),
                Sql = queryObject.Sql,
                Params = queryObject.DynamicParams,
                CommandType = queryObject.CommandType,
            });
        }
        catch
        {
            //ignore
        }
            
        return spanBuilder;
    }
        
    internal static IAuditSpanBuilder WithQueryObject(
        this IAuditSpanBuilder spanBuilder,
        IBulkCopyQueryObject queryObject)
    {
        try
        {
            if (spanBuilder.IsEnabled == false)
            {
                return spanBuilder;
            }
                
            spanBuilder.WithTag("QueryObject", new
            {
                Name = queryObject.TableName,
                Data = new
                {
                    Data = Convert(queryObject.TableHeaders, queryObject.TableRows),
                    TotalCount = queryObject.TableRows?.Count ?? 0
                }
            });
        }
        catch
        {
            //ignore
        }

        return spanBuilder;
    }

    private static IReadOnlyCollection<object> Convert(
        IReadOnlyCollection<TemporaryTable> temporaryTables)
    {
        if (temporaryTables == null || temporaryTables.Count == 0)
        {
            return Array.Empty<IReadOnlyCollection<IReadOnlyDictionary<string, object>>>();
        }
            
        return temporaryTables.Select(Convert).ToArray();
    }
        
    private static object Convert(TemporaryTable temporaryTable)
    {
        return new
        {
            Name = temporaryTable.Name,
            CreateSql = temporaryTable.CreateSql,
            Data = Convert(temporaryTable.DataTable),
            TotalCount = temporaryTable.DataTable.Rows.Count
        };
    }

    // ограничиваем максимальное количество строк, которое попадает в дамп
    private static readonly int MaxRowsToDump = 100;
        
    private static Dictionary<string, object>[] Convert(DataTable dataTable)
    {
        var rowsCount = Math.Min(MaxRowsToDump, dataTable.Rows.Count);
        var array = new Dictionary<string, object>[rowsCount];

        for (int i = 0; i < rowsCount; i++)
        {
            var row = dataTable.Rows[i];
            var dict = new Dictionary<string, object>();
            var colsCount = dataTable.Columns.Count;

            for (int j = 0; j < colsCount; j++)
            {
                var col = dataTable.Columns[j];
                dict[col.ColumnName] = row[col];
            }

            array[i] = dict;
        }

        return array;
    }
        
    private static Dictionary<string, object>[] Convert(
        IList<DbTypedColumnInfo> columns,
        IList<IReadOnlyList<object>> rows)
    {
        if (columns is null || rows is null)
            return null;
            
        var rowsCount = Math.Min(MaxRowsToDump, rows.Count);
        var array = new Dictionary<string, object>[rowsCount];

        for (var i = 0; i < rowsCount; i++)
        {
            var row = rows[i];
            var dict = new Dictionary<string, object>();
            var colsCount = columns.Count;

            for (var j = 0; j < colsCount; j++)
            {
                var col = columns[j];
                dict[col.ColumnName] = row[j];
            }

            array[i] = dict;
        }

        return array;
    }
}