using Microsoft.Extensions.Logging;
using Moedelo.Common.Consul.ServiceDiscovery.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Consul.ServiceDiscovery.Internals;

[InjectAsSingleton(typeof(IMoedeloServiceDiscoverySettings))]
internal sealed class MoedeloServiceDiscoverySettings : IMoedeloServiceDiscoverySettings
{
    public MoedeloServiceDiscoverySettings(ISettingsConfigurations configuration)
    {
        Domain = configuration.Config.Domain;
        AppName = configuration.Config.AppName;
        ServiceName = $"{Domain}.{AppName}";
        ServiceId = $"{ServiceName}.{Guid.NewGuid():N}";
    }

    /// <summary>
    /// Таймаут в секундах между соседними уведомлениями в consul о нормальном состоянии сервиса
    /// </summary>
    private const int HealthPassEverySeconds = 30;
    /// <summary>
    /// Максимальное время ожидания регистрации сервиса в consul в секундах
    /// </summary>
    private const int InitialRegistrationWaitTimeoutSeconds = 10;
    /// <summary>
    /// Тревожное количество пропущенных "пингов" 
    /// </summary>
    private const int WarnIfMissedHealthPasses = 3;
    /// <summary>
    /// Критическое количество пропущенных "пингов" 
    /// </summary>
    private const int HaltIfMissedHealthPasses = 10;

    public TimeSpan HealthPassTimeout => TimeSpan.FromSeconds(HealthPassEverySeconds);
    public int LogEveryNthSuccessfulPass => 100;
    public TimeSpan ServiceTtl => TimeSpan.FromSeconds(HealthPassEverySeconds * WarnIfMissedHealthPasses);
    public TimeSpan ServiceDeregistrationTimeout => TimeSpan.FromSeconds(HealthPassEverySeconds * HaltIfMissedHealthPasses);
    public string Domain { get; }
    public string AppName { get; }
    public string ServiceName { get; }
    public string ServiceId { get; }
    public LogLevel LogLevel => LogLevel.Information;
    public TimeSpan InitialRegistrationWaitTimeout => TimeSpan.FromSeconds(InitialRegistrationWaitTimeoutSeconds);
}
