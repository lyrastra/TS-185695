using System.Data;
using System.Diagnostics;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.SqlDataAccess.Abstractions.Models;

public class MssqlBulkCopyQueryObject : IBulkCopyQueryObject, IAuditTrailSpanNameSource
{
    private readonly BulkCopyQueryObject queryObject;

    public MssqlBulkCopyQueryObject(string name, DataTable dataTable)
    {
        queryObject = new BulkCopyQueryObject(name, dataTable);
    }

    public string Name => queryObject.Name;

    public DataTable DataTable => queryObject.DataTable;
    
    public string AuditTrailSpanName { get; private set; } = null;

    public MssqlBulkCopyQueryObject WithAuditTrailSpanName(string name)
    {
        Debug.Assert(name == null || name.Length > 0, "AuditTrailSpanName can be null or non empty");
        AuditTrailSpanName = name;

        return this;
    }
}
