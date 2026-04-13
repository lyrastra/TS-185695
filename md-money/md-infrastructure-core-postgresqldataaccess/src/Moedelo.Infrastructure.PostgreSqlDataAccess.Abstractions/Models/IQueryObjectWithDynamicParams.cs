using System.Collections.Generic;
using System.Data;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

public interface IQueryObjectWithDynamicParams
{
    string Sql { get; }
    IReadOnlyCollection<DynamicParam> DynamicParams { get; }
    IReadOnlyCollection<TemporaryTable> TemporaryTables { get; }
    CommandType? CommandType { get; }
}
