using Microsoft.Extensions.Logging;

namespace Moedelo.Common.Consul.ServiceDiscovery.Abstractions;

public interface IMoedeloServiceDiscoverySettings
{
    /// <summary>
    /// Промежуток времени, через который сервис должен делать отметку о своей доступности в consul
    /// </summary>
    TimeSpan HealthPassTimeout { get; }

    /// <summary>
    /// Логировать каждую N-ую успешную отметку о доступности сервиса  
    /// </summary>
    int LogEveryNthSuccessfulPass { get; }

    /// <summary>
    /// Время после которого (если не пингануть консул) сервис переходит в статус Warning
    /// </summary>
    TimeSpan ServiceTtl { get; }

    /// <summary>
    /// Время, после которого, сервис будет снят с регистрации в случае отстуствия сигналов о существовании
    /// </summary>
    TimeSpan ServiceDeregistrationTimeout { get; }

    string Domain { get; }

    string AppName { get; }

    /// <summary>
    /// Название сервиса
    /// </summary>
    string ServiceName { get; }

    /// <summary>
    /// Уникальный идентификатор сервиса
    /// </summary>
    string ServiceId { get; }
    
    LogLevel LogLevel { get; }

    /// <summary>
    /// Максимальное время ожидания до регистрации сервиса в консуле
    /// </summary>
    TimeSpan InitialRegistrationWaitTimeout { get; }
}