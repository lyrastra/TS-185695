using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Outsource.Dto.Outsourcings;

namespace Moedelo.Outsource.Client.Outsourcings;

/// <summary>
/// Получение сотрудников аутсорса
/// </summary>
public interface IOutsourcingsApiClient
{
    /// <summary>
    /// Получение сотрудника аутсорса закрепленного за фирмой по идентификатору аутсорса(accountId) и идентификатору связи фирмы и аутсорс аккаунта(clientId)
    /// </summary>
    Task<OutsourcingDto> GetByClientIdAsync(int firmId, int userId, int clientId, int accountId);

    /// <summary>
    /// Получение сотрудников аутсорса закрепленных за фирмами по идентификатору связи фирмы и аутсорс аккаунта(clientId)
    /// </summary>
    Task<OutsourcingDto[]> GetByClientIdsAsync(IReadOnlyCollection<int> clientIds);

    /// <summary>
    /// Обновляет настройки клиента
    /// </summary>
    Task<int> UpdateAsync(int firmId, int userId, OutsourcingDto dto);
}