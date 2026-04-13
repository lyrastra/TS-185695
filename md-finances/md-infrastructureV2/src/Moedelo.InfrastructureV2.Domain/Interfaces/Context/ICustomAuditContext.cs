namespace Moedelo.InfrastructureV2.Domain.Interfaces.Context;

/// <summary>
/// Класс, использующийся для подмены значений UserId и FirmId в IAuditContext
/// Любое выставленное значение будет использовано как соответствующее значение в AuditContext 
/// </summary>
public interface ICustomAuditContext
{
    int? UserId { get; set; }

    int? FirmId { get; set; }
}