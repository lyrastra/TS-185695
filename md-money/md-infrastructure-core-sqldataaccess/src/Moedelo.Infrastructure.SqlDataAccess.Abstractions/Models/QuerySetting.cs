using System.Data.Common;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

public sealed class QuerySetting
{
    public QuerySetting(DbTransaction? transaction = null, int? timeoutSeconds = null)
    {
        Transaction = transaction;
        TimeoutSeconds = timeoutSeconds;
    }

    public DbTransaction? Transaction { get; }

    public int? TimeoutSeconds { get; }
}