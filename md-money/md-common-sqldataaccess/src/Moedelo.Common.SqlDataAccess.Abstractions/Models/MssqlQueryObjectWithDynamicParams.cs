using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.SqlDataAccess.Abstractions.Models;

public class MssqlQueryObjectWithDynamicParams : IQueryObjectWithDynamicParams, IAuditTrailSpanNameSource
{
    private readonly QueryObjectWithDynamicParams queryObject;

    public MssqlQueryObjectWithDynamicParams(
        string sql, 
        IReadOnlyCollection<DynamicParam> dynamicParams,
        IReadOnlyCollection<TemporaryTable> temporaryTables = null,
        CommandType? commandType = null)
    {
        queryObject = new QueryObjectWithDynamicParams(sql, dynamicParams, temporaryTables, commandType);
    }

    public string Sql => queryObject.Sql;

    public IReadOnlyCollection<DynamicParam> DynamicParams => queryObject.DynamicParams;

    public IReadOnlyCollection<TemporaryTable> TemporaryTables => queryObject.TemporaryTables;

    public CommandType? CommandType => queryObject.CommandType;

    public string AuditTrailSpanName { get; private set; } = null;

    public MssqlQueryObjectWithDynamicParams WithAuditTrailSpanName(string name)
    {
        Debug.Assert(name == null || name.Length > 0, "AuditTrailSpanName can be null or non empty");
        AuditTrailSpanName = name;

        return this;
    }
}
