using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Authorization.Dto;

namespace Moedelo.Authorization.Client.Abstractions;

/// <summary>
/// Получение данных по функциональным лимитам, установленным для фирмы 
/// </summary>
public interface IFeatureLimitsClient
{
    /// <summary>
    /// Получить описание лимита, установленного для фирмы
    /// </summary>
    /// <returns>если лимит установлен - описание лимита, если лимит не установлен - null</returns>
    Task<FirmFeatureLimitDto> GetActualAsync(int firmId, FeatureLimitId limitId);

    /// <summary>
    /// Получить описание лимита, установленного для фирмы
    /// </summary>
    /// <returns>список установленных лимитов (для неустановленных лимитов в списке не будет записей)</returns>
    Task<List<FirmFeatureLimitDto>> GetActualAsync(int firmId, IReadOnlyCollection<FeatureLimitId> limitIds);

    /// <summary>
    /// Получить все периоды действия указанных лимитов, установленных для фирмы
    /// </summary>
    /// <returns>список установленных лимитов</returns>
    Task<List<FirmFeatureLimitDto>> GetAsync(int firmId, IReadOnlyCollection<string> limitCodes);
}