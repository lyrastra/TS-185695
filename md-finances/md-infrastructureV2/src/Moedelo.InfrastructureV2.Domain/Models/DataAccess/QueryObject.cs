#nullable enable
using System;
using System.Data;
using System.Diagnostics;

namespace Moedelo.InfrastructureV2.Domain.Models.DataAccess;

public class QueryObject : IQueryObject
{
    public QueryObject(string sql, object? queryParams = null, CommandType? commandType = null)
    {
        if (string.IsNullOrEmpty(sql))
        {
            throw new ArgumentNullException(nameof(sql));
        }

        Sql = sql;
        QueryParams = queryParams;
        CommandType = commandType;
    }

    public string Sql { get; private set; }

    public object? QueryParams { get; private set; }

    public CommandType? CommandType { get; private set; }

    public string? AuditTrailSpanName { get; private set; }

    public QueryObject WithAuditTrailSpanName(string name)
    {
        Debug.Assert(name == null || name.Length > 0, "AuditTrailSpanName can be null or non empty");
        AuditTrailSpanName = name;

        return this;
    }
}
