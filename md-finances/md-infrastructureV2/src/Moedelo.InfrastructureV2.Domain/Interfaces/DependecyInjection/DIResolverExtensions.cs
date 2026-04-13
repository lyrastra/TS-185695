using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

public static class DIResolverExtensions
{
    public static ILogger Logger(this IDIResolver idi) => idi.GetInstanceStateless<ILogger>();
}
