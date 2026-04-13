#nullable enable
using System.Collections.Generic;
using System.Diagnostics;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.PostgreSqlDataAccess.Abstractions;

public sealed class PsqlBulkCopyQueryObject : IBulkCopyQueryObject, IAuditTrailSpanNameSource
{
    private readonly BulkCopyQueryObject queryObject;

    private PsqlBulkCopyQueryObject(BulkCopyQueryObject queryObject)
    {
        this.queryObject = queryObject;
    }

    public PsqlBulkCopyQueryObject(string tableName,
        IList<DbTypedColumnInfo> tableHeaders,
        IList<IReadOnlyList<object>> tableRows)
    {
        this.queryObject = new BulkCopyQueryObject(tableName, tableHeaders, tableRows);
    }

    public static PsqlBulkCopyQueryObject FromCollection<TRow>(string tableName, IEnumerable<TRow> collection)
        where TRow : class
    {
        return new PsqlBulkCopyQueryObject(BulkCopyQueryObject.FromCollection(tableName, collection));
    }

    public string? AuditTrailSpanName { get; private set; } = null;

    public PsqlBulkCopyQueryObject WithAuditTrailSpanName(string name)
    {
        Debug.Assert(name == null || name.Length > 0, "AuditTrailSpanName can be null or non empty");
        AuditTrailSpanName = name;

        return this;
    }

    public string TableName => queryObject.TableName;

    public IList<DbTypedColumnInfo> TableHeaders => queryObject.TableHeaders;

    public IList<IReadOnlyList<object>> TableRows => queryObject.TableRows;
}
