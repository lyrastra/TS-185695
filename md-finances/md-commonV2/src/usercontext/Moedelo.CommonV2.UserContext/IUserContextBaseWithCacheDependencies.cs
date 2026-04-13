using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.CommonV2.UserContext.Domain.BillingContext;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.CommonV2.UserContext;

/// <summary>
/// все зависимости, необходимые для создания экземпляра класса UserContextBaseWithCache
/// класс создан для того, чтобы список зависимостей можно было править, не правя все места
/// использования UserContextBaseWithCache
/// </summary>
public interface IUserContextBaseWithCacheDependencies
{
    ILogger Logger { get; }
    IAuditContext AuditContext { get; }
    IAuthorizationContext AuthorizationContext { get; }
    IFirmBillingContext BillingContext { get; }
    IUserContextExtraDataLoader UserContextExtraDataLoader { get; }
}