namespace Moedelo.InfrastructureV2.Domain.Interfaces.Context;

public interface IAuditContextBase
{
    int? UserId { get; }

    int? FirmId { get; }
}