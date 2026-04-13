using System.Data.Common;

namespace Moedelo.InfrastructureV2.Domain.Models.DataAccess;

public class QuerySetting
{
    public QuerySetting() : this(null, null)
    {
    }

    public QuerySetting(DbTransaction transaction = null, int? timeout = null)
    {
        Transaction = transaction;
        Timeout = timeout;
    }

    public DbTransaction Transaction { get; set; }

    /// <summary>
    /// The command timeout (in seconds)
    /// </summary>
    public int? Timeout { get; set; }
}