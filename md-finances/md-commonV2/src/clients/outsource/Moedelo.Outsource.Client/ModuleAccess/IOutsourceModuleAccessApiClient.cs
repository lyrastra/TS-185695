using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Outsource.Dto.ModuleAccess;

namespace Moedelo.Outsource.Client.ModuleAccess;

/// <summary>
/// Подключаемые к аккаунту модули
/// </summary>
public interface IOutsourceModuleAccessApiClient
{
    /// <summary>
    /// Включенные модули по списку аккаунтов (с фильтром по типу)
    /// </summary>
    Task<IReadOnlyList<ModuleAccessDto>> GetAsync(ModuleGetDto dto, CancellationToken cancellationToken = default);
    /// <summary>
    /// Включить модуль для аккаунта
    /// </summary>
    Task EnableAsync(int accountId, ModuleType type);

    /// <summary>
    /// Выключить модуль для аккаунта
    /// </summary>
    Task DisableAsync(int accountId, ModuleType type);

    Task<IReadOnlyList<ModuleAccessDto>> GetByTypesAsync(int accountId, ModuleType[] types, CancellationToken cancellationToken = default);
}