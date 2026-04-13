using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.CommonV2.UserContext.Domain.BillingContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.CommonV2.UserContext;

[InjectPerWebRequest(typeof(IUserContextBaseWithCacheDependencies))]
public sealed class UserContextBaseWithCacheDependencies : IUserContextBaseWithCacheDependencies
{
    public UserContextBaseWithCacheDependencies(
        ILogger logger,
        IAuditContext auditContext,
        IFirmBillingContext billingContext,
        IAuthorizationContext authorizationContext,
        IUserContextExtraDataLoader userContextExtraDataLoader)
    {
        Logger = logger;
        AuditContext = auditContext;
        BillingContext = billingContext;
        AuthorizationContext = authorizationContext;
        UserContextExtraDataLoader = userContextExtraDataLoader;
    }

    public ILogger Logger { get; }
    public IAuditContext AuditContext { get; }
    public IAuthorizationContext AuthorizationContext { get; }
    public IFirmBillingContext BillingContext { get; }
    public IUserContextExtraDataLoader UserContextExtraDataLoader { get; }
}