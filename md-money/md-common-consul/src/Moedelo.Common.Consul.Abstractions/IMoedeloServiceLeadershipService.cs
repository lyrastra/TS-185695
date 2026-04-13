namespace Moedelo.Common.Consul.Abstractions;

/// <summary>
/// Сервис управления лидерством
/// </summary>
public interface IMoedeloServiceLeadershipService
{
    string ConsulSessionId { get; }
    /// <summary>
    /// Захватить лидерство, если оно свободно
    /// </summary>
    /// <param name="leadershipName">название лидерства</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>true - лидерство захвачено, false - не захвачено (уже занято)</returns>
    ValueTask<bool> AcquireLeadershipAsync(string leadershipName, CancellationToken cancellationToken);
    /// <summary>
    /// Освободить захваченное лидерство
    /// </summary>
    /// <param name="leadershipName">название лидерства</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    ValueTask ReleaseLeadershipAsync(string leadershipName, CancellationToken cancellationToken);
}
