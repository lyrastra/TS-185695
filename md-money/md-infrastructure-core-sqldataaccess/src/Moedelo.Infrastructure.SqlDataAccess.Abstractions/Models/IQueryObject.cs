using System.Collections.Generic;
using System.Data;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

public interface IQueryObject
{
    string Sql { get; }
    object? QueryParams { get; }
    IReadOnlyCollection<TemporaryTable>? TemporaryTables { get; }
    CommandType? CommandType { get; }
}
