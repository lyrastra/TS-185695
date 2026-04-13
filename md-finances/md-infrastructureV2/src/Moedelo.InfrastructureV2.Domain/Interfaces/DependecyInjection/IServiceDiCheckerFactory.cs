using System;

// ReSharper disable once CheckNamespace
namespace Moedelo.InfrastructureV2.Domain.Interfaces.DependencyInjection;

public interface IServiceDiCheckerFactory
{
    IServiceDiChecker CreateChecker(params Type[] serviceTypes);
}