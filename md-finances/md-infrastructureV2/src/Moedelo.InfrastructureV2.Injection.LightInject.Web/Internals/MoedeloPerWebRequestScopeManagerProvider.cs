using LightInject;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Web.Internals;

internal sealed class MoedeloPerWebRequestScopeManagerProvider : ScopeManagerProvider
{
    /// <inheritdoc/>
    protected override IScopeManager CreateScopeManager(IServiceFactory serviceFactory)
    {
        return new MoedeloPerWebRequestScopeManager(serviceFactory);
    }
}
