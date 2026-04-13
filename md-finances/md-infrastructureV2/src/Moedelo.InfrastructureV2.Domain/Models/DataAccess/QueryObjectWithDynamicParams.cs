#nullable enable
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Moedelo.InfrastructureV2.Domain.Models.DataAccess;

public class QueryObjectWithDynamicParams : IQueryObjectWithDynamicParams
{
    public QueryObjectWithDynamicParams(string sql, List<DynamicParam> dynamicParams, CommandType? commandType = null)
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
        CommandType = commandType;
    }

    public string Sql { get; private set; }

    public List<DynamicParam> DynamicParams { get; private set; }

    public CommandType? CommandType { get; private set; }

    public string? AuditTrailSpanName { get; private set; }

    public QueryObjectWithDynamicParams WithAuditTrailSpanName(string name)
    {
        Debug.Assert(name == null || name.Length > 0, "AuditTrailSpanName can be null or non empty");
        AuditTrailSpanName = name;

        return this;
    }
}