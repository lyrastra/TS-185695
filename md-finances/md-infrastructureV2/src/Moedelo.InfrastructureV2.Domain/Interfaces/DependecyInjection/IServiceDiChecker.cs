// ReSharper disable once CheckNamespace
namespace Moedelo.InfrastructureV2.Domain.Interfaces.DependencyInjection;

public interface IServiceDiChecker
{
    void EnsureServicesCanBeCreated();
}