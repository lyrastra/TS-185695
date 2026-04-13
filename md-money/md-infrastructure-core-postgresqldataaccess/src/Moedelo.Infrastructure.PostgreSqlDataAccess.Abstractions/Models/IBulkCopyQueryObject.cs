using System.Collections.Generic;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

public interface IBulkCopyQueryObject
{
    string TableName { get; }
    IList<DbTypedColumnInfo> TableHeaders { get; }
    IList<IReadOnlyList<object>> TableRows { get; }
}
