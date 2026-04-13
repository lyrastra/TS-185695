using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Abstraction.Models;

namespace Moedelo.Infrastructure.Consul.Abstraction;

/// <summary>
/// API доступа к <a href="https://developer.hashicorp.com/consul/api-docs/agent">Consul Agent API</a>
/// </summary>
public interface IConsulAgentApiClient
{
    /// <summary>
    /// Зарегистрировать сервис в consul service discovery
    /// </summary>
    /// <param name="registration">данные регистрации</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    Task RegisterServiceAsync(
        AgentServiceRegistration registration,
        CancellationToken cancellationToken);

    /// <summary>
    /// Отменить регистрацию сервиса в consul service discovery
    /// </summary>
    /// <param name="serviceId">идентификатор сервиса в consul service discovery</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    Task DeregisterServiceAsync(
        string serviceId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Проверить наличие регистрации сервиса в consul service discovery
    /// </summary>
    /// <param name="serviceId">идентификатор сервиса в consul service discovery</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    Task<bool> IsServiceRegisteredAsync(
        string serviceId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Отправить в consul service discovery уведомление о том, что сервис "жив".
    /// Такие уведомления должны отправляться на регулярной основе на основании данных регистрации
    /// </summary>
    /// <param name="checkId">идентификатор TTL-проверки, связанной с сервисом (см. <see cref="AgentServiceRegistration.Check"/> </param>
    /// <param name="note">строковый комментарий, который будет сохранён для сервиса</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    Task SendServiceCheckTtlAsync(
        string checkId,
        string note,
        CancellationToken cancellationToken);
}
