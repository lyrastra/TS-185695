using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Extensions;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CommonV2.UserContext.Domain;

public static class DiResolverExtensions
{
    public static async Task ExecuteInCustomUserContextAsync<TUserContext>(
        this IDIResolver diResolver,
        int firmId,
        int userId,
        Func<TUserContext, Task> action,
        bool continueOnCapturedContext) where TUserContext : IUserContext
    {
        using var scope = diResolver.BeginScopeWithCustomAuditContext(firmId, userId);

        var userContext = diResolver.GetInstance<TUserContext>();
        Debug.Assert(firmId == userContext.FirmId, "FirmId mismatched");
        Debug.Assert(userId == userContext.UserId, "UserId mismatched");

        await action(userContext).ConfigureAwait(continueOnCapturedContext); 
    }

    public static async Task ExecuteInCustomUserContextAsync<TUserContext>(
        this IDIResolver diResolver,
        int firmId,
        int userId,
        Func<TUserContext, CancellationToken, Task> action,
        bool continueOnCapturedContext,
        CancellationToken cancellationToken) where TUserContext: IUserContext
    {
        using var scope = diResolver.BeginScopeWithCustomAuditContext(firmId, userId);
        var userContext = diResolver.GetInstance<TUserContext>();

        Debug.Assert(firmId == userContext.FirmId, "FirmId mismatched");
        Debug.Assert(userId == userContext.UserId, "UserId mismatched");

        await action(userContext, cancellationToken).ConfigureAwait(continueOnCapturedContext);
    }
}
