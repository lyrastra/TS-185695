using System;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.InfrastructureV2.Domain.Extensions;

public static class DiResolverExtensions
{
    public static IDisposable BeginScopeWithCustomAuditContext(
        this IDIResolver diResolver,
        int firmId, int userId)
    {
        var scope = diResolver.BeginScope();

        var auditContext = diResolver.GetInstance<ICustomAuditContext>();
        auditContext.FirmId = firmId;
        auditContext.UserId = userId;

        return scope;
    }
}
