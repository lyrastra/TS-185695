#nullable enable
namespace Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

public interface IDiRegistry
{
    IDiRegistry RegisterSingleton<TAncestor, TDescendant>() where TDescendant : TAncestor;
    IDiRegistry RegisterPerWebRequest<TAncestor, TDescendant>() where TDescendant : TAncestor;
    IDiRegistry RegisterTransient<TAncestor, TDescendant>() where TDescendant : TAncestor;
}
