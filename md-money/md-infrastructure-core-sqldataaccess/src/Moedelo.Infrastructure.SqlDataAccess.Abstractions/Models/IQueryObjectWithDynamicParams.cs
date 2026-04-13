using System.Collections.Generic;
using System.Data;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

public interface IQueryObjectWithDynamicParams
{
    string Sql { get; }
    IReadOnlyCollection<DynamicParam> DynamicParams { get; }
    IReadOnlyCollection<TemporaryTable>? TemporaryTables { get; }
    CommandType? CommandType { get; }
}
