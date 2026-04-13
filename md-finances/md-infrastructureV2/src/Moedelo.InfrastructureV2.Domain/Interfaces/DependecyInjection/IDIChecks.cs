using System;
using System.Collections.Generic;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

public interface IDIChecks : IDI
{
    void CheckControllersCreation();
    void EnsureServicesCanBeCreated(IEnumerable<Type> services);
}