namespace Moedelo.InfrastructureV2.Domain.Interfaces.Context;

public interface IWebRequestAuditContext : IAuditContextBase
{
    int? ClientId { get; }
}