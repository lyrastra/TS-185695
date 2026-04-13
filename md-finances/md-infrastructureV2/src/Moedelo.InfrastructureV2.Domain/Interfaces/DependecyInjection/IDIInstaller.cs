#nullable enable
using System;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

public interface IDIInstaller : IDIResolver
{
    void Initialize(Action<IDiRegistry>? finalizeRegistration = null);
    void RegisterPreDisposeHandler(Action action);
}