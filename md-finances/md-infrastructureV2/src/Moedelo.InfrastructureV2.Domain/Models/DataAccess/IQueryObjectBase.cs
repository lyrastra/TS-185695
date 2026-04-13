#nullable enable
using System.Data;

namespace Moedelo.InfrastructureV2.Domain.Models.DataAccess;

public interface IQueryObjectBase
{
    string Sql { get; }
    CommandType? CommandType { get; }
    string? AuditTrailSpanName { get; }
}
