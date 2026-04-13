using System.Data.Common;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models
{
    public sealed class QuerySetting
    {
        public QuerySetting(DbTransaction transaction = null, int? timeout = null)
        {
            Transaction = transaction;
            Timeout = timeout;
        }

        public DbTransaction Transaction { get; }

        public int? Timeout { get; }
    }
}