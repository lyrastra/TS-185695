using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

public sealed class QueryObjectWithDynamicParams : IQueryObjectWithDynamicParams
{
    public QueryObjectWithDynamicParams(
        string sql, 
        IReadOnlyCollection<DynamicParam> dynamicParams,
        IReadOnlyCollection<TemporaryTable>? temporaryTables = null,
        CommandType? commandType = null)
    {
        if (string.IsNullOrEmpty(sql))
        {
            throw new ArgumentNullException(nameof(sql));
        }

        if (dynamicParams is not {Count: > 0})
        {
            throw new ArgumentNullException(nameof(dynamicParams));
        }

        Sql = sql;
        DynamicParams = dynamicParams;
        TemporaryTables = temporaryTables;
        CommandType = commandType;
    }

    public string Sql { get; }

    public IReadOnlyCollection<DynamicParam> DynamicParams { get; }
        
    public IReadOnlyCollection<TemporaryTable>? TemporaryTables { get; }

    public CommandType? CommandType { get; }

    public string? AuditTrailSpanName { get; private set; } = null;

    public QueryObjectWithDynamicParams WithAuditTrailSpanName(string name)
    {
        Debug.Assert(name == null || name.Length > 0, "AuditTrailSpanName can be null or non empty");
        AuditTrailSpanName = name;

        return this;
    }
}