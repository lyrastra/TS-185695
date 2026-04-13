using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.SqlDataAccess.Abstractions.Models;

public class MssqlQueryObject : IQueryObject, IAuditTrailSpanNameSource
{
    private readonly QueryObject queryObject;
    
    public MssqlQueryObject(
        string sql,
        object queryParams,
        TemporaryTable temporaryTable,
        CommandType? commandType = null)
    {
        queryObject = new QueryObject(sql, queryParams, temporaryTable, commandType);
    }

    public MssqlQueryObject(
        string sql,
        object queryParams = null,
        CommandType? commandType = null,
        IReadOnlyCollection<TemporaryTable> temporaryTables = null)
    {
        queryObject = new QueryObject(sql, queryParams, commandType, temporaryTables);
    }
    
    public string Sql => queryObject.Sql;

    public object QueryParams => queryObject.QueryParams;

    public IReadOnlyCollection<TemporaryTable> TemporaryTables => queryObject.TemporaryTables;

    public CommandType? CommandType => queryObject.CommandType;

    public string AuditTrailSpanName { get; private set; } = null;

    public MssqlQueryObject WithAuditTrailSpanName(string name)
    {
        Debug.Assert(name == null || name.Length > 0, "AuditTrailSpanName can be null or non empty");
        AuditTrailSpanName = name;

        return this;
    }
}

