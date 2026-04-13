namespace Moedelo.Infrastructure.Consul.Abstraction;

/// <summary>
/// Стратегия генерации имен сессий Consul
/// </summary>
public interface IConsulSessionNamingStrategy
{
    /// <summary>
    /// Генерация имени сессии Consul
    /// </summary>
    /// <returns>имя новой сессии</returns>
    public string GenerateSessionName();
}