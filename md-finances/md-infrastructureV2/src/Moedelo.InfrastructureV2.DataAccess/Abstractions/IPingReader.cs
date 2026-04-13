using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;

namespace Moedelo.InfrastructureV2.DataAccess.Abstractions;

/// <summary>
/// Проверка доступности ("ping") базы данных
/// </summary>
/// <typeparam name="TDbExecutor">Тип, реализующий интерфейс IDbExecutor.</typeparam>
public interface IPingReader<TDbExecutor> where TDbExecutor : IDbExecutor
{
    Task<string> PingAsync();
}
