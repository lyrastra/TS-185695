#nullable enable
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Injection.Lightinject;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Internals;

internal sealed class DiRegistryProxy(DIInstaller installer) : IDiRegistry
{
    public IDiRegistry RegisterSingleton<TAncestor, TDescendant>() where TDescendant : TAncestor
    {
        installer.RegisterSingleton<TAncestor, TDescendant>();
        return this;
    }

    public IDiRegistry RegisterPerWebRequest<TAncestor, TDescendant>() where TDescendant : TAncestor
    {
        installer.RegisterPerWebRequest<TAncestor, TDescendant>();
        return this;
    }

    public IDiRegistry RegisterTransient<TAncestor, TDescendant>() where TDescendant : TAncestor
    {
        installer.RegisterTransient<TAncestor, TDescendant>();
        return this;
    }
}
