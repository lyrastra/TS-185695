using System;
using System.Collections.Generic;
using System.Data;

namespace Moedelo.InfrastructureV2.MySqlDataAccess;

public sealed class QueryObject
{
    public QueryObject(
        string sql,                      
        object queryParams,
        TemporaryTable temporaryTable,
        CommandType? commandType = null) : this(sql, queryParams, commandType, new[] { temporaryTable })
    {
    }

    public QueryObject(
        string sql,
        object queryParams = null,
        CommandType? commandType = null,
        IReadOnlyCollection<TemporaryTable> temporaryTables = null)
    {
        if (string.IsNullOrEmpty(sql))
        {
            throw new ArgumentNullException(nameof(sql));
        }

        Sql = sql;
        QueryParams = queryParams;
        TemporaryTables = temporaryTables;
        CommandType = commandType;
    }

    public string Sql { get; }

    public object QueryParams { get; }
        
    public IReadOnlyCollection<TemporaryTable> TemporaryTables { get; }

    public CommandType? CommandType { get; }
}