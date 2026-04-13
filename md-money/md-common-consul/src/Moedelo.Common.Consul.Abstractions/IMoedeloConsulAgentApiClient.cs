using Moedelo.Infrastructure.Consul.Abstraction.Models;

namespace Moedelo.Common.Consul.Abstractions;

public interface IMoedeloConsulAgentApiClient
{
    /// <summary>
    /// Зарегистрировать сервис в consul service discovery
    /// </summary>
    /// <param name="registration">данные регистрации</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    ValueTask RegisterServiceAsync(
        AgentServiceRegistration registration,
        CancellationToken cancellationToken);

    /// <summary>
    /// Отменить регистрацию сервиса в consul service discovery
    /// </summary>
    /// <param name="serviceId">идентификатор сервиса в consul service discovery</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    ValueTask DeregisterServiceAsync(
        string serviceId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Проверить наличие регистрации сервиса в consul service discovery
    /// </summary>
    /// <param name="serviceId">идентификатор сервиса в consul service discovery</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    ValueTask<bool> IsServiceRegisteredAsync(
        string serviceId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Отправить в consul service discovery уведомление о том, что сервис "жив".
    /// Такие уведомления должны отправляться на регулярной основе на основании данных регистрации
    /// </summary>
    /// <param name="checkId">идентификатор TTL-проверки, связанной с сервисом (см. <see cref="AgentServiceRegistration.Check"/> </param>
    /// <param name="note">строковый комментарий, который будет сохранён для сервиса</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    ValueTask SendServiceCheckTtlAsync(
        string checkId,
        string note,
        CancellationToken cancellationToken);
}
