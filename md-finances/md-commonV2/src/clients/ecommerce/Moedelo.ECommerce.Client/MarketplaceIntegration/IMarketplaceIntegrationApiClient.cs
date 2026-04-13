using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Marketplaces;
using Moedelo.ECommerce.Dto.MarketplaceIntegration;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ECommerce.Client.MarketplaceIntegration
{
    public interface IMarketplaceIntegrationApiClient: IDI
    {
        Task<IntegrationManagementResultDto> AddAsync(int firmId, int userId, MarketplaceType marketplace, string data);

        Task<IntegrationManagementResultDto> UpdateAsync(int firmId, int userId, MarketplaceType marketplace, string data);

        Task<string> GetAsync(int firmId, int userId, MarketplaceType marketplace);

        Task<DateTime?> GetCredentialsExpirationDateAsync(int firmId, int userId, MarketplaceType marketplace);

        Task<bool> TurnAsync(int firmId, int userId, MarketplaceType marketplace, bool status);

        Task<List<IntegrationStatusDto>> GetStatusesAsync(int firmId, int userId, bool isNeedMarketplaceRequest);

        Task<bool> GetAutoImportAsync(int firmId, int userId, MarketplaceType marketplace);
        
        Task<int> SetAutoImportAsync(int firmId, int userId, MarketplaceType marketplace, bool status);
    }
}