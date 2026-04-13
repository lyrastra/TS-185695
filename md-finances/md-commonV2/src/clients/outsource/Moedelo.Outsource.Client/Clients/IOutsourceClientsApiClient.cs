using Moedelo.Outsource.Dto.Clients;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Outsource.Client.Clients;

/// <summary>
/// Получение связи аутсорс аккаунта и фирмы на обслуживании
/// </summary>
public interface IOutsourceClientsApiClient
{
    /// <summary>
    /// Получение связи по firmId
    /// </summary>
    Task<ClientDto> GetByFirmIdAsync(int firmId, int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение связей по firmId
    /// </summary>
    Task<ClientDto[]> GetByFirmIdsAsync(IReadOnlyCollection<int> firmIds);
}