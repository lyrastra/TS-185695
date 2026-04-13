#nullable enable
namespace Moedelo.Common.PostgreSqlDataAccess.Abstractions;

public interface IAuditTrailSpanNameSource
{
    public string? AuditTrailSpanName { get; }
}