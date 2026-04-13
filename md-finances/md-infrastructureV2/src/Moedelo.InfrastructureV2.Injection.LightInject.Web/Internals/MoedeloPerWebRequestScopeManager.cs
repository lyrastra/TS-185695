using LightInject;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Web.Internals;

internal sealed class MoedeloPerWebRequestScopeManager : ScopeManager
{
    private readonly LogicalThreadStorage<Scope> currentScope = new ();

    public MoedeloPerWebRequestScopeManager(IServiceFactory serviceFactory) : base(serviceFactory)
    {
    }

    public override Scope CurrentScope
    {
        get => GetThisScopeOrFirstValidAncestor(currentScope.Value);
        set => currentScope.Value = value;
    }

    public void RestoreRootScopeIfMissed(Scope scope)
    {
        currentScope.Value ??= scope;
    }
}