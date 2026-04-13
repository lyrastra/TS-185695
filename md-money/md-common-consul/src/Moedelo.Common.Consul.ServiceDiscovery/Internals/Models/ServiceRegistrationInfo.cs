using Moedelo.Infrastructure.Consul.Abstraction.Models;

namespace Moedelo.Common.Consul.ServiceDiscovery.Internals.Models;

/// <summary>
/// Сведения о регистрации службы
/// </summary>
/// <param name="ServiceRegistration">Параметры регистрации службы</param>
internal sealed record ServiceRegistrationInfo(AgentServiceRegistration? ServiceRegistration)
{
    /// <summary>
    /// Идентификатор регистрации службы
    /// </summary>
    public string? ServiceRegistrationId => ServiceRegistration?.ID;
    /// <summary>
    /// Идентификатор регистрации проверки здоровья службы
    /// </summary>
    public string? CheckRegistrationId => $"service:{ServiceRegistration?.ID}";
}
