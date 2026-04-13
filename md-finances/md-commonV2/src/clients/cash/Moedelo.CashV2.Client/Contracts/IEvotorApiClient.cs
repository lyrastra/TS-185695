using Moedelo.CashV2.Dto.Evotor;
using Moedelo.CashV2.Dto.Evotor.Partner;
using Moedelo.Common.Enums.Enums.Evotor.Sessions;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.CashV2.Client.Contracts
{
    public interface IEvotorApiClient : IDI
    {
        Task<IntegrationStatusDto> GetIntegrationStatusByFirmAsync(int firmId, int userId);

        Task DeactivateIntegrationForFirmAsync(int firmId, int userId);

        Task<bool> UpdateEvotorEntitiesForFirmAsync(
            int firmId,
            int userId,
            DateTime startDate,
            DateTime endDate,
            bool reloadRemovedDocuments = false);

        Task<ListWithCountDto<EvotorSessionInfoDto>> GetSessionListWithCountAsync(
            int firmId,
            int userId,
            int offset = 0,
            int size = 50,
            SortType sortType = SortType.ByOpeningDateDesc);

        Task<bool> IsIntegrationActiveAsync(int firmId, int userId);

        Task<bool> HasEverBeenIntegratedAsync(int firmId, int userId, CancellationToken cancellationToken = default);

        Task<IList<EvotorIntegrationDto>> GetIntegrationsAsync(bool onlyActive = false);

        Task ActualizeStatusAsync(EvotorIntegrationDto integration);

        Task UpdateDataAsync(int firmId);
    }
}