namespace Moedelo.Common.Consul.ServiceDiscovery.Abstractions;

public interface IMoedeloConsulServiceDiscovery
{
    string ServiceId { get; }

    /// <summary>
    /// Запустить цикл оповещение о работоспособности сервиса в Consul Service Discovery
    /// </summary>
    /// <param name="listenerAddress"></param>
    /// <param name="cancellationToken">токен отмены операции</param>
    Task ExecuteAsync(Uri listenerAddress, CancellationToken cancellationToken);

    /// <summary>
    /// Дождаться окончания регистрации сервиса в Consul Service Discovery
    /// </summary>
    /// <param name="cancellationToken">токен отмены операции</param>
    Task WaitForRegistrationCompleteAsync(CancellationToken cancellationToken);
}