using System.Data;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

public interface IBulkCopyQueryObject
{
    string Name { get; }
    DataTable DataTable { get; }
}
