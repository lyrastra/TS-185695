using Microsoft.AspNetCore.Mvc;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore
{
    public interface IIntegratedUserClient
    {
        Task<IntegratedUserDto> GetByFirmIdAsync(int firmId, IntegrationPartners partner, CancellationToken cancellationToken = default);

        Task<IntegratedUserDto> GetLastIntegratedUserAsync(IntegrationPartners partner, CancellationToken cancellationToken = default);

        Task<int> UpdateTokenDataAsync(UpdateTokenDataRequestDto dto, CancellationToken cancellationToken = default);

        Task<string> GetTokenDataAsync(IntegrationPartners partner, int firmId, CancellationToken cancellationToken = default);

        Task<int> UpdateIntegrationDataAsync(UpdateIntegrationDataRequestDto dto, CancellationToken cancellationToken = default);

        Task<string> GetIntegrationDataAsync(IntegrationPartners partner, int firmId, CancellationToken cancellationToken = default);

        Task<int> InsertAsync(IntegratedUserBaseDto dto, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<IntegrationPartnersInfoDto>> GetActiveIntegrationsPartnersInfoAsync(int firmId, CancellationToken cancellationToken = default);

        Task<IntegratedUserDto> GetIntegratedUserByIdInExternalSystemAsync(string externalSystemId, int integrationPartner, HttpQuerySetting setting = null, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<IntegratedUserDto>> GetIntegratedUsersByExternalSystemIdAsync(string externalSystemId, int integrationPartner, CancellationToken cancellationToken = default);

        Task<IntegratedUsersPageDto> GetByPageAsync(int integrationPartner, int page, 
            int pageSize = 100, bool? isActive = null, HttpQuerySetting setting = null, CancellationToken cancellationToken = default);

        Task<ExtentedIntegratedUserDto[]> GetForAutoImportAsync(IntegratedUserAutoImportRequestDto dto, HttpQuerySetting setting = null, CancellationToken cancellationToken = default);

        Task SaveAsync(IntegratedUserDto request, HttpQuerySetting setting = null, CancellationToken cancellationToken = default);

        Task DisableIntegrationAsync(DisableIntegrationRequestDto dto, HttpQuerySetting setting = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить базовые данные интеграционного пользователя по идентификатору фирмы и партнеру
        /// </summary>
        /// <param name="firmId">Идентификатор фирмы</param>
        /// <param name="partner">Партнер</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns> Интеграционный пользователь <see cref="IntegratedUserBaseDto"/> с базовой информацией.</returns>
        Task<IntegratedUserBaseDto> GetBaseByFirmIdAsync(int firmId, IntegrationPartners partner, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<IntegratedUserDto>> GetActiveIntegratedUsersByPartnerAsync(PartnerActiveIntegratedUsersRequestDto dto, CancellationToken cancellationToken = default);
    }
}