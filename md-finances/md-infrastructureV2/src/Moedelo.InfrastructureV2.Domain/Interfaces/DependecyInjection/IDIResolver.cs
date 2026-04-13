using System;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

public interface IDIResolver : IDI
{
    TR GetInstance<TR>();

    TR GetInstanceStateless<TR>();

    IDisposable BeginScope();
}