using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy
{
    [Obsolete("Не использовать! Заменен на Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore.IIntegratedUserApiClient")]
    public interface IIntegratedUserApiClient
    {
        Task<IntegratedUserDto> GetIntegratedUserByIdInExternalSystemAsync(string externalSystemId, int integrationPartner, HttpQuerySetting setting = null);

        Task<IntegratedUserDto> GetIntegratedUserByFirmId(int firmId, int integrationPartner, HttpQuerySetting setting = null);

        Task<IntegratedUsersPageDto> GetByPageAsync(int integrationPartner, int page, HttpQuerySetting setting = null);

        Task SaveAsync(IntegratedUserDto request, HttpQuerySetting setting = null);

        [Obsolete("Не использовать! Заменен на Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy.IIntegrationSsoApiClient")]
        Task SaveFromSso(SaveFromSsoDto request, HttpQuerySetting setting = null);

        Task<List<IntegrationPartners>> GetActiveIntegrationsForFirmAsync(int firmId);

        Task<IntegratedUserDto> GetLastIntegratedUserAsync(int integrationPartner, HttpQuerySetting setting = null);
    }
}
