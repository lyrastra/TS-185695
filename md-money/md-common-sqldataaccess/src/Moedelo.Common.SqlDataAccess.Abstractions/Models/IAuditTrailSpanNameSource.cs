#nullable enable
namespace Moedelo.Common.SqlDataAccess.Abstractions.Models;

public interface IAuditTrailSpanNameSource
{
    public string? AuditTrailSpanName { get; }
}
